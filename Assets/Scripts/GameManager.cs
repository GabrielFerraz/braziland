using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TileManager tileManager;


    private void Awake()
    {
        tileManager = GetComponent<TileManager>();
    }
}
