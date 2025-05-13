using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponInfo : MonoBehaviour
{
    public AudioClip specificShotSound;
    [SerializeField] protected Camera mainCamera;
    [SerializeField] protected GameObject bulletType;
    [SerializeField] protected PlayerController player;


    public bool isFiringNow { get; protected set; }
    [SerializeField] protected float fireRate;
    public int damagePerBullet;
    [SerializeField] protected int ammoCapacity;
    public int ammoNow;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float bulletSpreadModifier;

    [SerializeField] protected TextMeshProUGUI numberOfPatronsText;
    //public bool isIonicWeapon;
    public float rangeOfIonOrExplosionDamage = 0;

    //public virtual void IonDamage()
    //{
    //}

    public void Fire()
    {
        StartCoroutine(SpitBullets());
    }

    public virtual void Equip()
    {
        
    }

    public virtual void Unequip()
    {
        
    }

    protected virtual IEnumerator SpitBullets()
    {
        isFiringNow = true;
        float numberOfAlreadyFiredBullets = 0f;
        while (Input.GetMouseButton(0) && ammoNow > 0)
        {
            player.bulletDeflectionDiameter = numberOfAlreadyFiredBullets * bulletSpreadModifier;
            SpawnBullet();
            numberOfAlreadyFiredBullets += 1f;
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
        isFiringNow = false;
    }

    protected IEnumerator Reload()
    {
        player.isReload = true;
        player.reloadText.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        player.reloadText.SetActive(false);
        player.isReload = false;
        ammoNow = ammoCapacity;
        if (numberOfPatronsText != null)
        {
            numberOfPatronsText.text = ammoNow.ToString();
        }
    }


    public virtual void SpawnBullet()
    {
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 directionFromPlayer = hit.point - player.transform.position; //IMPORTANT player.transform.position
                directionFromPlayer.y = 0;
                Quaternion rotationFromPlayer = Quaternion.LookRotation(directionFromPlayer + new Vector3(Random.Range(-player.bulletDeflectionDiameter, player.bulletDeflectionDiameter + 1), 0, Random.Range(-player.bulletDeflectionDiameter, player.bulletDeflectionDiameter + 1)));
                // Spawn the bullet at the hit position
                Instantiate(bulletType, player.transform.position + new Vector3(0f, 0.1f, 0) + directionFromPlayer.normalized, rotationFromPlayer);
                player.shotSound.Play();    
            }
            if (ammoNow == 0) { StartCoroutine(Reload()); }
        }
    }
}
