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
        VivoxService.Instance.ChannelJoined += OnChannelJoined;
        VivoxService.Instance.DirectedMessageReceived += OnDirectedMessageReceived;
        VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;
        //VivoxService.Instance.ChannelMessageEdited += OnChannelMessageEdited;
        //VivoxService.Instance.ChannelMessageDeleted += OnChannelMessageDeleted;

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
        //FetchMessages = FetchHistory(true);
    }

   /* private async Task FetchHistory(bool scrollToBottom = false)
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
    }*/

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
        if (string.IsNullOrEmpty(MessageInputField.text))
        {
            return;
        }

        Debug.Log(VivoxService.Instance);
        Debug.Log(VivoxVoiceManager.LobbyChannelName);
        Debug.Log(MessageInputField.text);
        VivoxService.Instance.SendChannelTextMessageAsync(VivoxVoiceManager.LobbyChannelName, MessageInputField.text);
        ClearTextField();
    }

    void ClearTextField()
    {
        MessageInputField.text = string.Empty;
        MessageInputField.Select();
        MessageInputField.ActivateInputField();
    }

    /*
        async void InitializeAsync()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            await VivoxService.Instance.InitializeAsync();
        }

        public async void LoginToVivoxAsync()
        {
            LoginOptions options = new LoginOptions();
            options.DisplayName = "TEST";// UserDisplayName;
            options.EnableTTS = true;
            await VivoxService.Instance.LoginAsync(options);
        }

        public async void JoinEchoChannelAsync()
        {
            string channelToJoin = "Lobby";
            await VivoxService.Instance.JoinEchoChannelAsync(channelToJoin, ChatCapability.TextAndAudio);
        }

        public async void LeaveEchoChannelAsync()
        {
            string channelToLeave = "Lobby";
            await VivoxService.Instance.LeaveChannelAsync(channelToLeave);
        }

        public async void LogoutOfVivoxAsync()
        {
            await VivoxService.Instance.LogoutAsync();
        }

        private void BindSessionEvents(bool doBind)
        {
            if (doBind)
            {
                VivoxService.Instance.ParticipantAddedToChannel += onParticipantAddedToChannel;
                VivoxService.Instance.ParticipantRemovedFromChannel += onParticipantRemovedFromChannel;
            }
            else
            {
                VivoxService.Instance.ParticipantAddedToChannel -= onParticipantAddedToChannel;
                VivoxService.Instance.ParticipantRemovedFromChannel -= onParticipantRemovedFromChannel;
            }
        }

        private void onParticipantAddedToChannel(VivoxParticipant participant)
        {
            ///RosterItem is a class intended to store the participant object, and reflect events relating to it into the game's UI.
            ///It is a sample of one way to use these events, and is detailed just below this snippet.
            RosterItem newRosterItem = new RosterItem();
            newRosterItem.SetupRosterItem(participant);
            rosterList.Add(newRosterItem);
        }

        private void onParticipantRemovedFromChannel(VivoxParticipant participant)
        {
            RosterItem rosterItemToRemove = rosterList.FirstOrDefault<RosterItem>(p => p.Participant.PlayerId == participant.PlayerId);//FirstOrDefault(p => p.Participant.PlayerId == participant.PlayerId);
            rosterList.Remove(rosterItemToRemove);
        }

        private async void SendMessageAsync(string channelName, string message)
        {
            await VivoxService.Instance.SendChannelTextMessageAsync(channelName, message);
        }


        private void onChannelMessageReceived(VivoxMessage message)
        {
            string messageText = message.MessageText;
            string senderID = message.SenderPlayerId;
            string senderDisplayName = message.SenderDisplayName;
            string messageChannel = message.ChannelName;
        }*/
}
