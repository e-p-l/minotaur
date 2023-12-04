using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree {
  public class CheckEnemyClose: Node
  {
      public Transform transform;
      public int layerToSearch = 1 << 8;

      public CheckEnemyClose(Transform transform)
      {
          this.transform = transform;
      }

      public override NodeState Evaluate()
      {
          GameObject target = GetData("target");

          if (target == null)
          {
              Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, layerToSearch);
              if (colliders.Length > 0)
              {
                  SetData("target", colliders[0].gameObject);

                  state = NodeState.SUCCESS;
                  return state;
              }

              state = NodeState.FAILURE;
              return state;
          }

          state = NodeState.SUCCESS;
          return state;
      }

  }
}
