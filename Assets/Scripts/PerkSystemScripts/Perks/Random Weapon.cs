using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class RandomWeapon : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    private GameObject[] weapons;

    private void Awake()
    {
        title = "Random Weapon";
        description = "Grants a random weapon.";
    }
    public override void ApplyPerk()
    {
        weapons = GameObject.FindGameObjectsWithTag("WeaponInfo");
        playerController.ChangeWeapon(weapons[Random.Range(0, weapons.Length)].name);
    }
}
