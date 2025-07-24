using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.Android;

public class LoginVivox : MonoBehaviour
{

    public string username = "dorian";
    public TextMeshProUGUI textMeshProUGUI;
    public UnityEvent UnityEvent;
    // Start is called before the first frame update
    bool invoke =false;
    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        LoginToVivox();
        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
        VivoxService.Instance.LoggedOut += LogoutOfVivoxServiceAsync;


    }

    void Update()
    {
        if (VivoxService.Instance.IsLoggedIn && !invoke)
        {
            UnityEvent.Invoke();
            invoke = true;
        }
    }
    async void OnUserLoggedIn()
    {
        Debug.Log("OnUserLoggedIn triggered");
        try
        {
            await JoinLobbyChannel();
            Debug.Log("Successfully joined the lobby channel.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to join channel: {e.Message}");
        }
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
            Debug.Log($"Attempting to join channel: {VivoxVoiceManager.LobbyChannelName}");
            return Task.CompletedTask; /*VivoxService.Instance.JoinGroupChannelAsync(
                VivoxVoiceManager.LobbyChannelName,
                ChatCapability.TextOnly
            );*///normalement on join ici mais ne fonctionne pas en build android (join dans chattextvivox a la place)
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


        /*await VivoxVoiceManager.Instance.InitializeAsync(username);
        var loginOptions = new LoginOptions()
        {
            DisplayName = username,
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.TenPerSecond,
        };*/

        // Initialiser Vivox correctement
        Debug.Log("Initializing Vivox...");
        await VivoxVoiceManager.Instance.InitializeAsync(username);
        Debug.Log("Vivox initialized successfully");

        // Login
        var loginOptions = new LoginOptions()
        {
            DisplayName = username,
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.TenPerSecond,
        };

        await VivoxService.Instance.LoginAsync(loginOptions);
        Debug.Log("Vivox login successful");
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
