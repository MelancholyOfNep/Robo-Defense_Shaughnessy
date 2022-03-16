using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float shotSpeed;
	public float damage;
	public Rigidbody2D rb;

	Vector2 moveDir;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Fire(GameObject enemy)
    {
		Vector3 enemyPos = (enemy.transform.position - transform.position).normalized * 10;
		moveDir = enemyPos;
		rb.velocity = new Vector2(moveDir.x, moveDir.y).normalized * shotSpeed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
			EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
			enemyHealth.currentHealth -= damage;

			Destroy(gameObject);
        }

		Destroy(gameObject);
    }
}
