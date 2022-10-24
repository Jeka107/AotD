using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookUI : MonoBehaviour
{
    [SerializeField] private Transform notebook;
    [SerializeField] private GameObject notebookSlotPrefab;
    [SerializeField] private NotebookSystem notebookSystem;
    [SerializeField] public TextMeshProUGUI tapeAmount;
    [SerializeField] public TextMeshProUGUI noteAmount;

    [Header("Order of items")]
    [SerializeField] private Vector3 notebookNoteStartPosition;
    [SerializeField] private Vector3 notebookTapeStartPosition;
    [SerializeField] private int columnDistance;
    [SerializeField] private int RowDistance;
    [SerializeField] private int numInRow;

    private Dictionary<string, Vector3> positionDictionary = new Dictionary<string, Vector3>();
    private int countNumberInRowNote = 1;
    private int countNumberInRowTape = 1;
    private Vector3 currentPosition;
    private void Awake()
    {
        positionDictionary.Add(ConstantsNames.NotebookItemTypes.Note, notebookNoteStartPosition);
        positionDictionary.Add(ConstantsNames.NotebookItemTypes.Tape, notebookTapeStartPosition);
    }
    void Start()
    {
        NotebookSystem.OnChange += OnUpdateNotebook;
    }
    private void OnDestroy()
    {
        NotebookSystem.OnChange -= OnUpdateNotebook;
    }
    private void OnUpdateNotebook(string type)//update notebook UI.
    {
        countNumberInRowNote =1;
        countNumberInRowTape = 1;
        positionDictionary[ConstantsNames.NotebookItemTypes.Note] = notebookNoteStartPosition;
        positionDictionary[ConstantsNames.NotebookItemTypes.Tape] = notebookTapeStartPosition;

        foreach (Transform t in notebook)
        {
            Destroy(t.gameObject);
        }

        tapeAmount.text = notebookSystem.tapes.Count.ToString() + "/3";
        noteAmount.text = notebookSystem.notes.Count.ToString() + "/3";

        DrawInventory();
    }
    public void DrawInventory()
    {
        foreach (NotebookItem item in notebookSystem.notes)//draw notes.
        {
            if (countNumberInRowNote > numInRow)//next row
            {
                currentPosition = new Vector3(notebookNoteStartPosition.x, currentPosition.y- RowDistance, 0f);
                positionDictionary[item.data.itemType] = currentPosition;
                countNumberInRowNote = 1;
            }
            AddNotebookSlot(item);
            countNumberInRowNote++;
        }
        foreach (NotebookItem item in notebookSystem.tapes)//draw tapes.
        {
            if (countNumberInRowTape > numInRow)//next row
            {
                currentPosition = new Vector3(notebookTapeStartPosition.x, currentPosition.y - RowDistance, 0f);
                positionDictionary[item.data.itemType] = currentPosition;
                countNumberInRowTape = 1;
            }
            AddNotebookSlot(item);
            countNumberInRowTape++;
        }
    }
    public void AddNotebookSlot(NotebookItem item)
    {
        currentPosition = positionDictionary.GetValueOrDefault(item.data.itemType);
        GameObject obj = Instantiate(notebookSlotPrefab);

        currentPosition = new Vector3(currentPosition.x + columnDistance, currentPosition.y, 0f);
        positionDictionary[item.data.itemType] = currentPosition;

        obj.transform.position = new Vector3(currentPosition.x, currentPosition.y, 0f);
        obj.transform.SetParent(notebook, false);
        
        var slot = obj.GetComponent<NotebookItemSlot>();

        slot.Set(item);
    }
}
