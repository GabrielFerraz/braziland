using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System;
using TMPro;

public class LetterSchedule : MonoBehaviour
{
    public static LetterSchedule scheduler;
    public List<LetterData> letters;

    [ReadOnly]
    public LetterData currentSelectedLetter;
    public GameObject letterModelPrefab;

    public int currentDay;

    [Header("MAIL BOX SECTION")]
    public Transform mailBox;
    public Transform MailPanel;

    [Header("READ LETTER SECTION")]
    public GameObject letterPanel;
    public GameObject personality;
    public GameObject letterReadContent;
    public Button nextContent;
    public Button endRead;

    [Header("Dynamic Areas")]
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI authorNPC;
    public TextMeshProUGUI readingDate;
    public Transform profileParent;


    public GameObject mailBoxNotif;

    private void Awake()
    {
        scheduler = this;

    }

    private void Start()
    {
        DeliverDailyLetters();
    }

    [Button("Test Next day")]
    public void GoToNextDay()
    {
        ++currentDay;
        DeliverDailyLetters();
        if (CheckAllRead() && mailBoxNotif != null)
            mailBoxNotif.SetActive(false);
        else
            mailBoxNotif.SetActive(true);
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
        MailPanel.gameObject.SetActive(false);
        letterPanel.SetActive(true);

        SetupPersonalityPage();
        personality.SetActive(true);

        // we don't do the personality thing so, 
        ToContent();
    }

    void SetupPersonalityPage()
    {
        authorNPC.SetText("Name: " + currentSelectedLetter.author.characterName +
            '\n' + "Species: " + currentSelectedLetter.author.species);

        readingDate.SetText("Day: " + (currentDay + 1));

        int count = profileParent.childCount;

        for (int i = 0; i < count; i++)
        {
            profileParent.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(
                currentSelectedLetter.personalityCues[i]);
        }
    }

    /// <summary>
    /// Called to go to next content if any
    /// </summary>
    public void NextContent()
    {
        nextContent.gameObject.SetActive(false);
        endRead.gameObject.SetActive(true);

        // we should only have 2 content for now. since we use magic numbers. 
        contentText.SetText(currentSelectedLetter.letterContent[1]);
    }


    /// <summary>
    /// called after reading personality. 
    /// </summary>
    public void ToContent()
    {
        personality.SetActive(false);
        endRead.gameObject.SetActive(false);

        nextContent.gameObject.SetActive(true);
        letterReadContent.SetActive(true);

        contentText.SetText(currentSelectedLetter.letterContent[0]);
    }


    [Button]
    public void OpenMailBox()
    {
        MailPanel.gameObject.SetActive(true);
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);
        MailPanel.gameObject.SetActive(true);


        // check if notice should remain up or note. 
        if (CheckAllRead() && mailBoxNotif != null)
            mailBoxNotif.SetActive(false);
        else
            mailBoxNotif.SetActive(true);
    }

    public bool CheckAllRead()
    {
        foreach (var letter in letters)
        {
            if (!letter.isRead)
                return false;
        }
        return true;
    }
}
