using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentKeyboardController: MonoBehaviour
{
    public Camera cam;
    public UnityEngine.AI.NavMeshAgent agent;

    void Update()
    {
       if(Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                agent.SetDestination(hit.point);
            }
       } 
    }
}