using UnityEngine;

public class DestroyParentOnParticleCollision : MonoBehaviour
{
    private Enemy enemy;
    private WeaponInfo flameThrower;

    private void Awake()
    {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
        flameThrower = GameObject.Find("FlameThrowerInfo").GetComponent<WeaponInfo>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collision with enemy detected.");
        enemy.HP -= flameThrower.damagePerBullet;
        Debug.Log(enemy.HP);
    }
}