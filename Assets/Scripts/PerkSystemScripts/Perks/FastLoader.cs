using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FastLoader : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float reloadTimeModifier = 0.5f; // Decrease reload time 
    private GameObject[] weapons;

    private void Awake()
    {
        title = "FastLoader";
        description = "Speeds up weapon reloading.";
    }
    public override void ApplyPerk()
    {
        weapons = GameObject.FindGameObjectsWithTag("WeaponInfo");
        {
            foreach (var weapon in weapons)
            {
                weapon.GetComponent<WeaponInfo>().reloadTime *= reloadTimeModifier; // Decrease reload time
                if (weapon.GetComponent<WeaponInfo>().reloadTime < 0.1f)
                {
                    weapon.GetComponent<WeaponInfo>().reloadTime = 0.1f; // Set minimum reload time
                }
            }
        }
    }
}
