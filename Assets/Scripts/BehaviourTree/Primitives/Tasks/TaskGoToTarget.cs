using UnityEngine;

namespace BehaviourTree{
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
          GameObject target = GetData("target");

          if (Vector3.Distance(transform.position, target.transform.position) > 0.6f)
          {
              agent.SetDestination(target.transform.position);
          }

          state = NodeState.RUNNING;
          return state;
      }

  }
}
