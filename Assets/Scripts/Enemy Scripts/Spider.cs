using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private bool isWaiting = false;
    private Vector3 finalDestination;
    [SerializeField] private float timeForWaiting;
    [SerializeField] private float timeForRunning;

    private void Awake()
    {
        StartCoroutine(MovementCycle());
        finalDestination = NewPoint();
    }

    new void Update()
    {
        base.Update();
        MoveBehaviour();
    }

    private Vector3 NewPoint()
    {
        return new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
    }

    protected override void MoveBehaviour()
    {

        if (!isWaiting)
        {
            transform.Translate((player.transform.position + finalDestination - transform.position).normalized * speed * Time.deltaTime);
        }

    }

    protected override IEnumerator MovementCycle()
    {
        while (true)
        {
            isWaiting = false;
            yield return new WaitForSeconds(timeForRunning);
            isWaiting = true;
            finalDestination = NewPoint();
            yield return new WaitForSeconds(timeForWaiting);
        }
    }
}
