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

    public Button btnLetter;
    [NaughtyAttributes.Button("Test Init Model")]
    public void InitModel()
    {
        btnLetter.onClick.AddListener(() =>
        {
            LetterSchedule.scheduler.currentSelectedLetter = this.letter;
            LetterSchedule.scheduler.ReadLetter();

            letter.isRead = true;
        });
        int date = LetterSchedule.scheduler.currentDay;

        letterDescription.SetText("Delivered by: " + letter.author.characterName + '\n' + "Day: " + (date + 1));
        letterIcon.sprite = letter.author.mainProfile;
    }
}
