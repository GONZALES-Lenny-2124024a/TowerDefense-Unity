using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity requires")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;  //part to rotate on the turret
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int dropMoney;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //Invoke the UpdateTarget procedure every 0.5 second from the beginning of the game
    }
  
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Find the enemies with their tag

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)    //We compare them to take the nearest enemy
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //We calculate the distance between the enemy and the turret
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)  //If there is an enemy and the nearest enemy is in the turret's range
        {
            target = nearestEnemy.transform;    //We change the target
        } else {
            target = null;  //Else, there is no target
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null){return;}    //If there is no target, the turret don't make anything

        //Target lock on
        Vector3 dir = target.position - transform.position; //Obtain the direction to turn
        Quaternion lookRotation = Quaternion.LookRotation(dir); //Represents the rotation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;  //Convert Rotation to utilizable rotation + Quaternion.Lerp is used to a smooth turn
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);   //We apply the rotation on the object to rotate

        if (fireCountdown <= 0f) {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot() {
        GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.Seek(target);
        }
    }


    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);   //Draw a sphere around the object to see his range

    }
}
