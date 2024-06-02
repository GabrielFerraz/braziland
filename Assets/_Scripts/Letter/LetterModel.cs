using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterModel : MonoBehaviour
{

    [SerializeField, NaughtyAttributes.ReadOnly]
    internal LetterData letter;

    public TextMeshProUGUI letterDescription;
    public Image letterIcon;

    [NaughtyAttributes.Button("Test Init Model")]
    public void InitModel()
    {
        int date = LetterSchedule.scheduler.currentDay;

        letterDescription.SetText("Delivered by: " + letter.author.characterName + '\n' + "Day: " + date);
        letterIcon.sprite = letter.author.mainProfile;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LetterSchedule.scheduler.currentSelectedLetter = this.letter;
            LetterSchedule.scheduler.ReadLetter();
        });
    }
}
