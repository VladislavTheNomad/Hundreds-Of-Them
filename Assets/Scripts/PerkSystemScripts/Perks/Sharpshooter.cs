using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpshooter : PerkInfo
{
    [SerializeField] private float bulletSpreadModifier = -0.1f;
    [SerializeField] private float fireRateModifier = 1.1f;
    [SerializeField] private PlayerController playerController;
    private GameObject[] weapons;

    private void Awake()
    {
        title = "Sharpshooter";
        description = "Highly improves aiming but slightly slows down firing speed.";
    }
    public override void ApplyPerk()
    {
        weapons = GameObject.FindGameObjectsWithTag("WeaponInfo");
        foreach (var weapon in weapons)
        {
            weapon.GetComponent<WeaponInfo>().bulletSpreadModifier += bulletSpreadModifier; // Increase bullet spread
            weapon.GetComponent<WeaponInfo>().fireRate *= fireRateModifier; // Increase fire rate
            if (weapon.GetComponent<WeaponInfo>().bulletSpreadModifier < 0f)
            {
                weapon.GetComponent<WeaponInfo>().bulletSpreadModifier = 0f;
            }
            if (weapon.GetComponent<WeaponInfo>().fireRate < 0.08f)
            {
                weapon.GetComponent<WeaponInfo>().fireRate = 0.08f;
            }
        }
    }
}
