using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class LetterSchedule : MonoBehaviour
{
    public static LetterSchedule scheduler;
    public List<LetterData> letters;

    public List<LetterDelivery> letterDelivery;

    [System.Serializable]
    public class LetterDelivery
    {
        public LetterData toDeliver;
        public int dateToDeliver;
    }

    [SerializeField, ReadOnly]
    private List<LetterData> deliveredLetters; // letters delivered. 

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
    public Image contentImage;
    public TextMeshProUGUI authorNPC;
    public TextMeshProUGUI readingDate;
    public Transform profileParent;

    public bool isSpriteContent;

    public GameObject mailBoxNotif;
    public GameObject letterInMail;

    public TextMeshProUGUI dayText; // this shouldn't be here, but in Day Cycle's script. 


    [Header("Game Events")]
    public GameEvent @MailboxAccess;

    private void Awake()
    {
        scheduler = this;
    }

    private void Start()
    {
        dayText.SetText("Day \n" + (currentDay + 1)); 
        DeliverDailyLetters();
        @MailboxAccess.OnRaise.AddListener((x) => OpenMailBox());
    }
    private void OnDestroy()
    {
        @MailboxAccess.OnRaise.RemoveAllListeners();
    }

    [Button("Test Next day")]
    public void GoToNextDay()
    {
        ++currentDay;
        dayText.SetText("Day \n" + (currentDay + 1));
        DeliverDailyLetters();
        if (CheckAllRead() && mailBoxNotif != null)
        {
            mailBoxNotif.SetActive(false);
            letterInMail.SetActive(false);
        }
        else
        {
            mailBoxNotif.SetActive(true);
            letterInMail.SetActive(false);
        }
    }

    public void DeliverDailyLetters()
    {
        //var dailyLetter = letters[currentDay];
        var dailyLetter = letterDelivery.Find(x => x.dateToDeliver == currentDay);
        if (dailyLetter == null) return;

        var letter = Instantiate(letterModelPrefab, mailBox).GetComponent<LetterModel>();

        letter.letter = dailyLetter.toDeliver;
        letter.InitModel();

        deliveredLetters.Add(dailyLetter.toDeliver);
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

        if (!isSpriteContent)
        {
            // we should only have 2 content for now. since we use magic numbers. 
            contentText.SetText(currentSelectedLetter.letterContent[1]);
        }
        else
        {
            contentImage.sprite = currentSelectedLetter.letterContentSprite[0];
        }
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

        if (!isSpriteContent)
            contentText.SetText(currentSelectedLetter.letterContent[0]);
        else
        {
            letterReadContent.SetActive(false);
            NextContent();
        }
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
        {
            mailBoxNotif.SetActive(false);
            letterInMail.SetActive(false);
        }
        else
        {
            mailBoxNotif.SetActive(true);
            letterInMail.SetActive(true);
        }
    }

    public bool CheckAllRead()
    {
        foreach (var letter in deliveredLetters)
        {
            if (!letter.isRead)
                return false;
        }
        return true;
    }
}
