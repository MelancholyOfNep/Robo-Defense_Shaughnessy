using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Image healthBar;

    public float maxHealth;
    public float currentHealth;

    [SerializeField]
    int reward;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(0.5f * (maxHealth/100), 0.05f);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.rectTransform.sizeDelta = new Vector2 (0.5f * (currentHealth/100), 0.05f);
        // change current health

        if (currentHealth <= 0)
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.money += reward;
            // Give points
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            for (int i = 0; i<turrets.Length;i++)
            {
                TurretShoot turretShoot = turrets[i].GetComponent<TurretShoot>();
                turretShoot.EnemyDestroyed(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
