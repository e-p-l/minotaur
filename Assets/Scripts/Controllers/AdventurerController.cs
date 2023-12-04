using UnityEngine;

public class AdventurerController : MonoBehaviour
{
    public Camera cam;
    public UnityEngine.AI.NavMeshAgent agent1;
    public UnityEngine.AI.NavMeshAgent agent2;
    public bool isAttacking;

    void Update()
    {
       if(Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                agent1.SetDestination(hit.point);
            }
       } 
       if(Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                agent2.SetDestination(hit.point);
            }
       } 
    }
}
