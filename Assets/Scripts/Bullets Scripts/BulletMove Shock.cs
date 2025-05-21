using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletMoveShock : BulletBehavior
{
    private ShockWave shockWaveInfo;
    //private bool isExploding = false;
    private float shockWaveRadius;
    private int damage;
    [SerializeField] private int numberOfCharges = 3;

    new void Start()
    {
        shockWaveInfo = GameObject.Find("Shock WaveInfo").GetComponent<ShockWave>();
        shockWaveRadius = shockWaveInfo.shockWaveRadius;
        damage = shockWaveInfo.damage;
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            numberOfCharges--;
            Enemy enemyComponent = other.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.HP -= damage;
            }

            if (numberOfCharges > 0)
            {
                Collider[] collidersNearBy = Physics.OverlapSphere(transform.position, shockWaveRadius);
                foreach (Collider probablyEnemy in collidersNearBy)
                {
                    if (probablyEnemy.CompareTag("Enemy") && probablyEnemy != other)
                    {
                        transform.rotation = Quaternion.LookRotation(probablyEnemy.transform.position - transform.position);
                        break; 
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
