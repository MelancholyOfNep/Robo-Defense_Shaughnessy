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
	bool paused = false;
	public int money;
	public int wave;
	public int health;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI waveText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI costText;
	public TextMeshProUGUI spaceToCont;

	[SerializeField]
	GameObject pauseObj;

	private void Start()
	{
		money = 200;
		waveText.text = "Next Wave: " + (wave + 1);
		health = 9;
		healthText.text = "Health " + (health + 1) + "/10";
		costText.text = "None Selected";
	}

	private void Update()
	{
		moneyText.text = "Money:" + money;

		if(waveBreak)
        {
			spaceToCont.enabled = true;
        }

		if(Input.GetKeyDown(KeyCode.Space) && waveBreak)
		{
			spaceToCont.enabled = false;
			waveBreak = false;
			waveText.text = "Wave " + (wave + 1);
		}

		if (gameWin == true)
			SceneManager.LoadScene("VictoryScene"); // change this

		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (paused)
            {
				pauseObj.SetActive(false);
				Time.timeScale = 1.0f;
				paused = false;
            }
			else
            {
				pauseObj.SetActive(true);
				Time.timeScale = 0.0f;
				paused = true;
            }
        }
	}

	public void WaveUpdate()
	{
		waveText.text = "Next Wave: " + (wave + 1);
	}

	public void DecreaseHealth()
	{
		health -= 1;
		healthText.text = "Health: " + (health + 1) + "/10";

		if(health < 0)
			SceneManager.LoadScene("LossScene");
	}

	public void SelectedUnit(int selectedUnit)
	{
		if (selectedUnit == 0)
			costText.text = "Light Turret | 100";
		if (selectedUnit == 1)
			costText.text = "Twin Turret | 400";
	}
}
