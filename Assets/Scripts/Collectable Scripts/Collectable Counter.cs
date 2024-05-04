using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    public static CollectableCounter instance;

    public TMP_Text collectableText;
    public int currentCollectable = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        collectableText.text = "COLLECTABLES: " + GameData.collectables.ToString();   
    }

    // Update is called once per frame
    public void IncreaseCollectables(int v)
    {
        currentCollectable += v;
        
        collectableText.text = "COLLECTABLES: " + currentCollectable.ToString();

        GameData.collectables += v;

    }

}
