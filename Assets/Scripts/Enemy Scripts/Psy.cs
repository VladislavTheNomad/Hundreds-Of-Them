using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psy : Enemy
{

    [SerializeField] private float timeForWaiting;
    [SerializeField] private float rangeOfTeleportation;
 

    private void Awake()
    {
        StartCoroutine(MovementCycle());

    }

    new void Update()
    {
        base.Update();
        transform.LookAt(player.transform.position);
    }


    protected override void MoveBehaviour()
    {
        if (player == null)
        {
            return;
        }
     
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        transform.position += directionToPlayer * rangeOfTeleportation;


    }

    protected override IEnumerator MovementCycle()
    {
        while (true)
        {
            
            MoveBehaviour();
            yield return new WaitForSeconds(timeForWaiting);

        }
    }
}
