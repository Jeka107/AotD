using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPuzzleScript : AbstractHitObjectAction
{
    [SerializeField] private NotebookSystem notebook;
    [SerializeField] private GameObject choiceCanvas;
    [SerializeField] public bool solved =false;
    [SerializeField] public bool[] order=new bool[] { false, false, false, false }; //array to check when puzzle completed.

    private void Start()
    {
        ItemPedestal.onCheckPuzzle += CheckPuzzleSolved;
    }
    private void OnDestroy()
    {
        ItemPedestal.onCheckPuzzle -= CheckPuzzleSolved;
    }
    public override void ShowText()
    {
        if (solved)
        {
            if (playerActions.hitObject.name == "RealButton") //text when looking on real ending button.
            {
                if (notebook.tapes.Count == 3 && notebook.notes.Count == 3)
                {
                    playerActions.gamePlayCanvas.PressFToFinishReal();//text press to finish the game
                }
                else
                {
                    playerActions.gamePlayCanvas.FindMoreTapeNote();//text press to finish the game
                }
            }
            else if (playerActions.hitObject.name == "FinalButton") //text when looking on normal ending button.
            {
                playerActions.gamePlayCanvas.PressFToFinish();//text press to finish the game
            }
        }
        else
        {
            playerActions.gamePlayCanvas.SolvePuzzle4();//solve the puzzle first
        }
    }

    public override void DoAction()
    {
        if (solved)//if puzzle solved.
        {
            if (playerActions.hitObject.name == "RealButton")//if player looking on real ending button.
            {
                if (notebook.tapes.Count == 3 && notebook.notes.Count == 3)// real ending can activated with full journal. 
                {
                   StartCoroutine(RealAction());
                }
                else
                {
                   //play wrong sound?
                }
            }
            else if (playerActions.hitObject.name == "FinalButton")//if player looking on normal ending button.
            {
                StartCoroutine(NormalAction());
            }
        }
        else
        {
            //play wrong sound?
        }
        #region
        /*   if (playerActions.hitObject.name=="RealButton")
           {

           }
           if (playerActions.hitObject.name=="FinalButton")
           {

           }
           //change to open canvas and do 2 seperate action, each will contain loading different scence, put on this before the open canvas an if condition = if the player has all tapes and notes
           if (playerActions.gameObject.GetComponent<CheckpointScript>().fourthFriend)
           {
               choiceCanvas.SetActive(true);
           }*/
        #endregion

    }

    public void CheckPuzzleSolved()//check if puzzle done in correct order and in correct place.
    {
        if (order[0]==true)
        {
            if (order[1]==true)
            {
                if (order[2]==true)
                {
                    if (order[3]==true)
                    {
                        solved = true;
                    }
                   
                }
                else
                {
                    order[3] = false;
                }
            }
            else
            {
                
                order[2] = false;
                order[3] = false;
            }
        }
        else
        {
            order[1] = false;
            order[2] = false;
            order[3] = false;
        }
    }
    IEnumerator RealAction()//activate real ending scene.
    {
        playerActions.hitObject.GetComponent<Animator>().SetBool("press", true);
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene("RealEnding");
    }
    IEnumerator NormalAction()//activate normal ending scene.
    {
        playerActions.hitObject.GetComponent<Animator>().SetBool("press", true);
        yield return new WaitForSeconds(2f);
       
        SceneManager.LoadScene("NormalEnding");
    }
}
