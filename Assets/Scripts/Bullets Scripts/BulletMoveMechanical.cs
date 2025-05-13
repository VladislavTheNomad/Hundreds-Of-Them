using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveMechanical : BulletBehavior
{
    private Light flareBullet;

    new void Start()
    {
        flareBullet = GetComponent<Light>();
    }

    private void LateUpdate()
    {
        flareBullet.range -= 1f;
        flareBullet.intensity -= 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
