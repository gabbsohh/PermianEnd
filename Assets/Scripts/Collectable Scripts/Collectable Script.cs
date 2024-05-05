using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public int value;

    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        { 
            collected = true;
            CollectableCounter.instance.IncreaseCollectables(value);
            gameObject.SetActive(false);
        }
    }
}
