using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAndWeaponTimeOfLiving : MonoBehaviour
{
    [SerializeField] private float timeOfLiving = 5f;
    void Start()
    {
        StartCoroutine(LivingTime(timeOfLiving));
    }

    private IEnumerator LivingTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
