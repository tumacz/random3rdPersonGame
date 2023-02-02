using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private HealthController enemyPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float spawnCollisionCheckRadious = 3f;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private int healthBonus = 1;

    private bool groupIsDead = false;
    private int enemiesLeft;

    HealthController[] pool;

    private List<HealthController> activeEnemies = new();

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDestroy()
    {
        foreach(var enemy in pool)
        {
            enemy.OnDisactive -= HandleDeactivatedEnamy;
        }
    }

    private void LifeChecker()
    {
        enemiesLeft = activeEnemies.Count;
        if (enemiesLeft < 2)
        {
            groupIsDead = true;
        }
    }

    private void PopulatePool()
    {
        pool = new HealthController[poolSize];
        int iterEnemy = 0;
        while (iterEnemy<poolSize)
        {
            Vector2 randomPoint2D = (transform.position + Random.insideUnitSphere * spawnRadius);
            Vector3 randomPoint3D = new Vector3(randomPoint2D.x, 2f, randomPoint2D.y);
            if (!Physics.CheckSphere(randomPoint3D, spawnCollisionCheckRadious))
            {
                HealthController activeEnemy = Instantiate(enemyPrefab, randomPoint3D, Quaternion.identity, transform);
                activeEnemies.Add(activeEnemy);
                activeEnemy.OnDisactive += HandleDeactivatedEnamy;
                pool[iterEnemy] = activeEnemy;
                pool[iterEnemy].gameObject.SetActive(true);
                iterEnemy++;
            }
        }
        
    }

    private IEnumerator SpawnEnemy()
    {
        while (!groupIsDead)
        {
            foreach (HealthController enemy in pool)
                {
                    if (enemy.gameObject.activeInHierarchy)
                    {
                        ManageLivingEnemy(enemy);
                    }
                if (!enemy.gameObject.activeInHierarchy)
                    {
                        RespawnEnemy(enemy);
                    }
            }
            yield return new WaitForSeconds(spawnTime);
        }
        
    }

    private void ManageLivingEnemy(HealthController healthController)
    {
        healthController.IncreaseHealth(healthBonus);
    }

    private void RespawnEnemy(HealthController enemy)
    {
        bool notFoundMatchingPosition = true;
        Vector3 randomPoint3D = GenerateRandomPosition();
        while (notFoundMatchingPosition)
        {
            randomPoint3D = GenerateRandomPosition();
            if (!Physics.CheckSphere(randomPoint3D, spawnCollisionCheckRadious))
            {
                notFoundMatchingPosition = false;
            }
            else
            {
                notFoundMatchingPosition = true;
            }

        }
        enemy.transform.localPosition = randomPoint3D;
        enemy.gameObject.SetActive(true);
        activeEnemies.Add(enemy);
    }

    private Vector3 GenerateRandomPosition()
    {

        Vector2 randomPoint2D = (transform.position + Random.insideUnitSphere * spawnRadius);
        Vector3 randomPoint3D = new Vector3(randomPoint2D.x, 2f, randomPoint2D.y);
        return randomPoint3D;
    }

    private void HandleDeactivatedEnamy(HealthController healthController)
    {
        activeEnemies.Remove(healthController);
        LifeChecker();
    }
}
