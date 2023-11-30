using System.Collections.Generic;

namespace HTN {
    public class HTNRogue {
        public RootNode root;

        // worldState = {
        //     isInAttackRange,
        //     isBeingChased,
        //     isHoldingTreasure,
        //     isAtTreasure,
        //     hasWonGame,
        //     minotaurIsChasing,
        //  }

        public HTNRogue(){
            this.root = BuildTree();
        }

        public RootNode BuildTree(){
            //build the tree that is used for the rogue characters
            Condition[] preconditions = {Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE};    
            Condition[] postconditions = {Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.TRUE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask goToTreasure = new PrimitiveTask(preconditions, postconditions, Action.GO_TO_TREASURE);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.DONT_CARE, Condition.TRUE, Condition.TRUE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask pickUp = new PrimitiveTask(preconditions, postconditions, Action.PICK_UP);
            
            preconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.TRUE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.TRUE, Condition.DONT_CARE, Condition.TRUE, Condition.DONT_CARE};    
            PrimitiveTask runToCorner = new PrimitiveTask(preconditions, postconditions, Action.GO_TO_CORNER);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            postconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            PrimitiveTask waitInPlace = new PrimitiveTask(preconditions, postconditions, Action.GO_TO_CORNER);

            preconditions = new Condition[]{Condition.DONT_CARE, Condition.DONT_CARE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.DONT_CARE};    
            List<PrimitiveTask> substasks = new List<PrimitiveTask>{goToTreasure, pickUp, runToCorner}; 
            CompositeTask straightToTreasure = new CompositeTask(preconditions, substasks);

            substasks = new List<PrimitiveTask>{waitInPlace, goToTreasure, pickUp, runToCorner}; 
            preconditions = new Condition[]{Condition.DONT_CARE, Condition.FALSE, Condition.FALSE, Condition.DONT_CARE, Condition.DONT_CARE, Condition.TRUE};    
            CompositeTask waitForOpening = new CompositeTask(preconditions, substasks);

            List<CompositeTask> methods = new List<CompositeTask>{waitForOpening, straightToTreasure};
            RootNode root = new RootNode(methods); 

            return root;
        }

        //TODO : HTNTank.cs and modify HTNNode.cs to add the tank tasks
        //TODO : Execute the actions in a RogueController and TankController 
    }
}