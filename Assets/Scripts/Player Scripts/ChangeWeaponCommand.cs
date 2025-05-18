using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponCommand : ICommand
{
    private PlayerController player;
    private readonly string _weaponName;
    private readonly Collider _weaponObject;
    public ChangeWeaponCommand(PlayerController player, string weaponName, Collider weaponObject = null)
    {
        this.player = player;
        _weaponName = weaponName;
        _weaponObject = weaponObject;
    }
    public void Execute()
    {
        player.PlayerChangedWeapon();
        if (_weaponObject != null)
        {
            player.weapon.Unequip();
        }
        WeaponInfo weapon = GameObject.Find(_weaponName).GetComponent<WeaponInfo>();
        player.weapon = weapon;
        player.weapon.Equip(player.normalSpeed);
        player.isReload = false;
        player.reloadText.SetActive(false);
        player.shotSound.clip = weapon.specificShotSound;
        // patrons for now UI
        if (player.numberOfPatronsText != null)
        {
            player.numberOfPatronsText.text = weapon.ammoNow.ToString();
        }
    }
}
