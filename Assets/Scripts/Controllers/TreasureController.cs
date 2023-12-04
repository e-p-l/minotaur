using UnityEngine;

public class TreasureController : MonoBehaviour
{
    public GameObject thief;
    public bool isStolen;

    void Start(){
        this.isStolen = false;
        this.thief = null;
    }
    
    void Update()
    {
        // move with the thief when stolen
        if (isStolen)
        {
            gameObject.transform.position = thief.transform.position;
        }
    }

    public void isDropped(){
        this.thief = null;
        this.isStolen = false;
    }

    public void isPickedUp(GameObject thief){
        this.thief = thief;
        this.isStolen = true;
    }
}
