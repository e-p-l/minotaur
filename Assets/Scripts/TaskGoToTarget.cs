using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform transform;
    private UnityEngine.AI.NavMeshAgent agent;

    public TaskGoToTarget(Transform transform, UnityEngine.AI.NavMeshAgent agent)
    {
        this.transform = transform;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.6f)
        {
            agent.SetDestination(target.position);
        }

        state = NodeState.RUNNING;
        return state;
    }

}

