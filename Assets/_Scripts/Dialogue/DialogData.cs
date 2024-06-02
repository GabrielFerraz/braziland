using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Dialog Data", menuName = "Dialogues/Dialog Data")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public struct Conversation
    {
        public List<Dialogue> dialogues;

        [ShowIf("isVoiceOver")]
        public bool waitForVoice;
    }


    public List<Conversation> conversationList;

    [System.Serializable]
    public class Dialogue
    {
        //public enum expression
        //{
        //    DEFAULT, ANGRY, SAD, HAPPY
        //}
        public SpeakerData speaker;
        [TextArea(3, 4)] public string line;

        public AudioClip barkSound;

        public PortraitEmotions dialogExpression;

        public bool isVoiceOver;

        [ShowIf("isVoiceOver")]
        public AudioClip voice;
    }
}
