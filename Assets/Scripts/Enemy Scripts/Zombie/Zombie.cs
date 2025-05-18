using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private float rageModeDistance = 5f;
    private void Awake()
    {
        movementStrategy = new ChasePlayerMove();
    }
    new void Update()
    {
        base.Update();
        PerformMoveBehaviour();
        if ((player.transform.position - transform.position).magnitude < rageModeDistance)
        {
            movementStrategy = new RageModeMove();
        }
        else
        {
            movementStrategy = new ChasePlayerMove();
        }
    }

    protected override IEnumerator MovementCycle()
    {
        return null;
    }

}
