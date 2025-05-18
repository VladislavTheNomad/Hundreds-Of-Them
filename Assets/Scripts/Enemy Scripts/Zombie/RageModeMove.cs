using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageModeMove : IMovementStrategy
{
    private float _speedModificator = 3f;
    public void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController)
    {
        Zombie enemy = enemyController as Zombie;
        if (player == null || enemy == null) { return; }
        enemy.transform.LookAt(player.transform.position);
        enemy.transform.Translate(Vector3.forward * speed * _speedModificator * Time.deltaTime);
    }
}
