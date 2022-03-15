using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public bool gameWin = false;
	public bool waveBreak = true;
	public int money;
	public int wave;
	public int health;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI waveText;
	public TextMeshProUGUI healthText;

	private void Start()
	{
		money = 200;
		waveText.text = "Next Wave: " + (wave + 1);
		health = 9;
		healthText.text = "Health " + (health + 1);
	}

	private void Update()
	{
		moneyText.text = "Money:" + money;

		if(Input.GetKeyDown(KeyCode.Space) && waveBreak)
        {
			waveBreak = false;
			waveText.text = "Wave " + (wave + 1);
		}

		if (gameWin == true)
        {
			SceneManager.LoadScene("MainMenuScene"); // change this
        }
    }

	public void WaveUpdate()
    {
		waveText.text = "Next Wave: " + (wave + 1);
	}

	public void DecreaseHealth()
    {
		health -= 1;
		healthText.text = "Health: " + (health + 1);
    }
}
