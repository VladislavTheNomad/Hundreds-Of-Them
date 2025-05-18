using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BonusInfo
{
    [SerializeField] private PlayerController playerController;
    public override void StartBonus()
    {
        playerController.isShield = true;
        StartCoroutine(TimeForBonus());
    }

    protected override void StopBonus()
    {
        playerController.isShield = false;
    }

}
