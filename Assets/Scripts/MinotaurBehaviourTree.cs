using BehaviorTree;

public class MinotaurBehaviourTree : Tree{

    public UnityEngine.Transform transform;
    public UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.Vector3[] destinations;

    protected override Node SetupTree(){
        Node root = new TaskPatrol(transform, destinations, agent);

        return root;
    } 
}