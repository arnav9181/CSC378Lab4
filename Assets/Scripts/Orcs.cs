using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Orcs : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    private string enemyID;
    private float speed;

    private void Start()
    {
        enemyID = GameLevel.getOrcId();
        player = GameLevel.getPlayer();
        enemy = GameObject.Find(enemyID);
        speed = GameLevel.getOrcMovementSpeed();
    }

    private void Update()
    {
        enemy = GameObject.Find(enemyID);
        Vector2 direction = player.transform.position - enemy.transform.position; 
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public string getOrcID() {
        return enemyID;
    }
}