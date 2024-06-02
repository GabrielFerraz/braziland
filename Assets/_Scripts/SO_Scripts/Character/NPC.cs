using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class NPC : MonoBehaviour
{
    public CharacterData character;
    public int activeDay = 0;

    [Button("Test Interaction")]
    public void Interact()
    {
        FindObjectOfType<DialogController>().InitiateDialog(character.dialogData[activeDay], 0, true);
        FindObjectOfType<DialogController>().OnDialogStart.Raise();
    }
}
