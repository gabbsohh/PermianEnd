using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    public static CollectableCounter instance;

    public TMP_Text collectableText;
    public int currentCollectable = 0;

    public GameObject key;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Update()
    {
        collectableText.text = " " + GameData.collectables.ToString() + "/10";

        if (currentCollectable == 10 && key != null)
        {
            key.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void IncreaseCollectables(int v)
    {
        currentCollectable += v;
        
        collectableText.text = " " + currentCollectable.ToString();

        GameData.collectables += v;

    }

}
