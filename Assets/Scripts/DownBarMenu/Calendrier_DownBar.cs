using System;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] TextMeshProUGUI textMonthYear;

    private List<List<Image>> calenderRow;

    [SerializeField] List<Image> Row1;
    [SerializeField] List<Image> Row2;
    [SerializeField] List<Image> Row3;
    [SerializeField] List<Image> Row4;
    [SerializeField] List<Image> Row5;
    [SerializeField] List<Image> Row6;

    private DateTime calenderDateTime;
    private string month_year;

    // Start is called before the first frame update
    void Start()
    {
        calenderRow = new List<List<Image>>() { Row1, Row2, Row3, Row4, Row5, Row6 };

        calenderDateTime = DateTime.Now;

        ChangeShowingDate();

        CalendarUpdate();

    }

    public void ChangeCalenderDate(int addMonth)
    {
        calenderDateTime = calenderDateTime.AddMonths(addMonth);

        ChangeShowingDate();

        CalendarUpdate();
    }
    void ChangeShowingDate()
    {
        string[] datenow = calenderDateTime.ToLongDateString().Split(" ");
        month_year = datenow[2] + " - " + datenow[3];
        textMonthYear.text = month_year;
    }
    void CalendarUpdate()
    {
        // Get information of the month (first day, nb day, last day)
        DateTime startOfMonth = new DateTime(calenderDateTime.Year, calenderDateTime.Month, 1);
        int DaysInMonth       = DateTime.DaysInMonth(calenderDateTime.Year, calenderDateTime.Month);
        DateTime lastDay      = new DateTime(calenderDateTime.Year, calenderDateTime.Month, DaysInMonth);

        // Change the color of the first row 
        string day = startOfMonth.ToLongDateString().Split(" ")[0];
        for (int i = 0; i < 7; i++)
        {
            // i + 1 --> There is minimum one active in the first row 
            calenderRow[0][i].color = i+1 < jour_value[day] ? colorActiveCalender[false] : colorActiveCalender[true];
        }

        day = lastDay.ToLongDateString().Split(" ")[0];
        if(jour_value[day] <= 2 && jour_value[startOfMonth.ToLongDateString().Split(" ")[0]] >= 6)
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
    public void ResetCalender()
    {
        calenderDateTime = DateTime.Now;

        ChangeShowingDate();

        CalendarUpdate();
    }

}
