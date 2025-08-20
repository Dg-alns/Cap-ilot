using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Vivox;
using Unity.Services.Vivox.AudioTaps;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Android;
using Unity.Services.Authentication;


public class ChatTextVivox : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _textNoFriend;
    [SerializeField] private GameObject _uiButtonFriend;

    List<RosterItem> rosterList = new List<RosterItem>();

    private IList<KeyValuePair<string, MessageObject>> m_MessageObjPool = new List<KeyValuePair<string, MessageObject>>();

    public GameObject ChatContentObj;
    public GameObject MessageObject;

    //public string playerId;
    static private string _channelName;

    ScrollRect m_TextChatScrollRect;
    public TMP_InputField MessageInputField;
    
    private Task FetchMessages = null;
    private DateTime? oldestMessage = null;

    private string _idOtherPlayer;

    private void Start()
    {



        StartCoroutine(WaitForVivoxInitThenSubscribe());
        /*VivoxService.Instance.ChannelJoined += OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived += OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;
        //VivoxService.Instance.ChannelMessageEdited += OnChannelMessageEdited;
        //VivoxService.Instance.ChannelMessageDeleted += OnChannelMessageDeleted;

        m_TextChatScrollRect = GetComponent<ScrollRect>();
        m_TextChatScrollRect.onValueChanged.AddListener(ScrollRectChange);*/
    }

    private IEnumerator WaitForVivoxInitThenSubscribe()
    {
        // Attend que Vivox soit prêt
        while (!VivoxService.Instance.IsLoggedIn)
        {
            Debug.Log("Waiting for Vivox to initialize...");
            yield return null;
        }

        _channelName = VivoxVoiceManager.LobbyChannelName;
        Debug.Log("Vivox ready, setting up ChatTextVivox");
        VivoxService.Instance.JoinGroupChannelAsync(
                _channelName,
                ChatCapability.TextOnly
            );//fait en sorte que l'on join ici a la place, ca fonctionne donc c bien
        VivoxService.Instance.ChannelJoined += OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived += OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;

        m_TextChatScrollRect = GetComponent<ScrollRect>();
        m_TextChatScrollRect.onValueChanged.AddListener(ScrollRectChange);
    }

    void OnDestroy()
    {
        VivoxService.Instance.ChannelJoined -= OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived -= OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived -= OnChannelMessageReceived;

        MessageInputField.onEndEdit.RemoveAllListeners();
        m_TextChatScrollRect.onValueChanged.RemoveAllListeners();
    }


    public void AddFriendChannel(string PlayerID)
    {
        /*foreach (KeyValuePair<string, MessageObject> keyValuePair in m_MessageObjPool)
        {
            Destroy(keyValuePair.Value.gameObject);
        }
        m_MessageObjPool.Clear();*/
        _uiButtonFriend.GetComponent<ButtonFriend>().ChangePanel();
        VivoxService.Instance.LeaveAllChannelsAsync();
        _channelName = GetPrivateChannelName(PlayerID, AuthenticationService.Instance.PlayerId);
        VivoxService.Instance.JoinGroupChannelAsync(
                _channelName,
                ChatCapability.TextOnly
        );

        //FetchMessages = FetchHistory(true);
    }

    public void JoinLobbyChannel()
    {
        _textNoFriend.gameObject.SetActive(false);
        VivoxService.Instance.LeaveAllChannelsAsync();
        _channelName = VivoxVoiceManager.LobbyChannelName;
        VivoxService.Instance.JoinGroupChannelAsync(
                _channelName,
                ChatCapability.TextOnly
        );
    }
    public void JoinFriendChannel()
    {
        foreach (KeyValuePair<string, MessageObject> keyValuePair in m_MessageObjPool)
        {
            Destroy(keyValuePair.Value.gameObject);
        }
        m_MessageObjPool.Clear();
        VivoxService.Instance.LeaveAllChannelsAsync();
        if (PlayerPrefs.HasKey("otherPlayerId"))
        {
            _channelName = GetPrivateChannelName(PlayerPrefs.GetString("otherPlayerId"), AuthenticationService.Instance.PlayerId);
            VivoxService.Instance.JoinGroupChannelAsync(
                    _channelName,
                    ChatCapability.TextOnly
            );
        }
        else
        {
            _textNoFriend.gameObject.SetActive(true);
        }
        
    }

    private void ScrollRectChange(Vector2 vector)
    {
        // Scrolled near end and check if we are fetching history already
        if (m_TextChatScrollRect.verticalNormalizedPosition >= 0.95f && FetchMessages != null && (FetchMessages.IsCompleted || FetchMessages.IsFaulted || FetchMessages.IsCanceled))
        {
            m_TextChatScrollRect.normalizedPosition = new Vector2(0, 0.8f);
            //FetchMessages = FetchHistory(false);
        }
    }

    void OnChannelJoined(string channelName)
    {
        foreach (KeyValuePair<string, MessageObject> keyValuePair in m_MessageObjPool)
        {
            Destroy(keyValuePair.Value.gameObject);
        }
        m_MessageObjPool.Clear();
        FetchMessages = FetchHistory(true);
    }
    public void History()
    {
        if(VivoxService.Instance.IsLoggedIn){

            FetchMessages = FetchHistory(true);
        }
    }

    private async Task FetchHistory(bool scrollToBottom = false)
    {
        try
        {
            Debug.Log(_channelName);
            var chatHistoryOptions = new ChatHistoryQueryOptions()
            {
                TimeEnd = oldestMessage
            };
            var historyMessages =
                await VivoxService.Instance.GetChannelTextMessageHistoryAsync(_channelName, 20,
                    null);
            var reversedMessages = historyMessages.Reverse();
            foreach (var historyMessage in reversedMessages)
            {
                AddMessageToChat(historyMessage, true, scrollToBottom);
            }

            // Update the oldest message ReceivedTime if it exists to help the next fetch get the next batch of history
            oldestMessage = historyMessages.FirstOrDefault()?.ReceivedTime;
        }
        catch (TaskCanceledException e)
        {
            Debug.Log($"Chat history request was canceled, likely because of a logout or the data is no longer needed: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Tried to fetch chat history and failed with error: {e.Message}");
        }
    }

    void OnDirectedMessageReceived(VivoxMessage message)
    {
        AddMessageToChat(message, false, true);
    }

    void AddMessageToChat(VivoxMessage message, bool isHistory = false, bool scrollToBottom = false)
    {
        var newMessageObj = Instantiate(MessageObject, ChatContentObj.transform);
        var newMessageTextObject = newMessageObj.GetComponent<MessageObject>();
        if (isHistory)
        {
            m_MessageObjPool.Insert(0, new KeyValuePair<string, MessageObject>(message.MessageId, newMessageTextObject));
            newMessageObj.transform.SetSiblingIndex(0);
        }
        else
        {
            m_MessageObjPool.Add(new KeyValuePair<string, MessageObject>(message.MessageId, newMessageTextObject));
        }

        newMessageTextObject.SetTextMessage(message);
        if (scrollToBottom)
        {
            StartCoroutine(SendScrollRectToBottom());
        }

    }

    IEnumerator SendScrollRectToBottom()
    {
        yield return new WaitForEndOfFrame();

        // We need to wait for the end of the frame for this to be updated, otherwise it happens too quickly.
        m_TextChatScrollRect.normalizedPosition = new Vector2(0, 0);

        yield return null;
    }

    void OnChannelMessageReceived(VivoxMessage message)
    {
        AddMessageToChat(message, false, true);
    }

    public void SendMessage()
    {
        if (!VivoxService.Instance.IsLoggedIn)
        {
            Debug.LogWarning("User not logged in yet.");
            return;
        }

        if (string.IsNullOrEmpty(MessageInputField.text))
        {
            return;
        }
        VivoxService.Instance.SendChannelTextMessageAsync(
            _channelName,
            MessageInputField.text
        ).ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError("Message send failed: " + task.Exception.Message);
            }
            else
            {
                Debug.Log("Message sent successfully");
            }
        });

        ClearTextField();
    }

/*    public void SendMessageAsync()
    {
        if (string.IsNullOrEmpty(MessageInputField.text))
        {
            return;
        }
        Debug.Log(playerId);
        VivoxService.Instance.SendDirectTextMessageAsync(playerId, MessageInputField.text);
        MessageInputField.text = string.Empty;
    }*/

    void ClearTextField()
    {
        MessageInputField.text = string.Empty;
        MessageInputField.Select();
        MessageInputField.ActivateInputField();
    }

    public static string GetPrivateChannelName(string playerId1, string playerId2)
    {

        PlayerPrefs.SetString("otherPlayerId", playerId1);
        var sorted = new List<string> { playerId1, playerId2 };
        sorted.Sort(); // Assure un ordre stable
        _channelName = $"private_{sorted[0]}_{sorted[1]}";
        return $"private_{sorted[0]}_{sorted[1]}";
    }


}
