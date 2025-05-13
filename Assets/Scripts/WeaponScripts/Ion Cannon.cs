using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCannon : WeaponInfo
{
    public override void SpawnBullet()
    {
        base.SpawnBullet();
    }

    //public override void IonDamage()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, rangeOfIonOrExplosionDamage);
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.CompareTag("Enemy"))
    //        {
    //            Enemy enemy = collider.GetComponent<Enemy>();
    //            if (enemy != null)
    //            {
    //                enemy.HP -= damagePerBullet;
    //            }
    //        }
    //    }
    //}
}
