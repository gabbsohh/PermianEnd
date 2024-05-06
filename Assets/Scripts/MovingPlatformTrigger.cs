using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    [SerializeField] public MovingPlatform movingPlatform;

    public BoxCollider2D platformRB;

    // Start is called before the first frame update
    void Start()
    {
        platformRB = movingPlatform.bc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if(other.gameObject.tag == "Player")
        {
            platformRB.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            platformRB.isTrigger = true;
        }
    }
}
