using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortManager : MonoBehaviour
{
    public GameObject Player;
    public LoadNexScene loadNexScene;

    List<GameObject> allPorts = new List<GameObject>();

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Archipel"))
        {
            allPorts = Tools.CreateGameObjectList<AccesToPort>(gameObject.name);

            for (int i = 0; i < allPorts.Count; i++)
            {
                if (allPorts[i].GetComponent<AccesToPort>().port.CanGoToIle == false)
                    allPorts[i].SetActive(false);
            }

            PositionBateau();
        }
    }

    void PositionBateau()
    {
        for (int i = 0; i < allPorts.Count; i++)
        {
            if (allPorts[i].activeSelf)
            {
                if (loadNexScene.GetPreviousSceneName().Contains(allPorts[i].name))
                {
                    Player.transform.position = new(allPorts[i].transform.position.x, allPorts[i].transform.position.y, 0);
                    Player.SetActive(true);
                }
            }
        }
    }
}
