using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamage : BonusInfo, IPlayerChangedWeapon
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int damageMultiplier = 2;

    private void Awake()
    {
        playerController.AddObserver(this);
    }
    public override void StartBonus()
    {
        playerController.weapon.damagePerBullet *= damageMultiplier;
        StartCoroutine(TimeForBonus());
    }

    protected override void StopBonus()
    {
        playerController.weapon.damagePerBullet /= damageMultiplier;
    }

    public void OnPlayerChangedWeapon(string weaponName)
    {
        playerController.weapon.damagePerBullet /= damageMultiplier;
        StopCoroutine(TimeForBonus());
    }
}
