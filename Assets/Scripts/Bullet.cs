using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public GameObject impactEffect;

    public void Seek (Transform _target) {  //Recovering the target from 'Turret.cs'
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position; //The direction
        float distanceThisFrame = speed * Time.deltaTime; //The distance we are going to travel 

        if (dir.magnitude <= distanceThisFrame) {   //dir.magnitude is equals to the length of the distance
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() {
        GameObject effectIns = (GameObject) Instantiate (impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
