using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : BonusInfo
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject shockBulletPrefab;
    [SerializeField] private int maxEnemies = 5;
    public float shockWaveRadius = 10f;
    public int damage = 20;
    
    private List<Enemy> enemiesNearByArray = new List<Enemy>();


    public override void StartBonus()
    {
        bool hasEmptyBonusSlot = gameHelper.DisplayBonus("Shock Wave", timeBonus);
        if (hasEmptyBonusSlot)
        {
            Collider[] collidersNearBy = Physics.OverlapSphere(player.transform.position, shockWaveRadius);
            foreach (Collider enemy in collidersNearBy)
            {
                if (enemy.CompareTag("Enemy") && enemiesNearByArray.Count <= maxEnemies)
                {
                    enemiesNearByArray.Add(enemy.GetComponent<Enemy>());
                }
            }
            foreach (Enemy enemy in enemiesNearByArray)
            {
                Quaternion rotationToEnemy = Quaternion.LookRotation(enemy.transform.position - player.transform.position);
                Instantiate(shockBulletPrefab, player.transform.position + new Vector3(0f, 0.5f, 0f), rotationToEnemy);
            }
            enemiesNearByArray.Clear();
        }
    }

    protected override void StopBonus()
    {
        throw new System.NotImplementedException();
    }


}
