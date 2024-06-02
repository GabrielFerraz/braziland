using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortraitEmotions { NORMAL, HAPPY, SAD, ANGRY, DETERMINED, TERRIFIED }

[CreateAssetMenu(fileName = "Speaker Data", menuName = "Dialogues/Speaker Data")]
public class SpeakerData : ScriptableObject
{
    public string Name;
    public PortraitEmotion portraitEmotions;
}

[System.Serializable]
public class PortraitEmotion
{
    // doing this because it'll be clearer for now. 
    public Sprite Default;
    public Sprite Happy;
    public Sprite Sad;
    public Sprite Angry;
    public Sprite Determined;
    public Sprite Terrified;
}

