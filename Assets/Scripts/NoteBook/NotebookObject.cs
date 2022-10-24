using UnityEngine;

public class NotebookObject : MonoBehaviour
{
    [SerializeField] public NotebookItemData referenceItem;

    public void OnPickUp()//pick up notebook ite.
    {
        FindObjectOfType<NotebookSystem>().Add(referenceItem);//add the item to notebook.
        Destroy(gameObject);
    }
}
