using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Attributes")]

    public float speed = 15f;
    public int health = 100;

    [Header("Unity Requires")]

    public string turretTag = "Turret";
    private int wavePointIndex = 0;

    private Transform target;

    public GameObject deathEffect;
    public int dropMoney;    

    void Start() {
        target = Waypoints.waypoints[0];

        //My part
        if (targetTurret == null) { InvokeRepeating("UpdateTarget",0f,0.5f);}    //Invoke the UpdateTarget procedure every 0.5 second from the beginning of the game if there is no turret
    }

    public void TakeDamage(int amount) {
        health -= amount;

        if(health <= 0) {
            Die();
        }
    }

    void Die() {
        PlayerStats.Money += dropMoney;
        Debug.Log("Money: " + PlayerStats.Money);

        GameObject effect = (GameObject) Instantiate(deathEffect,transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        Destroy(gameObject);
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }

        //My part
        if (targetTurret == null) { return; }   //If there is no target

        if (fireCountdown <= 0f) {  //If the enemy can shoot
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void GetNextWaypoint() {
        if (wavePointIndex >= Waypoints.waypoints.Length -1) {  //After the last waypoint, we destroy the enemy [for now]
            EndPath();
            return;
        }

        ++wavePointIndex;
       target = Waypoints.waypoints[wavePointIndex];
    }

    void EndPath() {
        --PlayerStats.Lives;
        Destroy(gameObject);
    }


    //My part
    [Header("My Part")]

    public float range = 10f;
    public int damage;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public Vector3 positionOffSet;

    public GameObject bulletPrefab;
    private Transform targetTurret = null;

    void UpdateTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTurret = null;

        foreach(GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
            if(distanceToTurret < shortestDistance)
            {
                shortestDistance = distanceToTurret;
                nearestTurret = turret;
            }

            if (nearestTurret != null && shortestDistance <= range)
            {
                targetTurret = nearestTurret.transform;
            } else {
                targetTurret = null;
            }
        }
    }

    void Shoot () {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null) {
            bullet.Seek(targetTurret);
            bullet.damage = damage;
        }
    }


    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);   //Draw a sphere around the object to see his range

    }

    
    
}
