using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NextSceneDestination", menuName = "ScriptableObjects/NextSceneDestination")]
public class NextSceneDestination : ScriptableObject
{

    [SerializeField] private string _sceneName = "";
    [SerializeField] private string _CurrentsceneName = "";
    private string _ileNamePort = "";

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
        return _sceneName;
    }

    public IEnumerator NextScene(string scene)
    {
        SetNextSceneDestination(scene);
        SetCurrentScene(SceneManager.GetActiveScene().name);

        AsyncOperation operation =  SceneManager.LoadSceneAsync(_sceneName);
        //operation.allowSceneActivation = false;
        while (operation.isDone == false)
        {
            yield return null;
        }
        //SceneManager.LoadScene(_sceneName);
    }

    public void eee(string scene)
    {
        SetNextSceneDestination(scene);
        SetCurrentScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(_CurrentsceneName);
    }

    public void LoadNewIle()
    {
        SceneManager.LoadScene(_ileNamePort);
    }

}