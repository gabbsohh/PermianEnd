using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public int Respawn;
    public PlayerHealth playerHealth;

    [SerializeField] private AudioClip hurtSoundClip;

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
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioManager.instance.PlaySoundFXClip(hurtSoundClip, transform, 0.5f);
            StartCoroutine(playerHealth.Die());
        }
    }
}
