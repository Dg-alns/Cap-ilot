using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Calendrier_DownBar : MonoBehaviour
{
    Dictionary<bool, Color> colorActiveCalender = new Dictionary<bool, Color>()
    {
        {true, Color.white},
        {false, new Color(0.1f,0.1f,0.1f) },
    };
    Dictionary<string, int> jour_value = new Dictionary<string, int>()
    {
        {"lundi"    ,1 },
        {"mardi"    ,2 },
        {"mercredi" ,3 },
        {"jeudi"    ,4 },
        {"vendredi" ,5 },
        {"samedi"   ,6 },
        {"dimanche" ,7 }
    }; 

    Dictionary<string,Dictionary<string,Dictionary<string,string>>> OrderedJournal = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

    [SerializeField] TextMeshProUGUI textMonthYear;

    private List<List<Image>> calenderRow;

    [Header("Rows")]
    [SerializeField] List<Image> Row1;
    [SerializeField] List<Image> Row2;
    [SerializeField] List<Image> Row3;
    [SerializeField] List<Image> Row4;
    [SerializeField] List<Image> Row5;
    [SerializeField] List<Image> Row6;

    [Header("CurrentDay")]
    [SerializeField] Image currentDay;

    [Header("Journal Info")]
    [SerializeField] TextMeshProUGUI themeText;
    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField] TextMeshProUGUI emotionText;

    private DateTime TodayDateTime;
    private DateTime calenderDateTime;
    private string month_year;

    private int firstMonthDay;
    private int DaysInMonth;

    private Saving save;

    // Start is called before the first frame update
    void Start()
    {
        string jsonstring = File.ReadAllText("save.json");
        save = JsonUtility.FromJson<Saving>(jsonstring);

        OrderSave();

        calenderRow = new List<List<Image>>() { Row1, Row2, Row3, Row4, Row5, Row6 };

        TodayDateTime = DateTime.Today;

        calenderDateTime = DateTime.Now;

        UpdateCalender();
    }

    private void OrderSave()
    {
        foreach (var item in save.journal.journal)
        {
            string[] date = item.Key.Split("/");

            string day = date[0];
            string month = date[1];
            string year = date[2];

            //OrderedJournal[date[2]][date[1]] = {date[0], item.Value};
            if (!OrderedJournal.ContainsKey(year))
                OrderedJournal.Add(year, new Dictionary<string, Dictionary<string, string>>());

            if (!OrderedJournal[year].ContainsKey(month))
                OrderedJournal[year].Add(month, new Dictionary<string, string>());

            OrderedJournal[year][month].Add(day, item.Value);
        }
    }

    void UpdateCalender()
    {
        ChangeShowingDate();

        ResetCalenderCase();

        UpdateCaseCalender();

        TryPlaceCurrentDay();

        TryShowSavedJournal();
    }

    private void ResetCalenderCase()
    {
        // Reset all Case in white
        foreach (var Row in calenderRow)
        {
            foreach (var calenderCase in Row)
            {
                calenderCase.color = Color.white;
                calenderCase.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }

    private void TryShowSavedJournal()
    {
        string[] date = calenderDateTime.ToString("d").Split("/");
        string day = date[0];
        string month = date[1];
        string year = date[2];

        if (!OrderedJournal.ContainsKey(year))
            return;


        if (!OrderedJournal[year].ContainsKey(month))
            return;

        foreach(var item in OrderedJournal[year][month])
        {

            int today = int.Parse(item.Key);

            int num = today + firstMonthDay - 1 - 1;

            calenderRow[num / 7][(num) % 7].color = Color.green;
            calenderRow[num / 7][(num) % 7].GetComponent<Button>().onClick.AddListener(() => { OnClickCaseCalender(item.Value); });
        }
    }

    void ChangeShowingDate()
    {
        string[] datenow = calenderDateTime.ToLongDateString().Split(" ");
        month_year = datenow[2] + " - " + datenow[3];
        textMonthYear.text = month_year;
    }

    void UpdateCaseCalender()
    {
        // Get information of the month (first day, nb day, last day)
        DateTime startOfMonth = new DateTime(calenderDateTime.Year, calenderDateTime.Month, 1);
        DaysInMonth = DateTime.DaysInMonth(calenderDateTime.Year, calenderDateTime.Month);
        DateTime lastDay = new DateTime(calenderDateTime.Year, calenderDateTime.Month, DaysInMonth);

        // Change the color of the first row 
        string day = startOfMonth.ToLongDateString().Split(" ")[0];

        firstMonthDay = jour_value[day];

        for (int i = 0; i < 7; i++)
        {
            // i + 1 --> There is minimum one active in the first row 
            bool condition = i + 1 < jour_value[day];
            calenderRow[0][i].color = condition ? colorActiveCalender[false] : colorActiveCalender[true];
        }

        day = lastDay.ToLongDateString().Split(" ")[0];
        if (jour_value[day] <= 2 && jour_value[startOfMonth.ToLongDateString().Split(" ")[0]] >= 6)
        {
            for (int i = 0; i < 7; i++)
            {
                calenderRow[4][i].color = colorActiveCalender[true];
                calenderRow[5][i].color = i >= jour_value[day] ? colorActiveCalender[false] : colorActiveCalender[true];

            }
            return;
        }
        for (int i = 0; i < 7; i++)
        {
            calenderRow[4][i].color = i >= jour_value[day] ? colorActiveCalender[false] : colorActiveCalender[true];
            calenderRow[5][i].color = colorActiveCalender[false];
        }
    }

    private void TryPlaceCurrentDay()
    {
        if(TodayDateTime.Month != calenderDateTime.Month)
        {
            currentDay.gameObject.SetActive(false);
            return;
        }
        currentDay.gameObject.SetActive(true);

        int day = TodayDateTime.Day; 

        int num = day + firstMonthDay - 1 - 1; 

        Vector3 positionCurrentDay = calenderRow[num/7][(num)%7].transform.position;

        currentDay.transform.position = positionCurrentDay;
    }

    public void ChangeCalenderDate(int addMonth)
    {
        calenderDateTime = calenderDateTime.AddMonths(addMonth);

        UpdateCalender();
    }
   
    public void Calender_GoForToday()
    {
        calenderDateTime = DateTime.Now;

        UpdateCalender();
    }

    public void OnClickCaseCalender(string journalContentSaved)
    {
        string[] data = journalContentSaved.Split("\n");

        string theme = data[0];
        string emotion = data[1];
        string content = "";

        for (int i = 2; i < data.Length; i++)
        {
            content += data[i] + "\n";
        }

        themeText.text = "Thème : " + theme;
        emotionText.text = emotion;
        contentText.text = content;
    }

    public void IsAddingJournalContent(string totalContent)
    {
        TodayDateTime = DateTime.Today;

        string[] date = TodayDateTime.ToString("d").Split("/");
        string day = date[0];
        string month = date[1];
        string year = date[2];

        if (!OrderedJournal.ContainsKey(year))
            OrderedJournal.Add(year, new Dictionary<string, Dictionary<string, string>>());

        if (!OrderedJournal[year].ContainsKey(month))
            OrderedJournal[year].Add(month, new Dictionary<string, string>());

        if (!OrderedJournal[year][month].ContainsKey(day))
            OrderedJournal[year][month].Add(day, totalContent);
        else
            OrderedJournal[year][month][day] = totalContent;

        TryShowSavedJournal();
    }
}
