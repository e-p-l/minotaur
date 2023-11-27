using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyIsAttacking : Node
{
    public GameObject[] enemies;

    public CheckEnemyIsAttacking(GameObject[] enemies)
    {
        this.enemies = enemies;
    }

    public override NodeState Evaluate()
    {
        foreach (GameObject enemy in this.enemies){
            AdventurerController adventurerController = enemy.GetComponent<AdventurerController>();
            if (adventurerController.isAttacking){
                SetData("target", enemy);

                state = NodeState.SUCCESS;
                return state;
            } 
        }

        state = NodeState.FAILURE;
        return state;
    }

}