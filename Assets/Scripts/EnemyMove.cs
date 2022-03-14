using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	[HideInInspector]
	public GameObject[] waypoints;
	int activeWaypoint = 0;
	float lastWPToggleTime;
	public float moveSpeed = 0.25f;

	private void Start()
	{
		lastWPToggleTime = Time.time;
	}

	private void Update()
	{
		Vector2 startPos = waypoints[activeWaypoint].transform.position;
		Vector2 endPos = waypoints[activeWaypoint + 1].transform.position;

		float pathingLength = Vector2.Distance(startPos, endPos);
		float currentPathingTime = Time.time - lastWPToggleTime;
		float timeForPath = pathingLength / moveSpeed;
		

		gameObject.transform.position = Vector2.Lerp(startPos, endPos, currentPathingTime / timeForPath);

		if (gameObject.transform.position.Equals(endPos))
		{
			if (activeWaypoint < waypoints.Length - 2)
			{
				activeWaypoint++;
				lastWPToggleTime = Time.time;
				Rotate();
			}
			else
			{
				Destroy(gameObject);

				// AudioSource playerLoseHealth = gameObject.GetComponent<AudioSource>();
				// AudioSource.PlayClipAtPoint(playerLoseHealth.clip, transform.position);
				// TODO: take away health, get audio clip for player losing health
			}
		}
	}

	void Rotate()
	{
		// Tried to do this with the other startPos and endPos variables by prototyping them in the main,
		// using the Update to update them, and then calling them here. Didn't work, but for some reason, calling their contents works. idk.
		Vector2 rotEndPos = waypoints[activeWaypoint+1].transform.position;
		Vector2 rotStartPos = waypoints[activeWaypoint].transform.position;
		
		// find a vector facing in the new direction
		Vector2 rotation = (rotEndPos - rotStartPos);
		
		// get the x and y coords of the vector
		float x = rotation.x;
		float y = rotation.y;

		// find the angle direction of the new vector. atan2 gives the angle whose tangent is y/x. The rest of it converts it to degrees.
		float rotationAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // programmer's note: why can't they just make an Atan2 for degrees? It took me forever to figure this out. ;_;

		gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward); // Rotation of angle about axis, so here, rotation of angle around z
		
		// get the health bar, and rotate it back. Might end up making the enemy itself a child so I don't have to do this.
		GameObject healthBar = gameObject.transform.Find("Canvas").gameObject;
		healthBar.transform.rotation = Quaternion.AngleAxis(rotationAngle*4, Vector3.forward);
	}
}
