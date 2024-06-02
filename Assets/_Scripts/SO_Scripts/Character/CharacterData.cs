using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Chaaracter", fileName = "Character")]
public class CharacterData : ScriptableObject
{
    public Sprite mainProfile; // used for letter et al. 
    public enum CharacterType
    {
        NPC, Player
    }
    public CharacterType characterType;

    public string characterName;

    [ShowIf("IsNpc")]
    public List<DialogData> dialogData;
    public bool IsNpc() => characterType == CharacterType.NPC;

    [ShowIf("IsNpc")]
    public bool satisfiedAll; // satisfied all likes. 

    [System.Serializable]
    public class CharacterTaste
    {
        public InventoryItem likedItem;
        public bool isGiven;
    }

    // list of stuff they like. 
    [ShowIf("IsNpc"), AllowNesting]
    public List<CharacterTaste> characterLikes;
    public void DoSatisfyItem(InventoryItem _item)
    {
        foreach (var item in characterLikes)
        {
            if (_item == item.likedItem)
            {
                item.isGiven = true;
            }
        }

        if (CheckAll())
        {
            Debug.Log("Raise character satisfied");
        }
    }
    bool CheckAll()
    {
        for (int i = 0; i < characterLikes.Count; i++)
        {
            if (!characterLikes[i].isGiven)
                return false;
        }
        return true;
    }
}

