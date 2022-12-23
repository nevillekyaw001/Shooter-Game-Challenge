using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate =15f;
    float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; //Gun in Automatic mode
            Shoot();
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            AIShooter AI = hit.transform.GetComponent<AIShooter>(); //Reduce AI enemy health
            if(AI != null)
            {
                AI.TakeDamage(damage);
            }

            Enemy Boxes = hit.transform.GetComponent<Enemy>(); //Reduce Boxes Health
            if (Boxes != null)
            {
                Boxes.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
