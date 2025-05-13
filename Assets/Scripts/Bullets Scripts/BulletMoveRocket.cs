using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveRocket : BulletBehavior
{

    [SerializeField] private float speedAcceleration = 2f;
    private Light rocketExplosion;
    private PlayerController playerControls;
    [SerializeField] private float rangeOfExplosionDamage;
    private int damagePerBullet;
    private bool isExploding = false;

    private new void Start()
    {
        rocketExplosion = GetComponent<Light>();
        playerControls = GameObject.Find("Player").GetComponent<PlayerController>();
        rangeOfExplosionDamage = playerControls.weapon.rangeOfIonOrExplosionDamage;
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
            rocketExplosion.intensity += 4f;
            rocketExplosion.range += 0.25f;
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        speed += speedAcceleration;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeOfExplosionDamage);
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
