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


public class ChatTextVivox : MonoBehaviour
{

    List<RosterItem> rosterList = new List<RosterItem>();

    private IList<KeyValuePair<string, MessageObject>> m_MessageObjPool = new List<KeyValuePair<string, MessageObject>>();

    public GameObject ChatContentObj;
    public GameObject MessageObject;

    ScrollRect m_TextChatScrollRect;
    public TMP_InputField MessageInputField;
    
    private Task FetchMessages = null;
    private DateTime? oldestMessage = null;

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

        Debug.Log("Vivox ready, setting up ChatTextVivox");
        VivoxService.Instance.JoinGroupChannelAsync(
                VivoxVoiceManager.LobbyChannelName,
                ChatCapability.TextOnly
            );
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
        FetchMessages = FetchHistory(true);
    }
    public void History()
    {
        if(VivoxService.Instance.IsLoggedIn){

            FetchMessages = FetchHistory(false);
        }
    }

    private async Task FetchHistory(bool scrollToBottom = false)
    {
        try
        {
            var chatHistoryOptions = new ChatHistoryQueryOptions()
            {
                TimeEnd = oldestMessage
            };
            var historyMessages =
                await VivoxService.Instance.GetChannelTextMessageHistoryAsync(VivoxVoiceManager.LobbyChannelName, 10,
                    chatHistoryOptions);
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
            VivoxVoiceManager.LobbyChannelName,
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

    void ClearTextField()
    {
        MessageInputField.text = string.Empty;
        MessageInputField.Select();
        MessageInputField.ActivateInputField();
    }

}
