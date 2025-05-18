using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public bool isWaiting = false;
    public Vector3 finalDestination;
    [SerializeField] private float timeForWaiting;
    [SerializeField] private float timeForRunning;

    private void Awake()
    {
        movementStrategy = new SpiderMovement();
        StartCoroutine(MovementCycle());
        finalDestination = NewPoint();
    }

    new void Update()
    {
        base.Update();
        PerformMoveBehaviour();
    }

    private Vector3 NewPoint()
    {
        return new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
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
