using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWeaponScript : MonoBehaviour
{
    public PlayerCombat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check to see if player touches weapon.
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Picked up a weapon!");
            if(gameObject.tag == "Pickaxe")
            {
                playerCombat.weapons.Add("Pickaxe");
            }

            gameObject.SetActive(false);
        }
    }
}
