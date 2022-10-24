using UnityEngine;
using UnityEngine.UI;


public class NotebookItemSlot : MonoBehaviour
{
    public delegate void OnClickEvent(string id);
    public static event OnClickEvent onClickEvent;

    [HideInInspector] public Image icon;
    [HideInInspector] public NotebookItem item;

    public void Set(NotebookItem item)//set notebook item.
    {
        this.item = item;
        icon.sprite = item.data.icon;
        
    }
    public void OnClick()//item clicked on UI.
    {
        onClickEvent?.Invoke(item.data.id);
    }  
}
