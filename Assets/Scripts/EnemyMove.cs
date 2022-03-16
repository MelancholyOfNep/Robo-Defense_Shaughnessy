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
		lastWPToggleTime = Time.time; // the time when the previous waypoint was hit
		gameObject.transform.position = waypoints[0].transform.position;
	}

	private void Update()
	{
		Vector2 startPos = waypoints[activeWaypoint].transform.position; // the waypoint the enemy just hit
		Vector2 endPos = waypoints[activeWaypoint + 1].transform.position; // the waypoint the enemy will hit next

		float pathingLength = Vector2.Distance(startPos, endPos); // the distance between startPos and endPos
		float currentPathingTime = Time.time - lastWPToggleTime; // the amount of time since the last waypoint was hit
		float timeForPath = pathingLength / moveSpeed; // the amount of time the pathing will take, considering the path's length and the move speed
		

		gameObject.transform.position = Vector2.Lerp(startPos, endPos, currentPathingTime / timeForPath); // interpolate between the enemy and the next location based on how far through the path the enemy is

		if (gameObject.transform.position.Equals(endPos)) // if the enemy is at the endPos
		{
			if (activeWaypoint < waypoints.Length - 2) // and there are still waypoints left
			{
				activeWaypoint++; // go to the next one
				lastWPToggleTime = Time.time; // reset the time since hitting the last waypoint
				Rotate(); // change rotation if needed
			}
			else // if there aren't any waypoints left
			{
                GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>(); // get the GameManager
				manager.DecreaseHealth(); // decrease the player's lives

				// AudioSource playerLoseHealth = gameObject.GetComponent<AudioSource>();
				// AudioSource.PlayClipAtPoint(playerLoseHealth.clip, transform.position);
				// TODO: get audio clip for player losing health

				Destroy(gameObject); // destroy the enemy
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

		// find the angle direction of the new vector. atan2 gives the angle whose tangent is y/x.
		// The rest of it converts it to degrees.
		// programmer's note: why can't they just make an Atan2 for degrees? Or just an easier way of doing this?
		// PN cont: It took me forever to figure this out. ;_;
		float rotationAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

		gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward); // Rotation of angle about axis, so here, rotation of angle around z
		
		// get the health bar, and rotate it back. Might end up making the enemy itself a child so I don't have to do this.
		GameObject healthBar = gameObject.transform.Find("Canvas").gameObject;
		healthBar.transform.rotation = Quaternion.AngleAxis(rotationAngle*4, Vector3.forward);
	}

	//function to see how far the enemy is from the goal, so turrets know which to shoot first.\
	public float Progress()
    {
		// the distance the enemy must travel to reach the goal
		float goalDist = 0;
		
		// add the distance between the enemy and the next waypoint to the goalDist
		goalDist += Vector2.Distance(gameObject.transform.position, waypoints[activeWaypoint + 1].transform.position);
		
		// add the distance between the rest of the waypoints to the goalDist
		for (int i = activeWaypoint+1; i < waypoints.Length - 1; i++)
        {
			Vector2 startPos = waypoints[i].transform.position;
			Vector2 endPos = waypoints[i + 1].transform.position;
			goalDist += Vector2.Distance(startPos, endPos);
        }
		//return the goalDist
		return goalDist;
    }
}
