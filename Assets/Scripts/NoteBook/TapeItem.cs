using UnityEngine;

[CreateAssetMenu(menuName = "Tape Data")]
public class TapeItem : NotebookItemData
{
    public AudioClip clip;

    public TapeItem()
    {
        itemType = ConstantsNames.NotebookItemTypes.Tape;//set item type.
    }
    public override AudioClip GetClip()//get the audio clip on this tape.
    {
        return clip;
    }
}
