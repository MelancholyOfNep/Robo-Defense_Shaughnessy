using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
	public List<GameObject> enemiesClose; // list of targets within range

	float cooldown;
	TurretData turretData;

	[SerializeField]
	GameObject bulletPF, gun1, gun2;

	[SerializeField]
	int turretType;

	void Start()
	{
		enemiesClose = new List<GameObject>(); // initialize the list
		turretData = gameObject.GetComponent<TurretData>();
	}
	
	void Update()
	{
		GameObject target = null;
		float closestEnemyDist= 100f;

		for(int i = 0; i < enemiesClose.Count; i++)
		{
			float progress = enemiesClose[i].GetComponent<EnemyMove>().Progress();
			
			if (progress < closestEnemyDist)
			{
				target = enemiesClose[i];
				closestEnemyDist = progress;
			}
		}

		if (target != null)
		{
			if (Time.time - cooldown > turretData.fireRate)
			{
				Shoot(target.GetComponent<Collider2D>()); // here we have to shoot, and we have to make it lock to the target.
				cooldown = Time.time;
			}

			Vector2 rot = gameObject.transform.position - target.transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg + 90, Vector3.forward);
		}
	}

	void Shoot(Collider2D target)
	{
		turretData.lifetime--;

		GameObject newBullet = (GameObject)Instantiate(bulletPF, gun1.transform.position, Quaternion.identity);
		Bullet bulletBehavior = newBullet.GetComponent<Bullet>();
		bulletBehavior.Fire(target.gameObject);

		if (turretType == 1)
		{
			GameObject newBullet2 = (GameObject)Instantiate(bulletPF, gun2.transform.position, Quaternion.identity); // might need to change this.
			Bullet bulletBehavior2 = newBullet2.GetComponent<Bullet>();
			bulletBehavior2.Fire(target.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) // when detecting something entering range...
	{
		if (collision.gameObject.CompareTag("Enemy")) // if it's an enemy...
		{
			enemiesClose.Add(collision.gameObject); // adds it to the list of targets in range

			// EnemyKillDel killDel = collision.gameObject.GetComponent<EnemyKillDel>(); // opens the enemy's kill delegate
			// killDel.enemyDel += EnemyDestroyed; // adds the OnEnemyDestroyed function to the delegate, so when the enemy dies, it calls the function
		}
	}

	private void OnTriggerExit2D(Collider2D collision) // when detecting something leaving range...
	{
		if (collision.gameObject.CompareTag("Enemy")) // if it's an enemy...
		{
			enemiesClose.Remove(collision.gameObject); // removes it from the list of targets in range

			// EnemyKillDel killDel = collision.gameObject.GetComponent<EnemyKillDel>(); // opens the enemy's kill delegate
			// killDel.enemyDel -= EnemyDestroyed; // removes the function from the delegate
		}
	}

	public void EnemyDestroyed(GameObject enemy) // added call in enemy health
	{
		enemiesClose.Remove(enemy);
	}
}