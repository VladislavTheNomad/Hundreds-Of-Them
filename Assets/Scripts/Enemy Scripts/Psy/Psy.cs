using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psy : Enemy
{
    [SerializeField] private float timeForWaiting;
    public float rangeOfTeleportation;
    private ParticleSystem particleSystemObject;
    private Light lightObject;
    [SerializeField] private AudioClip teleportSound;


    private void Awake()
    {
        movementStrategy = new TeleportMovement();
        particleSystemObject = GetComponentInChildren<ParticleSystem>();
        lightObject = GetComponentInChildren<Light>();

        StartCoroutine(MovementCycle());
    }

    new void Update()
    {
        base.Update();
        transform.LookAt(player.transform.position);
    }

    protected override IEnumerator MovementCycle()
    {
        var emission = particleSystemObject.emission; // Access the EmissionModule of the ParticleSystem to modify emission rate
        while (true)
        {
            yield return new WaitForSeconds(timeForWaiting);

            float teleportAnimationDuration = timeForWaiting;
            float teleportAnimationDurationFading = 0.2f;
            float elapsedTime = 0f;
            while (teleportAnimationDuration > elapsedTime)
            {
                elapsedTime += Time.deltaTime;
                emission.rateOverTime = Mathf.Lerp(0, 2000, elapsedTime / teleportAnimationDuration);
                lightObject.intensity = Mathf.Lerp(0, 5, elapsedTime / teleportAnimationDuration);
                yield return null; // <--- ¬от это приостанавливает выполнение на кадр
            }
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
            PerformMoveBehaviour();
            elapsedTime = 0f;
            while (teleportAnimationDurationFading > elapsedTime)
            {
                elapsedTime += Time.deltaTime;
                emission.rateOverTime = Mathf.Lerp(2000, 0, elapsedTime / teleportAnimationDurationFading);
                lightObject.intensity = Mathf.Lerp(5, 0, elapsedTime / teleportAnimationDurationFading);
                yield return null; // <--- ¬от это приостанавливает выполнение на кадр
            }
        }
    }
}
