using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    private void Start()
    {

            Init();
    }

    public void Init()
    {

        GameManager.instance.timeManager.Subscribe(this);

    }

    public void Invoke() 
    {

        onTimeTick?.Invoke();
    }

    private void OnDestroy() 
    {

        GameManager.instance.timeManager.Unsubscribe(this);
    }
}