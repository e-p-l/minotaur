using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckIsCloseToTreasure : Node
{
    public Transform transform;
    public GameObject treasure;

    public CheckIsCloseToTreasure(Transform transform, GameObject treasure)
    {
        this.transform = transform;
        this.treasure = treasure;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(transform.position, this.treasure.transform.position) < 5f){
            state = NodeState.SUCCESS;
            return state;
        }
        else {
            ClearData("target");
            state = NodeState.FAILURE;
            return state;
        }
    }
}