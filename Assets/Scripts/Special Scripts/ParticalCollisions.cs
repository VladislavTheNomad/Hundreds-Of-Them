using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalCollisions : MonoBehaviour
{
    public WeaponInfo flameThrowerInfo;
    private void Start()
    {
        flameThrowerInfo = GameObject.Find("FlameThrowerInfo").GetComponent<WeaponInfo>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().HP -= flameThrowerInfo.damagePerBullet;
        }
    }
}

