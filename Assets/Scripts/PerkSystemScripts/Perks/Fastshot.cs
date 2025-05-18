using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastshot : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float fireRateModifier = 0.8f; // increase fire rate by 20%
    private GameObject[] weapons;

    private void Awake()
    {
        title = "Fastshot";
        description = "Increases firing speed.";
    }
    public override void ApplyPerk()
    {
        weapons = GameObject.FindGameObjectsWithTag("WeaponInfo");
        foreach (var weapon in weapons)
        {
            weapon.GetComponent<WeaponInfo>().fireRate *= fireRateModifier; // Increase fire rate
            if(weapon.GetComponent<WeaponInfo>().fireRate < 0.1f)
            {
                weapon.GetComponent<WeaponInfo>().fireRate = 0.1f; // Set a minimum fire rate
            }
        }
    }
}
