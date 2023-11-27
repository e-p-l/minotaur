using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInFOVRange : Node
{
    public Transform transform;
    public int layerToSearch = 1 << 8;

    public CheckEnemyInFOVRange(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        GameObject target = GetData("target");

        if (target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, layerToSearch);
            if (colliders.Length > 0)
            {
                SetData("target", colliders[0].gameObject);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

}