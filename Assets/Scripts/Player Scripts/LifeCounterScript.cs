using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;


public class LifeCounterScript : MonoBehaviour
{
    [SerializeField] int maxLives = 3;
    [SerializeField] public int currentLives;
    [SerializeField] private TMP_Text livesText;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        currentLives = maxLives;

        playerHealth = FindObjectOfType<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivesUI();
    }

    public void UpdateLives()
    {
        if (currentLives > 0)
        {
            currentLives--;

            if (currentLives == 0)
            {
                playerHealth.isDead = true;
            }
        }
    }

    public void UpdateLivesUI()
    {
        livesText.text = "LIVES: " + currentLives;
    }
}
