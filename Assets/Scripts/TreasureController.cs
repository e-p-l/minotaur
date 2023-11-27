using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    public GameObject thief;
    public bool isStolen = false;

    void Update()
    {
        // move with the thief when stolen
        if (isStolen)
        {
            gameObject.transform.position = thief.transform.position;
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 0.5f, 1 << 8);
            if (colliders.Length > 0)
            {
                thief = colliders[0].gameObject;
                isStolen = true;
            }
        }
    }
}