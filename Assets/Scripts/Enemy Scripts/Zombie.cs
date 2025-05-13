using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    new void Update()
    {
        base.Update();
        MoveBehaviour();
    }

    void LateUpdate()
    {
        transform.LookAt(player.transform.position);
    }
    protected override void MoveBehaviour()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    
    }

    protected override IEnumerator MovementCycle()
    {
        return null;
    }
}
