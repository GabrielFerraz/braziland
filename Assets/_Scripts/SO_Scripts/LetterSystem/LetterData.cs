using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Letter", fileName = "Letter")]
public class LetterData : ScriptableObject
{
    public CharacterData author; // used to quickly navigate stuff like profile and others. 

    [TextArea(5, 5)]
    public List<string> letterContent; // 0 for intro, 1 for continuation? 

    public List<string> personalityCues; // to build a list of personality cues. 

    public bool isSpriteContent;

    [NaughtyAttributes.ShowIf("isSpriteContent")]
    public List<Sprite> letterContentSprite;

    public bool isDelivered;
    public bool isRead;

}
