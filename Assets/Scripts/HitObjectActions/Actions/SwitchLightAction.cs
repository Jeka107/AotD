using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightAction : AbstractHitObjectAction
{
    public override void ShowText()
    {
        if (playerActions.hitObject.GetComponent<SwitchScript>()?.lightOff==false)
        {
            playerActions.gamePlayCanvas.PressFToSwitchLight();//text press to turn switch
        }
    }

    public override void DoAction()
    {
        playerActions.hitObject.GetComponent<SwitchScript>()?.SwitchUse();
    }

}
