using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject target; // what is the target
	public Vector2 startPos; // where is the bullet starting
	public Vector2 targetPos; // where is it going (i.e. the position of the target)

	public float speed; // how fast the bullet moves
	public int damage; // how much damage it does

	float dist; // how far between the bullet spawn and the target
	float startTime; // when the bullet spawned

	GameManager manager;

	void Start()
	{
		startTime = Time.time; // set the time when the bullet spawned
		dist = Vector2.Distance(startPos, targetPos); // set the distance

		manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update()
	{
		
	}
}
