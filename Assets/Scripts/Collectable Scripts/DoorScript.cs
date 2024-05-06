using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public bool locked;
    [SerializeField] int sceneIndex;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        locked = true;
    }
    public void UnlockDoor()
    {
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && locked == false)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
