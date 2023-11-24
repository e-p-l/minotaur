using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3[] destinations;
    public Transform transform;
    public int currDestinationIndex;

    public TaskPatrol(Transform transform, Vector3[] destinations, UnityEngine.AI.NavMeshAgent agent)
    {
        this.transform = transform;
        this.destinations = destinations;
        this.agent = agent;
        currDestinationIndex = 0;
    }

    public override NodeState Evaluate()
    {
        // go in the direction of the next destination until it's attained, then change destination
        Vector3 currDestination = destinations[currDestinationIndex];
        if (Vector3.Distance(transform.position, currDestination) < 0.6f)
        {
            currDestinationIndex = (currDestinationIndex + 1) % destinations.Length;
        }
        else
        {
            agent.SetDestination(currDestination);
        }

        state = NodeState.RUNNING;
        return state;
    }

}