using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualWinning : MonoBehaviour
{
    public string loadingScene = "";
    public void ChangeScene()
    {
        SceneManager.LoadScene(loadingScene);
    }

    public void HideUI()
    {
        GetComponent<Animator>().SetBool("TEST",false);
    }
}
