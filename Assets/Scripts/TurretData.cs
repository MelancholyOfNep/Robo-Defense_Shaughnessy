using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretData : MonoBehaviour
{
    public int cost;
    public int lifetime;
    public float fireRate;

    [SerializeField]
    Image healthBar;

    private void Start()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(0.05f * lifetime, 0.05f);
    }

    private void Update()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(0.05f * lifetime, 0.05f);

        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
