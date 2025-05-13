using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLizard : BulletBehavior
{
    public int damage = 10; // Damage value for the bullet


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
