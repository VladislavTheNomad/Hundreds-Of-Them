using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastLegs : BonusInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float speedMultiplier = 1.5f;
    public override void StartBonus()
    {
        playerController.normalSpeed *= speedMultiplier;
        playerController.currentSpeed *= speedMultiplier;
        StartCoroutine(TimeForBonus());
    }

    protected override void StopBonus()
    {
        playerController.normalSpeed /= speedMultiplier;
        playerController.currentSpeed /= speedMultiplier;
    }
}
