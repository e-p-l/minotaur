using System.Collections.Generic;

namespace HTN {
    public class HTNTank {
        public RootNode root;

        // worldState = {
        //     isInAttackRange,
        //     isBeingChased,
        //     isHoldingTreasure,
        //     isAtTreasure,
        //     hasWonGame,
        //     minotaurIsChasing,
        //  }

        public HTNTank(){
            this.root = BuildTree();
        }

        public RootNode BuildTree(){
            //build the tree that is used for the rogue characters
            Condition[] preconditions = {Condition.TRUE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            Condition[] postconditions = {Condition.DONT_CARE, Condition.TRUE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.TRUE};    
            PrimitiveTask performHit = new PrimitiveTask(preconditions, postconditions, Action.PERFORM_HIT);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.TRUE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask flee = new PrimitiveTask(preconditions, postconditions, Action.FLEE);
            
            preconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.TRUE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask goToAttackRange = new PrimitiveTask(preconditions, postconditions, Action.GO_TO_ATTACK_RANGE);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask goInDetectionRange = new PrimitiveTask(preconditions, postconditions, Action.GO_IN_DETECTION_RANGE);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            List<PrimitiveTask> substasks = new List<PrimitiveTask>{goToAttackRange, performHit, flee}; 
            CompositeTask attack = new CompositeTask(preconditions, substasks);

            substasks = new List<PrimitiveTask>{goInDetectionRange, flee}; 
            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.FALSE};    
            CompositeTask distract = new CompositeTask(preconditions, substasks);

            List<CompositeTask> methods = new List<CompositeTask>{attack, distract};
            RootNode root = new RootNode(methods); 

            return root;
        }
    }
}