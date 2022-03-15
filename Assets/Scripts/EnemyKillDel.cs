using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillDel : MonoBehaviour
{
	public delegate void EnemyDel(GameObject enemy);
	public EnemyDel enemyDel;

	void OnDestroy()
	{
        enemyDel?.Invoke(gameObject);
    }
}
