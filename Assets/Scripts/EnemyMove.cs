using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints;
    int activeWaypoint = 0;
    float lastWPToggleTime;
    public float moveSpeed = 1.0f;

    private void Start()
    {
        lastWPToggleTime = Time.time;
    }
    private void Update()
    {
        Vector3 startPos = waypoints[activeWaypoint].transform.position;
        Vector3 endPos = waypoints[activeWaypoint + 1].transform.position;

        float pathingLength = Vector3.Distance(startPos, endPos);
        float timeForPath = pathingLength / moveSpeed;
    }
}
