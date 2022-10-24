using UnityEngine.SceneManagement;

public class ExitDoorAction : AbstractHitObjectAction
{

    public override void ShowText()
    {
            playerActions.gamePlayCanvas.PressFToFlee();//text press to exit the house
    }

    public override void DoAction()
    {

        /*depents on how many friends the player saved*/
        if (playerActions.gameObject.GetComponent<CheckpointScript>().firstFriend)
        {
            if (playerActions.gameObject.GetComponent<CheckpointScript>().secondFriend)
            {
                if (playerActions.gameObject.GetComponent<CheckpointScript>().thirdFriend)
                {
                    if (playerActions.gameObject.GetComponent<CheckpointScript>().fourthFriend)
                    {
                        /* if (//all notes)
                         {

                         }*/
                    }
                    else
                    {
                        SceneManager.LoadScene("Ending4");
                    }
                }
                else
                {
                    SceneManager.LoadScene("Ending3");
                }
            }
            else
            {
                SceneManager.LoadScene("Ending2");
            }
        }
        else
        {
            SceneManager.LoadScene("Ending1");
        }
    }
}
