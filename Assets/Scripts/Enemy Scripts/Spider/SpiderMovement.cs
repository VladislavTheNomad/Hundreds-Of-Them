using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : IMovementStrategy
{
    public void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController)
    {
        Spider spider = enemyController as Spider;
        if (spider == null || player == null || spider.isWaiting) return;

        enemyTransform.Translate((player.transform.position + spider.finalDestination - enemyTransform.position).normalized * speed * Time.deltaTime);
    }
}
