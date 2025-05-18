using UnityEngine;

public interface IMovementStrategy
{
    // enemyController может понадобиться для доступа к специфичным полям врага
    // (например, finalDestination для паука) или для изменения состояния врага.
    void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController);
}