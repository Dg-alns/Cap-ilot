using System;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using System.Collections.Generic;
using System.Linq;


public class ChatTextVivox : MonoBehaviour
{

    List<RosterItem> rosterList = new List<RosterItem>();
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
    }
}
