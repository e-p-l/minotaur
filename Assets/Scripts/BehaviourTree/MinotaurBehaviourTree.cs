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
        public GameObject[] enemies;

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

        public Node BuildTree()
        {
            Node root = new Selector(new List<Node> {
                //check if an adventurer is in attack range
                new Sequence(new List<Node>{
                    new CheckEnemyInAttackRange(transform),
                    new TaskAttack(),
                }),
                //check if the minotaur is being attacked
                new Sequence(new List<Node>{
                    new CheckEnemyIsAttacking(enemies),
                    new TaskGoToTarget(transform, agent),
                }),
                //check if someone has the treasure
                new Sequence(new List<Node>{
                    new CheckEnemyHasTreasure(treasure),
                    new TaskGoToTarget(transform, agent),
                }),
                //check if someone is close to the minotaur
                new Sequence(new List<Node>{
                    new CheckEnemyClose(transform),
                    new CheckIsCloseToTreasure(transform, treasure),
                    new TaskGoToTarget(transform, agent),
                }),
                //idle if all checks were false 
                new TaskIdle(transform, agent, treasure),
            });

            root.isRoot = true;

            return root;
        }
    }
}
