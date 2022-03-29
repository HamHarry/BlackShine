using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject knifePrefab;

    public float knifeForce = 15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject knife = Instantiate(knifePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * knifeForce, ForceMode2D.Impulse);
    }
    
}
