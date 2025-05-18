using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManiac : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int ammoCapacityIncrease = 5; // Increase ammo capacity in clips
    private GameObject[] weapons;

    private void Awake()
    {
        title = "Ammo Maniac";
        description = "Increases ammo capacity in clips.";
    }

    public override void ApplyPerk()
    {
        weapons = GameObject.FindGameObjectsWithTag("WeaponInfo");
        foreach (var weapon in weapons)
        {
            weapon.GetComponent<WeaponInfo>().ammoCapacity += ammoCapacityIncrease; // Increase ammo capacity in clips
            weapon.GetComponent<WeaponInfo>().ammoNow = weapon.GetComponent<WeaponInfo>().ammoCapacity; // Increase current ammo in clips
        }
    }
}
