using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : WeaponInfo
{
    [SerializeField] private float heavyModificator = 0.5f;

    public override void SpawnBullet()
    {
        base.SpawnBullet();
    }

    public override void Equip()
    {
        Debug.Log("Equip Machinegun!");
        player.speed *= heavyModificator;
    }

    public override void Unequip()
    {
        Debug.Log("Unequip Machinegun!");
        player.speed /= heavyModificator;
    }
}
