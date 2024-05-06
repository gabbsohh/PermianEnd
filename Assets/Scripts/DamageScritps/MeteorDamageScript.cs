using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDamageScript : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public FallingMeteorScript fallingMeteorScript;
    public GameObject meteorTriggerArea;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0,3,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            playerHealth.GetHurt(1);
        }

        //fallingMeteorScript.ResetMeteor();
        meteorTriggerArea.GetComponent<FallingMeteorScript>().ResetMeteor();
    }
}
