using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NextSceneDestination", menuName = "ScriptableObjects/NextSceneDestination")]
public class NextSceneDestination : ScriptableObject
{

    [SerializeField] private string _sceneName = "";
    [SerializeField] private string _CurrentsceneName = "";
    private string _ileNamePort = "";

    public bool isLauch = false;

    void SetNextSceneDestination(string sceneName)
    {
        _sceneName = sceneName;
    }
    void SetCurrentScene(string sceneName)
    {
        _CurrentsceneName = sceneName;
    }
    
    void SetIlePort(string sceneName)
    {
        _ileNamePort = sceneName;
    }

    public string GetNextSceneDestination()
    { 
        return _sceneName;
    }
    public string GetPreviousScene()
    {
        return _CurrentsceneName;
    }
    public string GetSceneIle()
    {
        return _ileNamePort;
    }

    public void LoadNewIle()
    {
        SceneManager.LoadScene(_ileNamePort);
    }

    public IEnumerator NextScene(string scene)
    {
        yield return null;
        SetNextSceneDestination(scene);
        SetCurrentScene(SceneManager.GetActiveScene().name);

        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1);
                isLauch = false;
                operation.allowSceneActivation = true;
            }
        }
        yield return null;
    }

    public IEnumerator MiniGameBoat(string scene)
    {
        yield return null;
        SetIlePort(scene);
        SetNextSceneDestination("MiniGame_Boat");
        SetCurrentScene(SceneManager.GetActiveScene().name);

        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1);
                isLauch = false;
                operation.allowSceneActivation = true;
            }
        }
        yield return null;
    }

    public IEnumerator NewIle()
    {
        yield return null;

        SetNextSceneDestination(_ileNamePort);
        SetCurrentScene(SceneManager.GetActiveScene().name);
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1);
                isLauch = false;
                operation.allowSceneActivation = true;
            }
        }
        yield return null;
    }

}