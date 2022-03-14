using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI moneyLabel;
    public int money;

    private void Start()
    {
        money = 200;
    }

    private void Update()
    {
        moneyLabel.text = "Money:" + money;
    }
}
