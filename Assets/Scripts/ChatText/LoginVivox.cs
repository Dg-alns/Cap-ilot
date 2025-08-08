using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.Android;
using TMPro;

public class LoginVivox : MonoBehaviour
{

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


        //LogoutOfVivoxServiceAsync();

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


    public async void LogoutOfVivoxServiceAsync()
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

/*        string jsonstring = File.ReadAllText("save.json");
        Saving save = JsonUtility.FromJson<Saving>(jsonstring);

        Debug.Log("Initializing Vivox...");

        save.profile.Username = save.profile.Username.Remove(save.profile.Username.Length - 1);*/
        await VivoxVoiceManager.Instance.InitializeAsync("Dorian");
        Debug.Log("Vivox initialized successfully");

        // Login
        var loginOptions = new LoginOptions()
        {
            DisplayName = "Dorian",
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
        //textMeshProUGUI.text = username;
    }
}
