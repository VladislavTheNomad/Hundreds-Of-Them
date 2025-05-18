using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScore : BonusInfo
{
    [SerializeField] private InGameHelper gameHelper;
    public override void StartBonus()
    {
        gameHelper.isScoreDouble = true;
        StartCoroutine(TimeForBonus());
    }

    protected override void StopBonus()
    {
        gameHelper.isScoreDouble = false;
    }
}
