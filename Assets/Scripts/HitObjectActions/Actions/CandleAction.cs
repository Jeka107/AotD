using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAction : AbstractHitObjectAction
{
    public override void ShowText()
    {
        playerActions.gamePlayCanvas.PressFToLightCandle();
    }
    public override void DoAction()
    {
        playerActions.hitObject.GetComponent<CandleScript>()?.CandleUse(); //turn of candle.
    }
}
