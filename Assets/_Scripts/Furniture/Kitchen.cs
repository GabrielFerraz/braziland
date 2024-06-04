using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        FindObjectOfType<SceneLoader>().LoadCookingScene();
    }
}
