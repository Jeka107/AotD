using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{

    #region Constants - Tags
    public static class ObjectTag
    {
		public static readonly string Door = "Door";
		public static readonly string Safe = "Safe";
		public static readonly string Collectable = "Collectable";
		public static readonly string Friend = "Friend";
		public static readonly string Null = "Untagged";
	}
	#endregion
	
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public Vector2 mousePosition;
		public bool jump;
		public bool sprint;
		public bool crouch;
		public bool action;
		public bool light;
		public bool pause;
		public bool notebook;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private bool inventory = false;
		private bool crouched = false;
		private bool notebookstatus = false;

		[SerializeField] private ManagerCanvas managerCanvas;
		
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void Start()
        {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}
		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnCrouch(InputValue value)
		{
			CrouchInput(value.isPressed);
		}
		public void OnFlashlight(InputValue value)
		{
			LightInput(value.isPressed);
		}
		public void OnAction(InputValue value)
		{
			ActionInput(value.isPressed);
		}
        public void OnMousePosition(InputValue value)
        {
			MousePositionInput(value.Get<Vector2>());
		}
		public void OnPause(InputValue value)//Menu
		{
			PauseInput(value.isPressed);
		}

		public void OnNotebook(InputValue value)
        {
			if (!notebookstatus)
			{
				managerCanvas.NotebookStatus(true);
				notebookstatus = true;
			}
			else
			{
				managerCanvas.NotebookStatus(false);
				notebookstatus = false;
			}
		}
		public void OnItemUse(InputValue value)
        {
			float itemNum = value.Get<float>();
			GetComponent<PlayerActions>().ItemUse(itemNum);
		}
#endif
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;				
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}
		public void MousePositionInput(Vector2 newMousePosition)
        {
			mousePosition = newMousePosition;
        }

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void CrouchInput(bool newCrouchState)
		{
			if (!crouched)
			{
				crouch = newCrouchState;
				crouched = true;
			}
			else
            {
				crouch = !newCrouchState;
				crouched = false;
			}
		}

		public void LightInput(bool newLightState)
		{
			light = newLightState;
			GetComponent<PlayerActions>().FlashLight(light);
		}
		public void ActionInput(bool newActionState)
		{
			action = newActionState;
			GetComponent<PlayerActions>().PressActions(action);
		}
		public void PauseInput(bool newPauseState)
        {
            if (GetComponent<PlayerActions>().gameIsPaused == false)
            {
				pause = newPauseState;
			GetComponent<PlayerActions>().PauseLabel(pause);
            }
			
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}