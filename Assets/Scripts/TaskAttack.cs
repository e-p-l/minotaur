using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private float cooldown = 1f;
    private float timer;
    
    public TaskAttack(){
        this.timer = 0f;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
           Debug.Log("Minotaur ATTACKS : BOOOM!");
           timer = 0f; 
        }

        state = NodeState.RUNNING;
        return state;
    }

}