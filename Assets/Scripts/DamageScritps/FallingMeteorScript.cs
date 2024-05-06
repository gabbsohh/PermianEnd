using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMeteorScript : MonoBehaviour
{
    public GameObject meteor;
    public GameObject meteorPos;
    public BoxCollider2D meteorTriggerArea;

    [SerializeField] private AudioClip meteorSoundClip;

    public Rigidbody2D meteorRB;

    public GameObject player;

    private bool canCreateMeteor = false;
    public float meteorTimer = 7.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");      
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > meteorTimer)
        {
            canCreateMeteor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if(other.gameObject.tag == "Player")
        {
            CreateMeteor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer = 0;
        }
    }

    public void CreateMeteor()
    {
        if(canCreateMeteor == true)
        {
            meteorRB.constraints = RigidbodyConstraints2D.None;
            meteorRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            Instantiate(meteor, meteorPos.transform.position, Quaternion.identity);
            AudioManager.instance.PlaySoundFXClip(meteorSoundClip, transform, 0.5f);
            canCreateMeteor = false;      
        }
    }

    public void ResetMeteor()
    {
        meteorRB.constraints = RigidbodyConstraints2D.FreezeAll;
        meteor.transform.position = meteorPos.transform.position;
    }
}