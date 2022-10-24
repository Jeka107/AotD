using UnityEngine;

public class SafeAction : AbstractHitObjectAction
{
	public override void ShowText() 
	{
		
		if (playerActions.hitObject.GetComponent<Animator>().GetBool("Activate") == false)
		{
			playerActions.gamePlayCanvas.PressFTextDoorOn();//text press F on
		}
		else
		{
			playerActions.gamePlayCanvas.TextOff(); //texts off
		}
		
	}
	public override void DoAction()
    {
		if (playerActions.hitObject.GetComponent<LockedSafeScript>().isSafeOpen == false)
		{
			playerActions.hitObject.GetComponent<Animator>().enabled = true;

            if (playerActions.hitObject.gameObject.name== "TreasureChest") //if hitobejct is the safe.
            {
				playerActions.safeCanvas.SetActive(true);  //activate safe canvas.
			}
            else if (playerActions.hitObject.gameObject.name == "SyringeBox") //if hitobejct is the sytinge box.
			{
				playerActions.colorSafeCanvas.SetActive(true); //activate sytinge box canvas.
			}

			/*when safe canvas is on disable input and movement*/
            playerActions.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
            playerActions.GetComponent<StarterAssets.StarterAssetsInputs>().enabled = false;
            playerActions.flashlightHand.SetActive(false);
            playerActions.GetComponent<PlayerActions>().enabled = false;
            playerActions.GetComponent<PlayerActions>().gameIsPaused = true;

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Time.timeScale = 0f;
		}
		else
		{
			playerActions.hitObject.GetComponent<Animator>().SetBool("Activate", true);
			playerActions.hitObject.GetComponent<LockedSafeScript>().ActiveKnifeCollider();
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale = 1f;
			playerActions.gamePlayCanvas.TextOff(); //texts off
		}
	}
}