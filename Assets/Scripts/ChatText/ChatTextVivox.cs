using System;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;


public class ChatTextVivox : MonoBehaviour
{
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
}
