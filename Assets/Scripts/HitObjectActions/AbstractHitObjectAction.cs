using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHitObjectAction : MonoBehaviour, IHitObjectAction
{
    public PlayerActions playerActions { get; private set; }

    public virtual void ShowText() { }
    public virtual void DoAction() { }

    public virtual void SetPlayerActions(PlayerActions playerActions)
    {
        this.playerActions = playerActions;
    }
}
