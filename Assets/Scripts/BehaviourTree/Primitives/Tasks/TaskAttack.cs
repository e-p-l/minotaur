using UnityEngine;

namespace BehaviourTree {
  public class TaskAttack : Node
  {
      private float cooldown = 1f;
      private float timer;
      private Transform transform;
      private GameObject attack;

      public TaskAttack(GameObject attack, Transform transform)
      {
          this.timer = 0f;
          this.attack = attack;
          this.transform = transform;
      }

      public override NodeState Evaluate()
      {
          GameObject target = GetData("target");

          timer += Time.deltaTime;
          if (timer >= cooldown)
          {
              GameObject aoe = Object.Instantiate(attack, transform);
              Object.Destroy(aoe, 0.7f); 
              timer = 0f;
          }

          state = NodeState.RUNNING;
          return state;
      }

  }
}
