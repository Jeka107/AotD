using UnityEngine;

public class Targetable : MonoBehaviour
{
    private bool isSafe = false;

    public bool GetIsSafe()  
    {
        return isSafe;
    }

    /*when player enters safe zone then change isSafe to true*/
}
