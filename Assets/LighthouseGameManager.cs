using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<DragDrop,int> lighthouse = new Dictionary<DragDrop, int>();
    public int _part;

    [SerializeField] private GameObject _visualWinning;
    [SerializeField] private GameObject _scrollRect;

    //private bool _win;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Check(){
        if(_part == lighthouse.Count)
        {
            foreach (var d in lighthouse) { 
                if(d.Key.floor != d.Value)
                {
                    return;
                }


            }
            /*for (int i = 0; i < lighthouse.Count; i++) {
                if (lighthouse[i].floor != i)
                {
                    Debug.Log(i);
                    return;
                }
            }    */

            //_win =true;
            Debug.Log("normalement c gagné");
            PlayerPrefs.SetInt("LightHouseRepaired", 1);
            _visualWinning.SetActive(true);
            _visualWinning.GetComponent<RectTransform>().SetAsLastSibling();
            _visualWinning.GetComponent<Animator>().SetBool("TEST",true);

            _scrollRect.SetActive(false);
        }
    }
}
