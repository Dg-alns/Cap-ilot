using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginVivox : MonoBehaviour
{

    public string username;
    // Start is called before the first frame update
    void Start()
    {
        LoginToVivox();
        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
    }

    async void OnUserLoggedIn()
    {
        await JoinLobbyChannel();
    }

    async void LogoutOfVivoxServiceAsync()
    {

        await VivoxService.Instance.LogoutAsync();
        AuthenticationService.Instance.SignOut();
    }

    Task JoinLobbyChannel()
    {
        return VivoxService.Instance.JoinGroupChannelAsync(VivoxVoiceManager.LobbyChannelName, ChatCapability.TextOnly);
    }

    async void LoginToVivox()
    {

        await VivoxVoiceManager.Instance.InitializeAsync(username);
        var loginOptions = new LoginOptions()
        {
            DisplayName = username,
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.FivePerSecond
        };
       
        await VivoxService.Instance.LoginAsync(loginOptions);
    }

    void OnDestroy()
    {
        VivoxService.Instance.LoggedIn -= OnUserLoggedIn;
    }
}
