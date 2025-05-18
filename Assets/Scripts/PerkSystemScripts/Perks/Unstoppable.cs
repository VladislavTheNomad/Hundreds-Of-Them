using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstoppable : PerkInfo
{
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        title = "Unstoppable";
        description = "Prevents enemies from slowing the player down.";
    }
    public override void ApplyPerk()
    {
        playerController.isUnstoppablePerkIsTaken = true;
    }
}
