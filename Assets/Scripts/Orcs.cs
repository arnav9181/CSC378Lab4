using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Orcs : MonoBehaviour
{
    public GameObject person;
    private string enemyID;

    private static int killCount = 0;
    private static int enemyCount = 0;
    private static int currentLevel = 0;
    private readonly int[,] levelGoals = {
        {8, 1},
        {12, 2}
    };

    private void Start()
    {
        spawnEnemy();
    }

    private void Update()
    {
        
    }

    private void spawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f), 0);
        GameObject enemy = Instantiate(person, pos, Quaternion.Euler(Vector3.zero));
        enemyID = $"ENEMY_{currentLevel}_{enemyCount}_{killCount}";
        enemy.name = enemyID;
        enemy.AddComponent<BoxCollider2D>();
        enemyCount++;
    }

    public static void destroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        enemyCount--;
        killCount++;
    }

    private void spawnLevel()
    {
        
        if (killCount < levelGoals[currentLevel, 0] && enemyCount == 0)
        {
            
            spawnEnemy();
            
        }
            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag.Contains("Arrow"))
        {
            Destroy(col.gameObject, 0.01f); 
        }


        if (killCount == levelGoals[currentLevel, 0])
        {
            killCount = 0;
            enemyCount = 0;
            currentLevel++;
            if (currentLevel == levelGoals.GetLength(0))
            {
                return;
            }
        }

        spawnLevel();
    }
}
