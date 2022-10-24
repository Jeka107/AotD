using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsNames;

public class HitObjectActionFactory
{
    private PlayerActions playerActions;

    public Dictionary<string, AbstractHitObjectAction> actions = new Dictionary<string, AbstractHitObjectAction>();

    public HitObjectActionFactory(PlayerActions playerActions)
    {
        this.playerActions = playerActions;
        actions.Add(ObjectTag.Door, playerActions?.doorAction);
        actions.Add(ObjectTag.Safe, playerActions?.safeAction);
        actions.Add(ObjectTag.Collectable, playerActions?.collectableAction);
        actions.Add(ObjectTag.Friend, playerActions?.friendAction);
        actions.Add(ObjectTag.Switch, playerActions?.switchLightAction);
        actions.Add(ObjectTag.Candle, playerActions?.candleAction);
        actions.Add(ObjectTag.WallCut, playerActions?.wallCutAction);
        actions.Add(ObjectTag.ExitDoor, playerActions?.exitAction);
        actions.Add(ObjectTag.ItemPed, playerActions?.itemPed);
        actions.Add(ObjectTag.FinalButton, playerActions?.finishAction);
    }

    public IHitObjectAction GetActionByTag(string tagName)
    {
        var action = actions.GetValueOrDefault(tagName);
        action?.SetPlayerActions(playerActions);
        return action;
    }
}
