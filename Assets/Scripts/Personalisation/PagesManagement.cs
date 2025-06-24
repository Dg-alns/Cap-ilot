using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PagesManagement : MonoBehaviour
{
    string[] allNamepages = new string[3];
    GameObject[] allpages = new GameObject[3];
    List<PartOfBody> pages = new List<PartOfBody>();

    public Personalisation personalisation;

    public GameObject but1;
    public GameObject but2;
    public GameObject but3;

    public TextMeshProUGUI Namebut1;
    public TextMeshProUGUI Namebut2;
    public TextMeshProUGUI Namebut3;

    public string CurrentPage;

    Color currentColorPage = Color.red;
    Color classicColorPage = Color.black;

    void Start()
    {
        foreach (var prt in Playerpart.AllParOfPlayer)
        {
            pages.Add(prt.Key);
        }

        allNamepages[0] = pages[0].ToString();
        allNamepages[1] = pages[1].ToString();
        allNamepages[2] = pages[2].ToString();

        allpages[0] = but1;
        allpages[1] = but2;
        allpages[2] = but3;

        CurrentPage = allNamepages[0];
        ChangePages();

        personalisation.LoadSecondPArt();
        personalisation.UpdateGrillPersonalisation();

        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2((Screen.width) / 4f, gridLayout.cellSize.y);
    }

    void ChangeColorCurrentPage()
    {
        for(int i = 0; i < allpages.Length; i++)
        {
            if(allNamepages[i] == CurrentPage)
            {
                if (allpages[i].GetComponent<Image>().color != currentColorPage)
                    allpages[i].GetComponent<Image>().color = currentColorPage;

                continue;
            }

            else
            {
                if (allpages[i].GetComponent<Image>().color != classicColorPage)
                    allpages[i].GetComponent<Image>().color = classicColorPage;
            }
        }
    }

    void ChangePages()
    {
        Namebut1.text = allNamepages[0];
        Namebut2.text = allNamepages[1];
        Namebut3.text = allNamepages[2];

        ChangeColorCurrentPage();
    }

    public void SelectPage(TextMeshProUGUI txt)
    {
       CurrentPage = txt.text;
       ChangeColorCurrentPage();

       personalisation.UpdateGrillPersonalisation();
       personalisation.ChangeOfAnyPart();
    }

    public void NextPage()
    {
        allNamepages[0] = allNamepages[1];
        allNamepages[1] = allNamepages[2];

        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i].ToString() == allNamepages[2] )
            {
                if(i + 1 < pages.Count)
                    allNamepages[2] = pages[i + 1].ToString();
                else
                    allNamepages[2] = pages[0].ToString();

                break;
            }
        }
        ChangePages();
    }
}
