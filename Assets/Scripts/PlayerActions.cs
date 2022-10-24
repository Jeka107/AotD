using System;
using System.Collections;
using UnityEngine;
using ConstantsNames;

public class PlayerActions : MonoBehaviour
{
	public event Action<bool> flashLight;

	[Header("Canvas")]
	[SerializeField] public GamePlayCanvas gamePlayCanvas;
	[SerializeField] public GameObject safeCanvas;
	[SerializeField] public GameObject colorSafeCanvas;
	[SerializeField] public ManagerCanvas managerCanvas;
	[SerializeField] public GameObject noteCanvas;

	// light control
	[SerializeField] private GameObject WhiteLight;

	[Header("Sounds")]
	[SerializeField] private AudioSource flashlightON = null;
	[SerializeField] private AudioSource flashlightOff = null;

	[Header("For Me")]
	[SerializeField] public GameObject hitObject;
	[SerializeField] public InventoryItem holdingItem;

	[Header("Tools")]
	[SerializeField] public GameObject toolHand;
	[SerializeField] public GameObject flashlightHand;
	[SerializeField] public GameObject mirror;

	[Header("Inventory")]
	[SerializeField] private InventorySystem inventorySystem;
	[SerializeField] private InventoryUI inventoryUI;

	[Header("HitObject Actions")]
	[SerializeField] public DoorAction doorAction;
	[SerializeField] public SafeAction safeAction;
	[SerializeField] public CollectableAction collectableAction;
	[SerializeField] public FriendAction friendAction;
	[SerializeField] public SwitchLightAction switchLightAction;
	[SerializeField] public CandleAction candleAction;
	[SerializeField] public WallCutAction wallCutAction;
	[SerializeField] public MirrorAction mirrorAction;
	[SerializeField] public ExitDoorAction exitAction;
	[SerializeField] public PutItemAction itemPed;
	[SerializeField] public FinalPuzzleScript finishAction;

	[Header("Enemy Eyes")]
	[SerializeField] private Eyes enemyEyes;

	[SerializeField] public bool gameIsPaused = false;

	[Header("Tutorial")]
	[SerializeField] public bool firstSprintInteraction = false;
	[SerializeField] public bool firstCrouchInteraction = false;

	[HideInInspector] public bool flashLightStatus = false;
	private StarterAssets.StarterAssetsInputs _input;
	private bool mirrorOn;
	private HitObjectActionFactory hitObjectActionFactory;


	void Start()
    {
        _input = GetComponent<StarterAssets.StarterAssetsInputs>(); //not in use yet or ever
		hitObjectActionFactory = new HitObjectActionFactory(this);

		Noise.onFirstInteraction += Tutorial;
		Eyes.onFirstInteraction += Tutorial;
		StarterAssets.FirstPersonController.onFirstInteraction += TutorailOff;
	}
    private void OnDestroy()
    {
		Noise.onFirstInteraction -= Tutorial;
		Eyes.onFirstInteraction -= Tutorial;
		StarterAssets.FirstPersonController.onFirstInteraction -= TutorailOff;
	}
	private void Update()
    {
		if (firstSprintInteraction&&!firstCrouchInteraction)
			gamePlayCanvas.HoldShiftToSprintText();
		else if (firstCrouchInteraction&&!firstSprintInteraction)
			gamePlayCanvas.TapCtrlToCrouchText();
		else
			ActionTextAppearance(_input.mousePosition);
	}

    #region Labels-On/Off
    public void PauseLabel(bool pause)
	{
		if (!gameIsPaused)
		{
			managerCanvas.Pause();
			gameIsPaused = true;
		}
		else
		{
			managerCanvas.Resume();
			gameIsPaused = false;
		}
	}
	#endregion

	#region TextAppearance

	public void ActionTextAppearance(Vector2 mousePosition)
    {
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 2))//ratcast to get object player looking at.
		{
			Debug.DrawLine(ray.origin, hit.point);

			hitObject = hit.collider.gameObject;

			if(hitObject.tag== ObjectTag.ItemPed)
            {
				StartCoroutine(MirrorCoroutineOff());
			}

			gamePlayCanvas.TextOff();

			var action = hitObjectActionFactory.GetActionByTag(hitObject.tag);

			if (action != null)
				action.ShowText();//depents on hitobject show text.
			else
				gamePlayCanvas.TextOff();
		}
		else
        {
			gamePlayCanvas.TextOff(); //texts off
		}
	}

	#endregion

	#region Actions- F Button
	public void PressActions(bool inputAction)
	{
		if (_input.action)
		{
			if (hitObject != null)
			{
				hitObjectActionFactory.GetActionByTag(hitObject.tag)?.DoAction(); //using factory and stragety design pattern. 
			}
			else
			{
				gamePlayCanvas.TextOff(); //texts off
			}
		}
	}
    #endregion

    #region Actions-LeftMousePress
	public void FlashLight(bool light)// on/off flashLight.
    {
		if (light&&!gameIsPaused)
		{
			if (!flashLightStatus)
			{
				WhiteLight.SetActive(true);
				flashLightStatus = true;
				flashlightON.Play();
				flashLight?.Invoke(true);
			}
			else
			{
				WhiteLight.SetActive(false);
				flashLightStatus = false;
				flashlightOff.Play();
				flashLight?.Invoke(false);
			}
		}
	}
    #endregion

    #region InventoryUse
	public void ItemUse(float itemNum)
    {
		holdingItem = inventorySystem.ItemUse((int)itemNum); //current item player holds.
		
		SelectedItem(holdingItem);//show selected item on UI.
		//ReadbleItem(holdingItem);
	}

    private void SelectedItem(InventoryItem holdingItem)
    {
		inventoryUI.SelectedItem(holdingItem);

		if (holdingItem?.data?.id == ObjectId.InventoryMirror&&hitObject.tag!= ObjectTag.ItemPed)
		{
			StartCoroutine(MirrorCoroutineOn());
		}
		else
        {
			StartCoroutine(MirrorCoroutineOff());
		}
	}

    /*private void ReadbleItem(InventoryItem holdingItem)
	{
		if (holdingItem != null)
		{
			if (holdingItem?.data?.id == ObjectId.InventoryNote)
			{
				if (noteCanvas.activeSelf == false)
				{
					noteCanvas.SetActive(true);
					Time.timeScale = 0f;
				}
				else
				{
					noteCanvas.SetActive(false);
					Time.timeScale = 1f;
				}
			}
			else
			{
				noteCanvas.SetActive(false);
				Time.timeScale = 1f;
			} 
		}
	}*/
	IEnumerator MirrorCoroutineOn()
	{
		if (mirrorOn == false)
		{
			toolHand.GetComponent<Animator>().SetBool("toolOn", true);
			flashlightHand.SetActive(false);
			mirror.SetActive(true);
			yield return new WaitForSeconds(1.3f);
			mirrorOn = true;
			yield return new WaitForSeconds(0.5f);
		}
		else if (mirrorOn == true)
		{
			toolHand.GetComponent<Animator>().SetBool("toolOn", false);
			yield return new WaitForSeconds(1.3f);
			flashlightHand.SetActive(true);
			mirror.SetActive(false);
			mirrorOn = false;
			yield return new WaitForSeconds(0.5f);
		}
	}
	IEnumerator MirrorCoroutineOff()
    {
		if (mirrorOn == true)
		{
			toolHand.GetComponent<Animator>().SetBool("toolOn", false);
			yield return new WaitForSeconds(1.3f);
			flashlightHand.SetActive(true);
			mirror.SetActive(false);
			mirrorOn = false;
			yield return new WaitForSeconds(0.5f);
		}
	}
	public void ClearHoldingItem()
	{
		holdingItem = null;
	}
	#endregion

	#region Tutorial
	public void Tutorial(string state)
    {
		if (state == "Sprint")
			firstSprintInteraction = true;
		else if (state == "Crouch")
			firstCrouchInteraction = true;
    }
	public void TutorailOff(string state)
    {
		if (state == "Sprint")
			firstSprintInteraction = false;
		else if (state == "Crouch")
			firstCrouchInteraction = false;
	}
    #endregion
}
