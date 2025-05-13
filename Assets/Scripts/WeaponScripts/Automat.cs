using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automat : WeaponInfo
{


    [SerializeField] private float heavyModificator;

    public override void SpawnBullet()
    {
        base.SpawnBullet();
    }

    //public override void Equip()
    //{
    //    Debug.Log("Equip!");
    //    player.speed *= heavyModificator;
    //}

    //public override void Unequip()
    //{
    //    Debug.Log("Unequip!");
    //    player.speed *= heavyModificator;
    //}
}
