using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveImpulse : BulletBehavior
{
    [SerializeField] private float pushPower = 1f;

    protected new void Update()
    {
        base.Update();
        transform.localScale += new Vector3(1.5f, 0, -0.05f) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 directionFromBullet = (other.transform.position - transform.position).normalized;
            directionFromBullet.y = 0; // Ignore vertical direction
            other.transform.position += directionFromBullet * pushPower;
            Destroy(gameObject);
        }
    }
}
