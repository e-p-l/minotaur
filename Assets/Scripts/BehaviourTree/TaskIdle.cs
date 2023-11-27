using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskIdle: Node
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform transform;
    public GameObject treasure;

    public TaskIdle(Transform transform, UnityEngine.AI.NavMeshAgent agent, GameObject treasure)
    {
        this.transform = transform;
        this.agent = agent;
        this.treasure = treasure;
    }

    public override NodeState Evaluate()
    {
        agent.SetDestination(this.treasure.transform.position);

        state = NodeState.RUNNING;
        return state;
    }

}