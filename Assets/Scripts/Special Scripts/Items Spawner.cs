using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsWeapons;
    [SerializeField] private GameObject[] itemsBonuses;
    [SerializeField] private PlayerController playerController;

    [Header("Chances to get something")]

    [SerializeField] private int chanceForItem;
 
    [Tooltip("For example, if it will 40, then it will be 60% for Bonus")]
    [SerializeField] private int chanceForWeapon;
    [Space]
    [SerializeField] private int chanceForWeaponIfPlayerHasPistol;
    public void SpawnItem(Vector3 position)
    {
        int dice = Random.Range(0, 100);

        if (playerController.weapon.name == "PistolInfo" && dice < chanceForWeaponIfPlayerHasPistol)
        {
            Debug.Log("Pistol");
            ChangingPistol(position);
        }
        else if (dice < chanceForItem)
        {
            BonusOrWeaponDecision(position);
        }
    }

    private void ChangingPistol(Vector3 position)
    {
        int randomIndex = Random.Range(1, itemsWeapons.Length);
         Instantiate(itemsWeapons[randomIndex], position + new Vector3(0, 0.75f, 0), Quaternion.Euler(0f, 90f, 90f));
    }

    private void BonusOrWeaponDecision(Vector3 position)
    {
        int dice = Random.Range(0, 100);
        if (dice < chanceForWeapon)
        {
            int randomIndex = Random.Range(0, itemsWeapons.Length);
            Instantiate(itemsWeapons[randomIndex], position + new Vector3(0, 0.75f, 0), Quaternion.Euler(0f, 90f, 90f));
        }
        else
        {
            int randomIndex = Random.Range(0, itemsBonuses.Length);
            if (itemsBonuses[randomIndex].name == "FastLegs" || itemsBonuses[randomIndex].name == "Nuclear Blast")
            {
                Instantiate(itemsBonuses[randomIndex], position + new Vector3(0, 0.5f, 0), Quaternion.Euler(90f, 0f, 0f));
            }
            else
            {
                Instantiate(itemsBonuses[randomIndex], position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
        }
    }
}

