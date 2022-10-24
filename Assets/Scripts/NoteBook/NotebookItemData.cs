using UnityEngine;

public class NotebookItemData : ScriptableObject
{
    public string id;
    public Sprite icon;
    public int noteTypeData;
    [HideInInspector] public string itemType;

    public virtual int DoAction() { return 0; }
    public virtual AudioClip GetClip() { return null; }
}
