using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : WeaponInfo
{
    [SerializeField] private ParticleSystem flameThrowerInfo;

    void Start()
    {
        GameObject firethrowerObject = GameObject.Find("FirethrowerParticles");
        if (firethrowerObject != null)
        {
            flameThrowerInfo = firethrowerObject.GetComponent<ParticleSystem>();
        }
    }

    protected override IEnumerator SpitBullets()
    {
        isFiringNow = false;
        while (Input.GetMouseButton(0) && ammoNow > 0)
        {
            if (!isFiringNow && ammoNow != 0)
            {
                isFiringNow = true;
                StartCoroutine(StartFire());
            }
            ammoNow--;
            if (numberOfPatronsText != null)
            {
                numberOfPatronsText.text = ammoNow.ToString();
            }
            if (ammoNow <= 0)
            {
                isFiringNow = false;
                StartCoroutine(Reload());
            }
            else { yield return new WaitForSeconds(fireRate); }
        }
        StartCoroutine(StopFire());
        isFiringNow = false;
    }

    IEnumerator StopFire()
    {
        player.shotSound.Stop();
        var emissionModule = flameThrowerInfo.emission; // Store the emission module in a variable
        while (emissionModule.rateOverTime.constant > 0)
        {
            var rateOverTime = emissionModule.rateOverTime; // Получаете *копию* структуры rateOverTime
            rateOverTime.constant -= 300;                 // Изменяете *локальную копию*
            emissionModule.rateOverTime = rateOverTime;     // Присваиваете измененную *копию* обратно
            yield return new WaitForSeconds(0.01f);
        }
        emissionModule.rateOverTime = 0; // Устанавливаем значение в 0, чтобы избежать ошибок
    }

    IEnumerator StartFire()
    {
        player.shotSound.Play();
        var emissionModule = flameThrowerInfo.emission; // Store the emission module in a variable
        while (emissionModule.rateOverTime.constant < 3000)
        {
            var rateOverTime = emissionModule.rateOverTime; // Получаете *копию* структуры rateOverTime
            rateOverTime.constant += 300;                 // Изменяете *локальную копию*
            emissionModule.rateOverTime = rateOverTime;     // Присваиваете измененную *копию* обратно
            yield return new WaitForSeconds(0.01f);
        }
        emissionModule.rateOverTime = 3000; // Устанавливаем значение в 3000, чтобы избежать ошибок
    }
}
