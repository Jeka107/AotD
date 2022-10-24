using UnityEngine;

public class GamePlayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pressFOpen;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject pressFCollect;
    [SerializeField] private GameObject pressFToFree;
    [SerializeField] private GameObject findTool;
    [SerializeField] private GameObject pressFUse;
    [SerializeField] private GameObject pressFOpenNote;
    [SerializeField] private GameObject pressFUnlcok;
    [SerializeField] private GameObject pressFToLight;
    [SerializeField] private GameObject controlsPrompt;
    [SerializeField] private GameObject pressFToLightCandle;
    [SerializeField] private GameObject pressFToFlee;
    [SerializeField] private GameObject pressFToRemoveTool;
    [SerializeField] private GameObject holdShiftToSprint;
    [SerializeField] private GameObject tapCtrlToCrouch;
    [SerializeField] private GameObject pressFToFinish;
    [SerializeField] private GameObject pressFToFinishReal;
    [SerializeField] private GameObject findMoreNoteTape;
    [SerializeField] private GameObject solvePuzzle4;

    private void Awake()
    {
        pressFOpen.SetActive(false);
        pressFCollect.SetActive(false);
        pressFUse.SetActive(false);
    }
    public void PressFTextDoorOn()
    {
        pressFOpen.SetActive(true);
        locked.SetActive(false);
    }
    public void LockedDoorOn()
    {
        locked.SetActive(true);
    }
    public void PressFTextCollectOn()
    {
        pressFCollect.SetActive(true);
    }
    public void TextOff() //all texts off
    {
        pressFOpen.SetActive(false);
        pressFCollect.SetActive(false);
        locked.SetActive(false);
        findTool.SetActive(false);
        pressFToFree.SetActive(false);
        pressFOpenNote.SetActive(false);
        pressFUnlcok.SetActive(false);
        pressFToLight.SetActive(false);
        controlsPrompt.SetActive(false);
        pressFToLightCandle.SetActive(false);
        pressFToFlee.SetActive(false);
        pressFToRemoveTool.SetActive(false);
        holdShiftToSprint.SetActive(false);
        tapCtrlToCrouch.SetActive(false);
        pressFToFinish.SetActive(false);
        pressFToFinishReal.SetActive(false);
        findMoreNoteTape.SetActive(false);
        solvePuzzle4.SetActive(false);
    }
    public void PressFToFree()
    {
        pressFToFree.SetActive(true);
    }
    public void FindToolToFree()
    {
        findTool.SetActive(true);
    }
    public void PressFToOpenNote()
    {
        pressFOpenNote.SetActive(true);
    }
    public void PressFUnlock()
    {
        locked.SetActive(false);
        pressFUnlcok.SetActive(true);
    }
    public void PressFToSwitchLight()
    {
        pressFToLight.SetActive(true);
    }
    public void ControlsPrompt()
    {
        controlsPrompt.SetActive(true);
    }
    public void PressFToLightCandle()
    {
        pressFToLightCandle.SetActive(true);
    }
    public void PressFToFlee()
    {
        pressFToFlee.SetActive(true);
    }
    public void PressFToRemoveTool()
    {
        pressFToRemoveTool.SetActive(true);
    }
    public void HoldShiftToSprintText()
    {
        holdShiftToSprint.SetActive(true);
    }
    public void TapCtrlToCrouchText()
    {
        tapCtrlToCrouch.SetActive(true);
    }
    public void PressFToFinish()
    {
        pressFToFinish.SetActive(true);
    }
    public void PressFToFinishReal()
    {
        pressFToFinishReal.SetActive(true);
    }
    public void FindMoreTapeNote()
    {
        findMoreNoteTape.SetActive(true);
    }
    public void SolvePuzzle4()
    {
        solvePuzzle4.SetActive(true);
    }
}
