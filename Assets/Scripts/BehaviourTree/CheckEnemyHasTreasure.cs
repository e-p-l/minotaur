using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyHasTreasure : Node
{
    private GameObject treasure;

    public CheckEnemyHasTreasure(GameObject treasure)
    {
        this.treasure = treasure;
    }

    public override NodeState Evaluate()
    {
        // set target to be the thief
        TreasureController treasureController = treasure.GetComponent<TreasureController>();
        if (treasureController.isStolen)
        {
            SetData("target", treasureController.thief);
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

}