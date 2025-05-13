using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : WeaponInfo
{
    [SerializeField] private float timeBetweenShots = 0.1f;
    public override void SpawnBullet()
    {
        StartCoroutine(SpitOfRockets());
    }

    public IEnumerator SpitOfRockets()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int alredyFiredBullets = 0;
            while (ammoNow > 0)
            {
                player.bulletDeflectionDiameter += alredyFiredBullets * bulletSpreadModifier;
                Vector3 directionFromPlayer = hit.point - player.transform.position; //IMPORTANT player.transform.position
                directionFromPlayer.y = 0;
                Quaternion rotationFromPlayer = Quaternion.LookRotation(directionFromPlayer + new Vector3(Random.Range(-player.bulletDeflectionDiameter, player.bulletDeflectionDiameter + 1), 0, Random.Range(-player.bulletDeflectionDiameter, player.bulletDeflectionDiameter + 1)));
                Instantiate(bulletType, player.transform.position + new Vector3(0f, 0.1f, 0) + directionFromPlayer.normalized, rotationFromPlayer);
                player.shotSound.Play();
                alredyFiredBullets++;
                ammoNow--;
                if (numberOfPatronsText != null)
                {
                    numberOfPatronsText.text = ammoNow.ToString();
                }
                yield return new WaitForSeconds(timeBetweenShots);
            }
            if (ammoNow <= 0) { StartCoroutine(Reload()); }

        }
    }
}
