using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
    [SerializeField] private List <GameObject> enemies;
    [SerializeField] private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameOn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateEnemies()
    {
        int dice = Random.Range(0, 4);

        switch (dice)
        {
            case 0:
                Instantiate(enemies[Random.Range(0, 4)], PositionForSpawningLeft(), transform.rotation);
                break;
            case 1:
                Instantiate(enemies[Random.Range(0, 4)], PositionForSpawningRight(), transform.rotation);

                break;
            case 2:
                Instantiate(enemies[Random.Range(0, 4)], PositionForSpawningUp(), transform.rotation);
                break;
            case 3:
                Instantiate(enemies[Random.Range(0, 4)], PositionForSpawningDown(), transform.rotation);
                break;
            default:
                break;
        }
    }

    IEnumerator GameOn()
    {
        while (GameObject.Find("Player") != null)
        {
            CreateEnemies();
            yield return new WaitForSeconds(spawnRate);
        }

    }

    private Vector3 PositionForSpawningLeft()
    {
        float spawnPosZ = Random.Range(-29f, 29f);
        float spawnPosX = -29f;
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private Vector3 PositionForSpawningRight()
    {
        float spawnPosZ = Random.Range(-29f, 29f);
        float spawnPosX = 29f;
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private Vector3 PositionForSpawningUp()
    {
        float spawnPosX = Random.Range(-29f, 29f);
        float spawnPosZ = 29f;
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private Vector3 PositionForSpawningDown()
    {
        float spawnPosX = Random.Range(-29f, 29f);
        float spawnPosZ = -29f;
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
