using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int health;
    private HashSet<string> attackedOrcIDs = new HashSet<string>();

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        string collisionTag = col.gameObject.tag;
        string objectID = col.gameObject.name;
        if(collisionTag.Contains("ENEMY") && !attackedOrcIDs.Contains(objectID))
        {
            attackedOrcIDs.Add(objectID);
            health--;
            ShakeBehaviour.TriggerShake();
            if (health == 0) {
                SceneManager.LoadScene("EndGame");
            }
        }
    }
}
