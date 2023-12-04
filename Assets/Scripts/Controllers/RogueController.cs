using System.Collections.Generic;
using UnityEngine;

namespace HTN {
    public class RogueController : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public GameObject treasure;
        public GameObject corner;
        public GameObject minotaur;
        public MinotaurBehaviourTree minotaurBT;
        public bool isAttacking;
        public Queue<PrimitiveTask> plan;
        public bool[] worldState;
        public HTNRogue htn;
        public Planner planner;
        public PrimitiveTask currTask;
        public int hp;
        public float timer;
        public bool stoleTreasure =false;
        public bool gameWon = false;
        public int baseHP;
        public GameObject textWin;

        void Start(){
            this.htn = new HTNRogue();
            this.worldState = new bool[]{false,false,false,false,false,false};  
            this.planner = new Planner(htn.root);
            this.plan = this.planner.buildQueue(this.worldState); 
            this.currTask = null;
            this.timer = 0f;
            this.hp = this.baseHP;

            minotaurBT = minotaur.GetComponent<MinotaurBehaviourTree>();
        }

        void Update()
        {
            if (gameWon){
                textWin.SetActive(true);
            }
            // Debug.LogFormat("q.L : {0}", plan.Count);
            timer += Time.deltaTime;

            updateWorldState();

            checkCollisions();
            if (hp < 0){
                dropTreasure();
                respawn();
            }
            //update the world state here
            //check if we're doing an action
                //if we're doing one, continue
                //else, check if we have a future action
                    //if we do, dequeue and execute it
                    //else, build a plan

            if (currTask != null){
                //check if the preconditions is still met
                if (currTask.checkPreconditions(this.worldState)){
                    // Debug.LogFormat("Currently performing : {0}", currTask.action);
                    //implement different actions here
                    if (currTask.action == Action.GO_TO_TREASURE){
                        if (Vector3.Distance(gameObject.transform.position, treasure.transform.position) > 1f){
                            agent.SetDestination(treasure.transform.position);
                        }
                        // treasure is attained
                        else{
                            currTask = null;
                        }
                    }

                    else if (currTask.action == Action.PICK_UP){
                        // TODO: implementation of the pickup mecanism with 3 seconds
                        if (this.stoleTreasure){
                            currTask = null;
                        }
                    }
                    
                    else if (currTask.action == Action.GO_TO_CORNER){
                        if (Vector3.Distance(gameObject.transform.position, corner.transform.position) > 2f){
                            // Debug.Log("Still going");
                            agent.SetDestination(corner.transform.position);
                        }
                        // corner is attained
                        else{
                            // Debug.Log("Attained");
                            if(this.stoleTreasure){
                                gameWon = true;
                            }
                            currTask = null;
                            // Debug.Log("YOU WON!");
                        }
                    }
                    
                    else if (currTask.action == Action.WAIT_FOR_OPENING){
                        if (minotaurBT.root.target != null){
                            currTask = null;
                        }
                        else {
                            agent.SetDestination(gameObject.transform.position);
                        }

                    }
                }
                else {
                    // Debug.LogFormat("New plan created with length : {0}", this.plan.Count);
                    currTask = null;
                    this.plan = this.planner.buildQueue(this.worldState);
                }
            } 
            else{
                if (this.plan.Count > 0){
                    //dequeue the new action
                    currTask = this.plan.Dequeue();
                    // Debug.LogFormat("New action started : {0}", currTask.action);
                }
                else {
                    //compute a new plan
                    this.plan = this.planner.buildQueue(this.worldState);
                    // Debug.LogFormat("New plan created with length : {0}", this.plan.Count);
                }
            }
        }

        void checkCollisions(){
            //collision with treasure
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 0.3f, 1<<11);
            if (colliders.Length > 0){
                // Debug.Log("TREASURE COLLIDE");
                if (timer > 3f){
                    pickUpTreasure();
                } 
            }

            //collision with minotaur attack
            colliders = Physics.OverlapSphere(gameObject.transform.position, 0.3f, 1<<9);
            if (colliders.Length > 0){
                this.hp -= 1;
                if (this.stoleTreasure){
                    dropTreasure();
                }
            }
        
        }

        void pickUpTreasure(){
            TreasureController treasureController = treasure.GetComponent<TreasureController>();
            // if (!treasureController.isStolen){
                treasureController.isPickedUp(gameObject);
            // }
            this.stoleTreasure = true;
        } 

        void dropTreasure(){
            timer = 0f;
            TreasureController treasureController = treasure.GetComponent<TreasureController>();
            treasureController.isDropped();
            this.stoleTreasure = false;
        }

        void respawn(){
            gameObject.transform.position = corner.transform.position;
            this.hp = this.baseHP;
            this.htn = new HTNRogue();
            this.worldState = new bool[]{false,false,false,false,false,false};  
            this.plan = this.planner.buildQueue(this.worldState); 
            this.currTask.action = Action.NULL;
            this.timer = 0f;
        }
        
        void updateWorldState(){
            //isInAttackRange
            if (Vector3.Distance(gameObject.transform.position, minotaur.transform.position) < 0.3f){
                this.worldState[0] = true;
            }
            else{
                this.worldState[0] = false;
            }

            //isBeingChased
            if (minotaurBT.root.target == gameObject){
                this.worldState[1] = true;
            }
            else {
                this.worldState[1] = false;
            }
             
            //isHoldingTreasure
            if (stoleTreasure){
                this.worldState[2] = true;
            }
            else {
                this.worldState[2] = false;
            }

            //isAtTreasure
            if (Vector3.Distance(treasure.transform.position, gameObject.transform.position) < 0.3f){
                this.worldState[3] = true;
            }
            else {
                this.worldState[3] = false;
            }

            //hasWonGame
            if (gameWon){
                this.worldState[4] = true;
            }
            else{
                this.worldState[4] = false;
            }

            //minotaurIsChasing
            if (minotaurBT.root.target != null){
                this.worldState[5] = true;
            }
            else {
                this.worldState[5] = false;
            }
        }
    }
}
