using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableCounter : MonoBehaviour
{
    public static CollectableCounter instance;
    SceneManager sceneManager;

    public TMP_Text collectableText;
    public TMP_Text questionText;
    public int currentCollectable = 0;

    public GameObject key;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Update()
    {
        counterText();

        if (currentCollectable == 10 && key != null)
        {
            key.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void IncreaseCollectables(int v)
    {
        currentCollectable += v;
        
        //collectableText.text = " " + currentCollectable.ToString() + "/10";

        GameData.collectables += v;

    }

    public void counterText()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            questionText.text = ""; 
            collectableText.text = " " + GameData.collectables.ToString() + "/10";
        }
        else questionText.text = " " + currentCollectable.ToString() + " /?";
    }

}
