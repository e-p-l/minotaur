using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class MinotaurBehaviourTree : MonoBehaviour
    {
        public Transform transform;
        public UnityEngine.AI.NavMeshAgent agent;
        public Vector3[] destinations;
        public GameObject treasure;
        public Node root = null;

        public void Start()
        {
            root = BuildTree();
        }

        public void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        // Build the behaviour tree
        public Node BuildTree()
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

            root.isRoot = true;

            return root;
        }
    }
}
