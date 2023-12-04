using System.Collections.Generic;

namespace HTN {
    public enum Condition{
        TRUE, FALSE, DONT_CARE
    }

    public enum Action{
        NULL, GO_TO_TREASURE, PICK_UP, GO_TO_CORNER, WAIT_FOR_OPENING, PERFORM_HIT, FLEE, GO_TO_ATTACK_RANGE, GO_IN_DETECTION_RANGE
    }

    public abstract class HTNNode {
        public Condition[] preconditions;

        abstract public bool checkPreconditions(bool[] worldState);
    }

    public class PrimitiveTask : HTNNode{
        public Condition[] postconditions;
        public Action action;

        public PrimitiveTask(Condition[] preconditions, Condition[] postconditions, Action action){
            this.preconditions = preconditions;
            this.postconditions = postconditions;
            this.action = action;
        }

        override public bool checkPreconditions(bool[] worldState){
            // check if all the preconditions are met
            for (int i=0; i<worldState.Length; i++){
                if (preconditions[i] == Condition.TRUE && worldState[i] != true){
                    return false;
                }
                else if (preconditions[i] == Condition.FALSE && worldState[i] != false){
                    return false;
                }
            }    

            return true;
        }

        public bool[] applyPostconditions(bool[] worldState){
            //change the worldstate based on the postconditions we care about
            bool[] updatedWorldState = worldState;

            for (int i=0; i<worldState.Length; i++){
                if (postconditions[i] == Condition.TRUE){
                    updatedWorldState[i] = true;                    
                }
                else if (postconditions[i] == Condition.FALSE){
                    updatedWorldState[i] = false;
                }
            }           

            return updatedWorldState;
        }
    }     

    public class CompositeTask : HTNNode{
        public List<PrimitiveTask> subtasks;
        
        public CompositeTask(Condition[] preconditions, List<PrimitiveTask> subtasks){
            this.preconditions = preconditions;
            this.subtasks = subtasks;
        }

        override public bool checkPreconditions(bool[] worldState){
            // check if all the preconditions are met
            for (int i=0; i<worldState.Length; i++){
                if (preconditions[i] == Condition.TRUE && worldState[i] != true){
                    return false;
                }
                else if (preconditions[i] == Condition.FALSE && worldState[i] != false){
                    return false;
                }
            }    

            return true;
        }
    }

    public class RootNode {
        public List<CompositeTask> methods;

        public RootNode(List<CompositeTask> methods){
            this.methods = methods;
        }
    }
}
