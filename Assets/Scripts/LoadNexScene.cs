using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNexScene : MonoBehaviour
{
    [SerializeField] private NextSceneDestination _NextSceneData;

    [SerializeField] private Energy _energy;

    public Animator animator;

    [Header ("\nJust for Scene Accueil")]
    [SerializeField] private Tools tools;
    [Header ("Not Mandatory\nForSavePosition")]
    public GameObject Player;

    private void Start()
    {
        _NextSceneData.isLauch = false;
    }

    public void StartGame()
    {        
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            animator.SetTrigger("Transition");
            DeletPos();
            tools.DontDestroyTools();
            string scene = PlayerPrefs.HasKey("SceneName") ? PlayerPrefs.GetString("SceneName") : "Personalisation";
            StartCoroutine(_NextSceneData.NextScene(scene));
        }
    }

    public void NextScenePersonalisation(PlayerSpriteManager playerSpriteManager)
    {
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            animator.SetTrigger("Transition");

            playerSpriteManager.SavePersonalisation();
            string scene = _NextSceneData.GetPreviousScene().Length > 1 ? _NextSceneData.GetPreviousScene() : "Profile";
            StartCoroutine(_NextSceneData.NextScene(scene));
        }
    }

    public void LoadBoat(AccesToPort accesToPort)
    {
        if (_NextSceneData.isLauch == false)
        {
            if (accesToPort == null)
            {
                _NextSceneData.isLauch = true;
                animator.SetTrigger("Transition");
                StartCoroutine(_NextSceneData.SwitchScene("Archipel"));
            }
            else
            {
                accesToPort.LoadScene();
            }
        }
    }

    public void LoadNextScene(string scene)
    {        
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            animator.SetTrigger("Transition");
            StartCoroutine(_NextSceneData.SwitchScene(scene));
        }
    }

    public void LoadMiniGame(string scene)
    {
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            if (_energy.HaveEnergy())
            {
                SavePos();
                _energy.UseEnergy();
                animator.SetTrigger("Transition");
                StartCoroutine(_NextSceneData.SwitchScene(scene));
            }
        }
    }

    public void LoadPreviousScene()
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.SwitchScene(_NextSceneData.GetPreviousScene()));
    }

    public void LoadNewIle(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.MiniGameBoat(scene));
    }

    public void LoadIle()
    {
        if(_NextSceneData.isLauch == false)
        {
            animator.SetTrigger("Transition");
            StartCoroutine(_NextSceneData.NewIle());
            _NextSceneData.isLauch = true;
        }
    }

    public string GetPreviousSceneName() { return _NextSceneData.GetPreviousScene(); }


    private void SavePos()
    {
        PlayerPrefs.SetFloat("PosX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PosY", Player.transform.position.y);
        PlayerPrefs.SetFloat("RotateY", Player.transform.rotation.y);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
    }

    private void DeletPos()
    {
        PlayerPrefs.DeleteKey("PosX");
        PlayerPrefs.DeleteKey("PosY");
        PlayerPrefs.DeleteKey("RotateY");
        PlayerPrefs.DeleteKey("SceneName");
    }


    public bool GetBoatSpawn()
    {
        return _NextSceneData.GetPreviousScene() == "Archipel" || _NextSceneData.GetPreviousScene() ==  "MiniGame_Boat";
    }
}
