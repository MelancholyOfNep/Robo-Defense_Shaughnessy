using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
	public List<GameObject> enemiesClose; // list of targets within range

	void Start()
	{
		enemiesClose = new List<GameObject>(); // initialize the list
	}
	
	void Update()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision) // when detecting something entering range...
	{
		if (collision.gameObject.tag.Equals("Enemy")) // if it's an enemy...
		{
			enemiesClose.Add(collision.gameObject); // adds it to the list of targets in range

			EnemyKillDel killDel = collision.gameObject.GetComponent<EnemyKillDel>(); // opens the enemy's kill delegate
			killDel.enemyDel += EnemyDestroyed; // adds the OnEnemyDestroyed function to the delegate, so when the enemy dies, it calls the function
		}
	}

    private void OnTriggerExit2D(Collider2D collision) // when detecting something leaving range...
    {
		if (collision.gameObject.tag.Equals("Enemy")) // if it's an enemy...
        {
			enemiesClose.Remove(collision.gameObject); // removes it from the list of targets in range

			EnemyKillDel killDel = collision.gameObject.GetComponent<EnemyKillDel>(); // opens the enemy's kill delegate
			killDel.enemyDel -= EnemyDestroyed; // removes the function from the delegate
        }
    }

	void EnemyDestroyed(GameObject enemy)
	{
		enemiesClose.Remove(enemy);
	}
}