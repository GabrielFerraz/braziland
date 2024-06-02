using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterData : MonoBehaviour
{
    public CharacterData author; // used to quickly navigate stuff like profile and others. 

    [TextArea(5, 5)]
    public List<string> letterContent; // 0 for intro, 1 for continuation? 

    public List<string> personalityCues; // to build a list of personality cues. 

    public bool isDelivered;
    public bool isRead;

}
