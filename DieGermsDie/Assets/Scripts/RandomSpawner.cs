using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour
{
    public Banner banAd;
    int eKilled = 0;
    bool isStarted = false;
    public TextMeshProUGUI zKilled;
    public TextMeshProUGUI ZRemaining;
    public TextMeshProUGUI CurrWave;
    public GameObject[] itemsToSpawn;
    public int seed = 0;
    int xPos = 60;
    //int yPos = 0;
    int zPos = 60;
    List<Vector3> buildingsPos = new List<Vector3>();
    public bool addBuilding = true;
    public bool addEnemy = true;
    public GameObject[] enemies;
    public int initialEnemiesSpawned;
    int wave = 0;
    public GameObject[] enemiesOnMap;

    public GameObject fence;
    public GameObject[] trees;

    public GameObject[] terrainObjects;
    public GameObject[] grasses;


    private void Awake()
    {
        Random.InitState(seed);
    }

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            RandomItemSpawner();
        }
        SpawnFence();
        SpawnForest();
        SpawnTerrainObjects();
        SpawnGrass();
        for (int i = 0; i < initialEnemiesSpawned; i++)
        {
            EnemiesSpawner();
            if (!addEnemy)
            {
                i--;

            }


        }
        isStarted = true;
    }

    public void Update()
    {
        if (isStarted) 
        {
            enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");
            ZRemaining.text = enemiesOnMap.Length.ToString();
            CurrWave.text = (wave + 1).ToString();
            if (enemiesOnMap.Length < 1)
            {
                wave++;
                for (int i = 0; i < initialEnemiesSpawned + wave; i++)
                {
                    EnemiesSpawner();
                    if (!addEnemy)
                    {
                        i--;

                    }


                }
            }
        }

    }

    public void setScore()
    {
        banAd.DestroyBannerAd();
        PlayerPrefs.SetInt("Score", eKilled);

        if (PlayerPrefs.GetInt("HighScore") < eKilled)
        {
            PlayerPrefs.SetInt("HighScore",eKilled);

        }
        SceneManager.LoadScene("GameOver");
    }

    public void EnemiesKilled ()
    {
        eKilled++;
        zKilled.text = eKilled.ToString();

    }

    public GameObject RandomItemPicker()
    {

        return itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

    }
    public void RandomItemSpawner()
    {
        int xRandom = Random.Range(-xPos, xPos);
        int zRandom = Random.Range(-zPos, zPos);

        Vector3 randPos = new Vector3(xRandom, 0, zRandom);

        foreach (var pos in buildingsPos)
        {

            if ((randPos.z > pos.z - 20 && randPos.z < pos.z + 20) && (randPos.x > pos.x - 20 && randPos.x < pos.x + 20))
            {
                addBuilding = false;
                break;
            }
            else
            {
                addBuilding = true;


            }
        }

        if (addBuilding)
        {
            buildingsPos.Add(randPos);
            GameObject building = RandomItemPicker();
            Instantiate(building, building.transform.position + transform.position + randPos, building.transform.rotation);

        }



    }
    public GameObject RandomEnemyPicker()
    {

        return enemies[Random.Range(0, enemies.Length)];

    }

    public void EnemiesSpawner()
    {
        int xRandom = Random.Range(-xPos, xPos);
        int zRandom = Random.Range(-zPos, zPos);

        Vector3 randPos = new Vector3(xRandom, 0, zRandom);

        foreach (var pos in buildingsPos)
        {

            if ((randPos.z > pos.z - 20 && randPos.z < pos.z + 20) && (randPos.x > pos.x - 20 && randPos.x < pos.x + 20))
            {
                addEnemy = false;
                break;
            }
            else
            {
                addEnemy = true;


            }
        }

        if (addEnemy)
        {
            // buildingsPos.Add(randPos);
            GameObject enemy = RandomEnemyPicker();
            Instantiate(enemy, enemy.transform.position + transform.position + randPos, enemy.transform.rotation);

        }



    }

    public void SpawnFence()
    {
        var i = -100f;
        var j = -97f;
        while (i < 100f)
        {
            Instantiate(fence, new Vector3(fence.transform.position.x + i, fence.transform.position.y, 104f), fence.transform.rotation);
            Instantiate(fence, new Vector3(fence.transform.position.x + i, fence.transform.position.y, -100f), fence.transform.rotation);

            Instantiate(fence, new Vector3(101, fence.transform.position.y, fence.transform.position.z + j), Quaternion.Euler(fence.transform.rotation.x, fence.transform.rotation.x + 90f, fence.transform.rotation.z));
            Instantiate(fence, new Vector3(-103, fence.transform.position.y, fence.transform.position.z + j), Quaternion.Euler(fence.transform.rotation.x, fence.transform.rotation.x + 90f, fence.transform.rotation.z));

            i += 5.5f;
            j += 5.5f;
        }

    }

    public void SpawnForest()
    {
        GameObject[] fences = GameObject.FindGameObjectsWithTag("Fence");
        foreach (var fence in fences)
        {
            if (fence.transform.rotation.y == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var zForestPos = Random.Range(fence.transform.position.z, fence.transform.position.z * 1.5f);
                    var tree = trees[Random.Range(0, trees.Length)];
                    Instantiate(tree, new Vector3(fence.transform.position.x + Random.Range(-40, 40), tree.transform.position.y, tree.transform.position.z + zForestPos), tree.transform.rotation);
                }

            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    var xForestPos = Random.Range(fence.transform.position.x, fence.transform.position.x * 1.5f);
                    var tree = trees[Random.Range(0, trees.Length)];
                    Instantiate(tree, new Vector3(tree.transform.position.x + xForestPos, tree.transform.position.y, fence.transform.position.z + Random.Range(-40, 40)), tree.transform.rotation);
                }

            }

        }

    }

    public void SpawnTerrainObjects()
    {
        for (int i = 0; i < 100; i++)
        {
            int xRandom = Random.Range(-xPos - 20, xPos + 20);
            int zRandom = Random.Range(-zPos - 20, zPos + 20);
            Vector3 randPos = new Vector3(xRandom, 0, zRandom);
            var terrainObject = terrainObjects[Random.Range(0, terrainObjects.Length)];
            Instantiate(terrainObject, terrainObject.transform.position + randPos, terrainObject.transform.rotation);
        }

    }
    public void SpawnGrass()
    {
        for (int i = 0; i < 1000; i++)
        {
            int xRandom = Random.Range(-xPos - 40, xPos + 40);
            int zRandom = Random.Range(-zPos - 40, zPos + 40);
            Vector3 randPos = new Vector3(xRandom, 0, zRandom);
            var grass = grasses[Random.Range(0, grasses.Length)];
            Instantiate(grass, grass.transform.position + randPos, grass.transform.rotation);
        }
    }

}
