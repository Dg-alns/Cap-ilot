using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginVivox : MonoBehaviour
{

    public string username = "dorian";
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        LoginToVivox();
        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
    }

    async void OnUserLoggedIn()
    {
        Debug.Log("OnUserLoggedIn triggered"); // <- VOIS-TU CECI DANS LOGCAT ?
        await JoinLobbyChannel();
        Debug.Log("Joined channel"); // <- EST-CE QUE TU LA VOIS ?
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
    }

    public void ShowUsername()
    {
        textMeshProUGUI.text = username;
    }
}
