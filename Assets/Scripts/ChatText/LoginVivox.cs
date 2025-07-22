using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginVivox : MonoBehaviour
{

    public string username = "dorian";
    public TextMeshProUGUI textMeshProUGUI;
    public UnityEvent UnityEvent;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AH");
        LoginToVivox();
        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
        VivoxService.Instance.LoggedOut += LogoutOfVivoxServiceAsync;


    }

    void Update()
    {
        if (VivoxService.Instance.IsLoggedIn)
        {
            UnityEvent.Invoke();
        }
    }
    async void OnUserLoggedIn()
    {
        Debug.Log("OnUserLoggedIn triggered"); 
        await JoinLobbyChannel();
        Debug.Log("Joined channel"); 
    }

    async void LogoutOfVivoxServiceAsync()
    {

        await VivoxService.Instance.LogoutAsync();
        AuthenticationService.Instance.SignOut();
    }

    Task JoinLobbyChannel()
    {
        try
        {
            return VivoxService.Instance.JoinGroupChannelAsync(
                VivoxVoiceManager.LobbyChannelName,
                ChatCapability.TextOnly
            );
        }
        catch (Exception ex)
        {
            Debug.LogError($"JoinLobbyChannel failed: {ex.Message}");
            return Task.CompletedTask;
        }
        //return VivoxService.Instance.JoinGroupChannelAsync(VivoxVoiceManager.LobbyChannelName, ChatCapability.TextOnly);
    }

    async void LoginToVivox()
    {

        if (!Unity.Services.Core.UnityServices.State.Equals(Unity.Services.Core.ServicesInitializationState.Initialized))
        {
            await Unity.Services.Core.UnityServices.InitializeAsync();
        }


        await VivoxVoiceManager.Instance.InitializeAsync(username);
        var loginOptions = new LoginOptions()
        {
            DisplayName = username,
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.TenPerSecond,
        };
       
        await VivoxService.Instance.LoginAsync(loginOptions);
    }

    void OnDestroy()
    {
        VivoxService.Instance.LoggedIn -= OnUserLoggedIn;
        VivoxService.Instance.LoggedOut -= LogoutOfVivoxServiceAsync;
    }

    public void ShowUsername()
    {
        textMeshProUGUI.text = username;
    }
}
