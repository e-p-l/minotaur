using UnityEngine;

namespace BehaviourTree{
  public class CheckEnemyHasTreasure : Node
  {
      private GameObject treasure;
      public TreasureController treasureController;

      public CheckEnemyHasTreasure(GameObject treasure)
      {
          this.treasure = treasure;
      }

      public override NodeState Evaluate()
      {
          // set target to be the thief
          TreasureController treasureController = treasure.GetComponent<TreasureController>();
          if (treasureController.isStolen)
          {
              SetData("target", treasureController.thief);
              state = NodeState.SUCCESS;
              return state;
          }
          state = NodeState.FAILURE;
          return state;
      }
  }
}
