using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class NuclearBlast : BonusInfo
{
    private Light explosionLight;
    [SerializeField] private GameObject explosionPoint;
    [SerializeField] private GameObject player;
    private GameObject explosionPointInstance;
    [SerializeField] private AudioClip explosionSound;
    public override void StartBonus()
    {
        AudioSource.PlayClipAtPoint(explosionSound, player.transform.position);
        explosionPointInstance = Instantiate(explosionPoint, player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        explosionLight = explosionPointInstance.GetComponent<Light>();
        StartCoroutine(Exploding());
    }

    private IEnumerator Exploding()
    {
        float duration = 0.5f;
        float durationFading = 0.15f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            explosionLight.intensity = Mathf.Lerp(0f, 100f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Apply damage to enemies in the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPointInstance.transform.position, 20f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
        // Wait for a short duration before fading out the light
        elapsedTime = 0f;
        while (elapsedTime < durationFading)
        {
            explosionLight.intensity = Mathf.Lerp(100f, 0f, elapsedTime / durationFading);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(explosionPointInstance);
    }

    protected override void StopBonus()
    {
    }
}
