using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EagleEyes : PerkInfo
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float distance = 10f; // Increase distance of camera

    private void Awake()
    {
        title = "Eagle Eyes";
        description = "You look down on the world. Even higher!";
    }

    public override void ApplyPerk()
    {
        playerController.distanceofCameraByYAxis += distance; // Increase camera height
    }
}
