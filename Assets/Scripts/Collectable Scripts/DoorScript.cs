using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public bool locked;

    [SerializeField] GameObject player;

    private int currentSceneIndex;

    // Start is called before the first frame update
    private void Start()
    {
        locked = true;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void UnlockDoor()
    {
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (!locked && distance < 0.5f)
        {
            //new WaitForSeconds(1);
            Debug.Log("Go to next scene");
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !locked)
        {
            Debug.Log("Player collided with door");
            locked = false;
        }
    }
}
