using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounterScript : MonoBehaviour
{
    [SerializeField] int maxLives = 3;
    [SerializeField] public int currentLives;
    [SerializeField] private TMP_Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);
    }

    // Update is called once per frame
    void Update()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);
    }

    public void UpdateLivesUI()
    {
        livesText.text = "LIVES: " + currentLives;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("CurrentLives", currentLives);
    }
}
