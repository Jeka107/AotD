using System.Collections.Generic;
using UnityEngine;


public class NotebookSystem : MonoBehaviour
{
    public delegate void OnUpdateNoteBook(string id);
    public static event OnUpdateNoteBook OnChange;

    [SerializeField] public List<NotebookItem> notes;
    [SerializeField] public List<NotebookItem> tapes;
    [SerializeField] public GameObject noteImage1;
    [SerializeField] public GameObject noteImage2;
    [SerializeField] public GameObject noteImage3;

    private Dictionary<string, NotebookItem> itemDictionry;
    private AudioSource audio;

    private void Awake()
    {
        itemDictionry = new Dictionary<string, NotebookItem>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        NotebookItemSlot.onClickEvent += NotebookItemClicked;
    }
    private void OnDestroy()
    {
        NotebookItemSlot.onClickEvent -= NotebookItemClicked;
    }
    public NotebookItem Get(string dataId)//get all the inventory
    {
        if (itemDictionry.TryGetValue(dataId, out NotebookItem value))
        {
            return value;
        }
        return null;
    }
    public void Add(NotebookItemData data)//add item to inventory
    {
        NotebookItem newItem = new NotebookItem(data);
        if(newItem.data.itemType== ConstantsNames.NotebookItemTypes.Note)
        {
            notes.Add(newItem);
         
        }
        else if(newItem.data.itemType == ConstantsNames.NotebookItemTypes.Tape)
        {
            tapes.Add(newItem);
        }

        itemDictionry.Add(data.id, newItem);
        OnChange?.Invoke(newItem.data.itemType);
     
    }
    public void NotebookItemClicked(string dataId)
    {
        
        if (itemDictionry.TryGetValue(dataId, out NotebookItem value))
        {
            if (value.data.itemType == ConstantsNames.NotebookItemTypes.Tape)//if tape clicked.
            {
                if (!audio.isPlaying)//if no audio playing.
                {
                    audio.PlayOneShot(value.GetClip());//play audio on tape.
                }
               
            }
            else if (value.data.itemType == ConstantsNames.NotebookItemTypes.Note)//if note clicked.
            {
                /*check which note clicked and show the correct note*/
                if (value.data.DoAction()==1)
                {
                    noteImage1.SetActive(true);
                }
                else if(value.data.DoAction()== 2)
                    {
                    noteImage2.SetActive(true);
                }
                else if (value.data.DoAction() == 3)
                {
                    noteImage3.SetActive(true);
                }
            }
            else
            {
                value.ItemAction();
            }
               
        }
    }
}
