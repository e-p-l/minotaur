using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

namespace HTN {
    public class TankController : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public GameObject treasure;
        public GameObject corner;
        public GameObject minotaur;
        public MinotaurBehaviourTree minotaurBT;
        public bool isAttacking;
        public Queue<PrimitiveTask> plan;
        public bool[] worldState;
        public HTNTank htn;
        public Planner planner;
        public PrimitiveTask currTask;
        public int hp;
        // public float timer;
        public bool stoleTreasure =false;
        public bool gameWon = false;
        public bool isRanged = false;
        public int baseHP;

        void Start(){
            this.htn = new HTNTank();
            this.worldState = new bool[]{false,false,false,false,false,false};  
            this.planner = new Planner(htn.root);
            this.plan = this.planner.buildQueue(this.worldState); 
            this.currTask = null;
            this.hp = this.baseHP;
            // this.timer = 0f;

            minotaurBT = minotaur.GetComponent<MinotaurBehaviourTree>();
        }

        void Update()
        {
            Debug.LogFormat("target : {0}", minotaurBT.root.target);
            // Debug.LogFormat("q.L : {0}", plan.Count);
            // timer += Time.deltaTime;

            updateWorldState();

            checkCollisions();
            if (hp < 0){
                // dropTreasure();
                respawn();
            }
            //update the world state here
            //check if we're doing an action
                //if we're doing one, continue
                //else, check if we have a future action
                    //if we do, dequeue and execute it
                    //else, build a plan

            if (currTask != null){
                Debug.LogFormat("Trying performing : {0} with q.C: {1}", currTask.action, this.plan.Count);
                //check if the preconditions is still met
                if (currTask.checkPreconditions(this.worldState)){
                    Debug.LogFormat("Currently performing : {0}", currTask.action);
                    //implement different actions here
                    if (currTask.action == Action.GO_IN_DETECTION_RANGE){
                        if (Vector3.Distance(minotaur.transform.position, gameObject.transform.position) > 5f){
                            //go to the minotaur
                            agent.SetDestination(minotaur.transform.position);
                        }
                        else {
                            //stop when you're in detection range and go to next task
                            agent.SetDestination(gameObject.transform.position);
                            currTask = null;
                        }
                    }

                    else if (currTask.action == Action.GO_TO_ATTACK_RANGE){
                        //ranged adventurer
                        if (this.isRanged){
                            // int layerMask = 1<<7;
                            // RaycastHit hit;
                            // Vector3 direction = minotaur.transform.position - gameObject.transform.position;

                            // if (Physics.Raycast(gameObject.transform.position, direction, out hit, layerMask)) {
                            //     //if the minotaur is in line of sight, stop moving and go to next task
                            //     Debug.DrawRay(gameObject.transform.position, direction * hit.distance, Color.yellow);
                            //     Debug.LogFormat("")
                            //     if (hit.transform == minotaur.transform) {
                            //         agent.SetDestination(gameObject.transform.position);
                            //         currTask = null;
                            //     } 
                            //     //otherwise, continue moving towards the minotaur
                            //     else {
                            //         agent.SetDestination(minotaur.transform.position);
                            //     }
                            // }
                            if (Vector3.Distance(minotaur.transform.position, gameObject.transform.position) > 5f){
                                Debug.LogFormat("Inside First with p.l : {0}",this.plan.Count);                                
                                agent.SetDestination(minotaur.transform.position);
                            }
                            else {
                                Debug.LogFormat("Inside Second with p.l : {0}",this.plan.Count);
                                agent.SetDestination(gameObject.transform.position);
                                currTask = null;
                            }
                        }
                        //melee adventurer
                        else {
                            //if minotaur is close enough go to next action, otherwise continue moving towards him
                            if (Vector3.Distance(minotaur.transform.position, gameObject.transform.position) > 1f){
                                agent.SetDestination(minotaur.transform.position);
                            }
                            else {
                                agent.SetDestination(gameObject.transform.position);
                                currTask = null;
                            }
                        }
                    }
                    
                    else if (currTask.action == Action.FLEE){
                        if (Vector3.Distance(gameObject.transform.position, corner.transform.position) > 1f){
                            agent.SetDestination(corner.transform.position);
                        }
                        else {
                            currTask = null;
                        }
                    }
                    
                    else if (currTask.action == Action.PERFORM_HIT){
                        this.isAttacking = true;
                        // go to flee once minotaur is close
                        Debug.Log(Vector3.Distance(minotaur.transform.position, gameObject.transform.position)); 
                        if (Vector3.Distance(minotaur.transform.position, gameObject.transform.position) < 3f){
                            this.isAttacking = false;
                            currTask = null;
                        }
                    }
                }
                else {
                    currTask = null;
                    this.plan = this.planner.buildQueue(this.worldState);
                    Debug.LogFormat("New plan created with length : {0}", this.plan.Count);

                }
            } 
            else{
                Debug.Log("Preconditions not met");
                if (this.plan.Count > 0){
                    //dequeue the new action
                    currTask = this.plan.Dequeue();
                    // Debug.LogFormat("New action started : {0}", currTask.action);
                }
                else {
                    Debug.Log("No tasks left");
                    //compute a new plan
                    this.plan = this.planner.buildQueue(this.worldState);
                    // Debug.LogFormat("New plan created with length : {0}", this.plan.Count);
                }
            }
        }

        void checkCollisions(){
            //collision with treasure
            // Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 0.3f, 1<<11);
            // if (colliders.Length > 0){
            //     Debug.Log("TREASURE COLLIDE");
            //     if (timer > 3f){
            //         pickUpTreasure();
            //     } 
            // }

            //collision with minotaur attack
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 0.3f, 1<<9);
            if (colliders.Length > 0){
                this.hp -= 1;
                // if (this.stoleTreasure){
                //     dropTreasure();
                // }
            }
        
        }

        // void pickUpTreasure(){
        //     TreasureController treasureController = treasure.GetComponent<TreasureController>();
        //     // if (!treasureController.isStolen){
        //         treasureController.isPickedUp(gameObject);
        //     // }
        //     this.stoleTreasure = true;
        // } 

        // void dropTreasure(){
        //     timer = 0f;
        //     TreasureController treasureController = treasure.GetComponent<TreasureController>();
        //     treasureController.isDropped();
        //     this.stoleTreasure = false;
        // }

        void respawn(){
            gameObject.transform.position = corner.transform.position;
            this.hp = this.baseHP;
            this.htn = new HTNTank();
            this.worldState = new bool[]{false,false,false,false,false,false};  
            this.plan = this.planner.buildQueue(this.worldState); 
            this.currTask.action = Action.NULL;
            this.isAttacking = false;
            // this.timer = 0f;
        }
        
        void updateWorldState(){
            //isInAttackRange
            if (Vector3.Distance(gameObject.transform.position, minotaur.transform.position) <= 5f){
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