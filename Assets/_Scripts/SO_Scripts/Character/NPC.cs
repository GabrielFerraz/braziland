using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class NPC : MonoBehaviour, IInteractable
{
    public CharacterData character;
    public int activeDay;

    [Button("Test Interaction")]
    public void Interact()
    {
        Debug.Log("Interact with Sam"); 
        activeDay = LetterSchedule.scheduler.currentDay;

        FindObjectOfType<DialogController>().InitiateDialog(character.dialogData[activeDay], 0, true);
        FindObjectOfType<DialogController>().OnDialogStart.Raise();
    }
}
