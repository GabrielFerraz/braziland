using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class LetterSchedule : MonoBehaviour
{
    public static LetterSchedule scheduler;
    public List<LetterData> letters;

    public LetterData currentSelectedLetter;
    public GameObject letterModelPrefab;

    public int currentDay;

    [Header("MAIL BOX SECTION")]
    public Transform mailBox;
    public Transform MailPanel;

    [Header("READ LETTER SECTION")]
    public GameObject personality;
    public GameObject letterReadContent;

    private void Awake()
    {
        scheduler = this;
    }

    [Button("Test Next day")]
    public void GoToNextDay()
    {
        ++currentDay;
        DeliverDailyLetters();
    }

    public void DeliverDailyLetters()
    {
        var dailyLetter = letters[currentDay];
        var letter = Instantiate(letterModelPrefab, mailBox).GetComponent<LetterModel>();

        letter.letter = dailyLetter;
        letter.InitModel();
    }

    /// <summary>
    /// Init letter read and bring up necessary UIs for it. 
    /// </summary>
    internal void ReadLetter()
    {
    }

    /// <summary>
    /// Called to go to next content if any
    /// </summary>
    public void NextContent()
    {

    }


    /// <summary>
    /// called after reading personality. 
    /// </summary>
    public void ToContent()
    {

    }


    [Button]
    public void OpenMailBox()
    {
        MailPanel.gameObject.SetActive(true);
    }
}
