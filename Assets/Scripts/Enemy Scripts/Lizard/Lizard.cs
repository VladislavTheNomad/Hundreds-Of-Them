using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Enemy
{
    [SerializeField] private float timeForWaiting;
    [SerializeField] private GameObject lizardBullet;
    public Vector3 finalDestination;
    public bool isWaiting;
    [SerializeField] private float timeForRunning;
    [SerializeField] private float timeForSpitting;
    [SerializeField] private float rechargeAttack;
    public bool isSpittngMode = false;

    private void Awake()
    {
        movementStrategy = new LizardMovementAndAttack();
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

    public IEnumerator Spit()
    {
        while (isSpittngMode)
        {
            transform.LookAt(player.transform.position);
            Instantiate(lizardBullet, transform.position + transform.forward * 0.5f + new Vector3(0, 0.5f, 0), transform.rotation);
            yield return new WaitForSeconds(rechargeAttack);
        }
    }
    

    protected override IEnumerator MovementCycle()
    {
        while (true)
        {
            int randomBehaviourDice = Random.Range(0, 2);
            switch (randomBehaviourDice)
            {
                case 0:
                    isWaiting = false;
                    yield return new WaitForSeconds(timeForRunning);
                    finalDestination = NewPoint();
                    break;
                case 1:
                    isWaiting = true;
                    yield return new WaitForSeconds(timeForSpitting);
                    isSpittngMode = false;
                    break;
                default:
                    break;
            }
        }
    }
}
