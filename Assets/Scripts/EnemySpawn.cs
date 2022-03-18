using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
	public GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4;
	public float enemy1SpawnDelay = 2, enemy2SpawnDelay = 4, enemy3SpawnDelay, enemy4SpawnDelay;
	public int enemy1WaveCount, enemy2WaveCount, enemy3WaveCount, enemy4WaveCount;
}

public class EnemySpawn : MonoBehaviour
{
	public Wave[] waves;
	GameManager manager;

	public GameObject[] waypoints;

	[SerializeField]
	int currentEnemy1Count, currentEnemy2Count, currentEnemy3Count, currentEnemy4Count;
	float prevEnemy1SpawnTimer, prevEnemy2SpawnTimer, prevEnemy3SpawnTimer, prevEnemy4SpawnTimer;

	void Start()
	{
		prevEnemy1SpawnTimer = Time.time;
		prevEnemy2SpawnTimer = Time.time;
		prevEnemy3SpawnTimer = Time.time;
		prevEnemy4SpawnTimer = Time.time;

		manager = GameObject.Find("GameManager").GetComponent<GameManager>();

		// Instantiate(testEnemyPF).GetComponent<EnemyMove>().waypoints = waypoints; // remove later
	}

	private void Update()
	{
		int activeWave = manager.wave;

		if (activeWave < waves.Length)
		{
			if (manager.waveBreak == false)
			{
				float spawn1DelayTimer = Time.time - prevEnemy1SpawnTimer,
					spawn2DelayTimer = Time.time - prevEnemy2SpawnTimer,
					spawn3DelayTimer = Time.time - prevEnemy3SpawnTimer,
					spawn4DelayTimer = Time.time - prevEnemy4SpawnTimer;

				float spawn1Delay = waves[activeWave].enemy1SpawnDelay,
					spawn2Delay = waves[activeWave].enemy2SpawnDelay,
					spawn3Delay = waves[activeWave].enemy3SpawnDelay,
					spawn4Delay = waves[activeWave].enemy4SpawnDelay;

				if ((currentEnemy1Count == 0 || spawn1DelayTimer > spawn1Delay)
					&& currentEnemy1Count < waves[activeWave].enemy1WaveCount)
				{
					prevEnemy1SpawnTimer = Time.time;
					
					GameObject newEnemy1 = (GameObject)Instantiate(waves[activeWave].enemyPrefab1);
					newEnemy1.GetComponent<EnemyMove>().waypoints = waypoints;

					currentEnemy1Count++;
				}

				if ((currentEnemy2Count == 0 || spawn2DelayTimer > spawn2Delay)
					&& currentEnemy2Count < waves[activeWave].enemy2WaveCount)
				{
					prevEnemy2SpawnTimer = Time.time;

					GameObject newEnemy2 = (GameObject)Instantiate(waves[activeWave].enemyPrefab2);
					newEnemy2.GetComponent<EnemyMove>().waypoints = waypoints;

					currentEnemy2Count++;
				}

				if ((currentEnemy3Count == 0 || spawn3DelayTimer > spawn3Delay)
					&& currentEnemy3Count < waves[activeWave].enemy3WaveCount)
				{
					prevEnemy3SpawnTimer = Time.time;

					GameObject newEnemy3 = (GameObject)Instantiate(waves[activeWave].enemyPrefab3);
					newEnemy3.GetComponent<EnemyMove>().waypoints = waypoints;

					currentEnemy3Count++;
				}

				if ((currentEnemy4Count == 0 || spawn4DelayTimer > spawn4Delay)
					&& currentEnemy4Count < waves[activeWave].enemy4WaveCount)
				{
					prevEnemy4SpawnTimer = Time.time;

					GameObject newEnemy4 = (GameObject)Instantiate(waves[activeWave].enemyPrefab4);
					newEnemy4.GetComponent<EnemyMove>().waypoints = waypoints;

					currentEnemy4Count++;
				}

				if (currentEnemy1Count == waves[activeWave].enemy1WaveCount
					&& currentEnemy2Count == waves[activeWave].enemy2WaveCount
					&& currentEnemy3Count == waves[activeWave].enemy3WaveCount
					&& currentEnemy4Count == waves[activeWave].enemy4WaveCount
					&& GameObject.FindGameObjectWithTag("Enemy") == null)
				{
					// Debug.Log("Wave Over");
					manager.waveBreak = true;
					manager.wave++;
					manager.money += 25;
					manager.WaveUpdate();

					currentEnemy1Count = 0;
					currentEnemy2Count = 0;
					currentEnemy3Count = 0;
					currentEnemy4Count = 0;

					prevEnemy1SpawnTimer = Time.time;
					prevEnemy2SpawnTimer = Time.time;
					prevEnemy3SpawnTimer = Time.time;
					prevEnemy4SpawnTimer = Time.time;
				}
			}
		}
		else
        {
			manager.gameWin = true;
        }
	}
}
