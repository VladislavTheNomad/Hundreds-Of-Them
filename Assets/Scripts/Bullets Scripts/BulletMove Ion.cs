using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveIon : BulletBehavior
{
    private PlayerController playerControls;
    private Light ionExplosion;
    private float rangeOfIonDamage;
    private int damagePerBullet;
    private bool isExploding = false;

    new void Start()
    {
        ionExplosion = GetComponent<Light>();
        playerControls = GameObject.Find("Player").GetComponent<PlayerController>();
        rangeOfIonDamage = playerControls.weapon.rangeOfIonOrExplosionDamage;
        damagePerBullet = playerControls.weapon.damagePerBullet;
        base.Start();
    }

    new void Update()
    {
        if (!isExploding)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (isExploding)
        {
            ionExplosion.range += 0.4f;
            ionExplosion.intensity += 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(ExplodeAnimation());
        }
    }

    IEnumerator ExplodeAnimation()
    {
        isExploding = true;
        yield return new WaitForSeconds(0.1f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeOfIonDamage);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.HP -= damagePerBullet;
                }
            }
        }
        Destroy(gameObject);
    }
}
