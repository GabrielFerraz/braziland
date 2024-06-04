using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;

    private void Start()
    {
        //toolbarController.onChange += Highlighter.Highlight;
    }
    public override void OnClick(int id)
    {
        toolbarController.Set(id);

    }

}
