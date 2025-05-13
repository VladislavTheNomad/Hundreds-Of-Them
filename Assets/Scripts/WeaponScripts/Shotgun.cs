using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponInfo
{

    [SerializeField] private float bulletsPerShotSpreadModifier;
    [SerializeField] private int bulletsPerShot;




    public override void SpawnBullet()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 directionFromPlayer = hit.point - player.transform.position; //IMPORTANT  player.transform.position
            directionFromPlayer.y = 0;
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Quaternion rotationFromPlayer = Quaternion.LookRotation(directionFromPlayer + new Vector3(Random.Range(-player.bulletDeflectionDiameter - bulletsPerShotSpreadModifier, player.bulletDeflectionDiameter + bulletsPerShotSpreadModifier + 1), 0, Random.Range(-player.bulletDeflectionDiameter - bulletsPerShotSpreadModifier, player.bulletDeflectionDiameter + bulletsPerShotSpreadModifier + 1)));
                Instantiate(bulletType, player.transform.position + new Vector3(0, 0.1f, 0) + directionFromPlayer.normalized, rotationFromPlayer);
                player.shotSound.Play();
            }
        }
        if (ammoNow == 0) { StartCoroutine(Reload()); }
    }
}
