using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform transform;

    public CheckEnemyInAttackRange(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        GameObject target = GetData("target");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= 1.5f)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }

}