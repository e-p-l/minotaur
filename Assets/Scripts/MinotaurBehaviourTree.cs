using System.Collections;
using System.Collections.Generic;

using BehaviorTree;

public class MinotaurBehaviourTree : Tree
{
    public UnityEngine.Transform transform;
    public UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.Vector3[] destinations;
    public UnityEngine.GameObject treasure;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> {
            new Sequence(new List<Node> {
                new CheckEnemyHasTreasure(treasure),
                new TaskGoToTarget(transform, agent),
            }),
            new Sequence(new List<Node> {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(),
            }),
            new Sequence(new List<Node> {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform, agent),
            }),
            new TaskPatrol(transform, destinations, agent),
        });

        return root;
    }
}