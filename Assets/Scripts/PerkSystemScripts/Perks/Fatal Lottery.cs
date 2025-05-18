using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FatalLottery : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EXPCounter expCounter;
    [SerializeField] private int expGain = 10000;

    private void Awake()
    {
        title = "Fatal Lottery";
        description = "A 50/50 chance of either dying or gaining A lot of experience points.";
    }
    public override void ApplyPerk()
    {
        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            expCounter.ApplyFatalLottery(expGain);
        }
        else
        {
            playerController.HP = 0;
        }
    }
}
