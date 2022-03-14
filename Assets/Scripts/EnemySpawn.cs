using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject[] waypoints;
	public GameObject testEnemyPF; // change later

	// Start is called before the first frame update
	void Start()
	{
		Instantiate(testEnemyPF).GetComponent<EnemyMove>().waypoints = waypoints; // remove later
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
