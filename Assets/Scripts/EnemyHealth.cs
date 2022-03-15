using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        // change current health

        if (currentHealth <= 0)
        {

        }
    }
}
