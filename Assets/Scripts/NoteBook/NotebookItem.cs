using System;
using UnityEngine;

[Serializable]
public class NotebookItem
{
    public NotebookItemData data;

    public NotebookItem(NotebookItemData source)
    {
        data = source;
    }
    public void ItemAction()
    {
        data.DoAction();
    }
    public AudioClip GetClip()
    {
        return data.GetClip();
    }

}
