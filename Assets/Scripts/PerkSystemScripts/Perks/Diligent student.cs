using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diligentstudent : PerkInfo
{
    [SerializeField] private float extraEXPModifier = 1.3f; // Extra EXP gained
    [SerializeField] private EXPCounter _EXPCounter;

    private void Awake()
    {
        title = "Diligent student";
        description = "You get 30% more experience (but not points).";
    }
    public override void ApplyPerk()
    {
        _EXPCounter.GetComponent<EXPCounter>().extraEXPModifier = extraEXPModifier;
    }
}
