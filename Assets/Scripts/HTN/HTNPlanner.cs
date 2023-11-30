using System.Collections.Generic;

namespace HTN {
    public class Planner {
        public Queue<PrimitiveTask> queue;
        public RootNode root;

        public Planner(RootNode root){
            this.queue = new Queue<PrimitiveTask>();
            this.root = root;
        }

        public Queue<PrimitiveTask> buildQueue(bool[] worldState){
            //TODO : Randomize the choice of first method            

            Queue<PrimitiveTask> queue = new Queue<PrimitiveTask>();

            //choose a method based on preconditions
            foreach (CompositeTask method in root.methods){
                
                if (method.checkPreconditions(worldState) == true){
                    //add the primitive tasks of the method into the queue
                    foreach (PrimitiveTask task in method.subtasks){
                        queue.Enqueue(task);
                    }
                    return queue;
                }
                // foreach (PrimitiveTask task in method.subtasks){
                //     queue.Enqueue(task);
                    
                // }
                // return queue;
            }

            return queue;
        }
        
        public void makePlan(bool[] worldState){
            this.queue = buildQueue(worldState);
        } 
    }
}