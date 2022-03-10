using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;

    void Start() {
        target = Waypoints.waypoints[0];
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {
        if (wavePointIndex >= Waypoints.waypoints.Length -1) {
            Destroy (gameObject);
            return;
        }

        ++wavePointIndex;
       target = Waypoints.waypoints[wavePointIndex];
    }
}
