using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BonusInfo
{
    [SerializeField] private PlayerController player;
    public override void StartBonus()
    {
        bool hasEmptyBonusSlot = gameHelper.DisplayBonus("Heal", timeBonus);
        if (hasEmptyBonusSlot)
        {
            player.HP += 20;
            if (player.HP > 100)
            {
                player.HP = 100;
            }
            gameHelper.UpdatePlayerHP(player.HP);
        }
    }

    protected override void StopBonus()
    {
        
    }
}
