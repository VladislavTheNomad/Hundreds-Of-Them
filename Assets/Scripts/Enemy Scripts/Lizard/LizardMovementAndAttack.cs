using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardMovementAndAttack : IMovementStrategy
{
    public void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController)
    {
        Lizard enemy = enemyController as Lizard;
        if (enemy == null || player == null) { return;}
        if (!enemy.isWaiting)
        {
            enemy.transform.Translate((player.transform.position + enemy.finalDestination - enemy.transform.position).normalized * speed * Time.deltaTime);
        }

        if (enemy.isWaiting && !enemy.isSpittngMode)
        {
            enemy.isSpittngMode = true;
            enemy.StartCoroutine(enemy.Spit());
        }
    }
}
