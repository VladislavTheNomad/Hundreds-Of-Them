using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceRunning : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float speedModificator = 0.5f; // Increase movement speed

    private void Awake()
    {
        title = "Long Distance Runner";
        description = "Increases movement speed the longer you run without stopping.";
    }
    public override void ApplyPerk()
    {
        playerController.speedModificator = speedModificator; // Increase movement speed
    }
}
