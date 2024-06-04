using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        FindObjectOfType<PlayerController>().IsInteracting = true;
        // raise event for Mailbox UI. 
        LetterSchedule.scheduler.MailboxAccess.Raise();
        Debug.Log("Interacting with mail");
    }
}
