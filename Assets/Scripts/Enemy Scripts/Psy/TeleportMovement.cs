using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : IMovementStrategy
{
    public void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController)
    {
        Psy enemy = enemyController as Psy;
        if (enemy == null || player == null) { return; }
        Vector3 directionToPlayer = (player.transform.position - enemyTransform.position).normalized;
        enemy.transform.position = player.transform.position - directionToPlayer * enemy.rangeOfTeleportation;
    }


}
