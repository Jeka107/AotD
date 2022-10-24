using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemLabel;
    [SerializeField] private GameObject stackObj;
    [SerializeField] private TextMeshProUGUI stackLabel;
    [SerializeField] private TextMeshProUGUI slotNum;
    [SerializeField] private GameObject selected;
    [HideInInspector] public InventoryItem item;
    public void Set(InventoryItem item) //set item on slot.
    {
        this.item = item;
        icon.sprite = item.data.icon;
        itemLabel.text = item.data.displayName;
        slotNum.text = item.slotNum.ToString();

        if (item.stackSize<=1)
        {
            stackObj.SetActive(false);
            return;
        }
        else
        {
            stackObj.SetActive(true);
        }

        stackLabel.text = item.stackSize.ToString();
    }
    public void SetNumberSlot(int num)
    {
        slotNum.text = num.ToString();
    }
    public void ActivateSelected()//item selected.
    {
        selected.SetActive(true);
    }
    public void DeActivateSelected()//differnt item selected.
    {
        selected.SetActive(false);
    }
}
