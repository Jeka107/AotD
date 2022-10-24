using UnityEngine;

[CreateAssetMenu(menuName = "Note Data")]
public class NoteItem : NotebookItemData
{
    [TextArea(6, 6)]
    public string noteText;
    public int noteType;

    public NoteItem()
    {
        itemType = ConstantsNames.NotebookItemTypes.Note;
    }
   
    public override int DoAction()
    {
        return noteType;
    }
}
