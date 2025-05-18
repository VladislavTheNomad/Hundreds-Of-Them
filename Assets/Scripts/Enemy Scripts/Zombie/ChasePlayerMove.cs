using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerMove : IMovementStrategy
{
    public void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController)
    {
        Zombie enemy = enemyController as Zombie;
        if (player == null || enemy == null) { return; }
        enemy.transform.LookAt(player.transform.position);
        enemy.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
