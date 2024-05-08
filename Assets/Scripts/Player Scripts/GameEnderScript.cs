using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnderScript : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int SceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(TheInevitable());
        }
    }

    IEnumerator TheInevitable()
    {
        yield return new WaitForSeconds(1);
        playerMovement.speed = 0f;
        playerMovement.jump = 0f;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(SceneIndex);
    }
}
