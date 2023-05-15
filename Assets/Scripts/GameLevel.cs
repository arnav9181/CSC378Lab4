using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public GameObject initialOrcPrefab;
    public GameObject initialArrowPrefab;

    public GameObject initialPlayerObj;

    private static GameObject orcPrefab;
    private static GameObject arrowPrefab;

    private static GameObject playerObj;

    private static int initializedOrcsCount = 0;
    private static int initializedArrowsCount = 0;
    private static Queue<string> initializedOrcs = new Queue<string>();
    private static Queue<string> initializedArrows = new Queue<string>();

    private static int killCount = 0;
    private static int enemyCount = 0;
    private static int currentLevel = 0;
    private static float arrowLaunchSpeed = 14.0f;
    private static int totalKills = 0; 
    
    public static readonly int[,] levelGoals = {
        {10, 1},
        {10, 2},
        {15, 3},
    };

    // Start is called before the first frame update
    void Start()
    {
        orcPrefab = initialOrcPrefab;
        arrowPrefab = initialArrowPrefab;
        playerObj = initialPlayerObj;
        spawnOrc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void spawnOrc() {
        string newOrcID = $"Orc_{initializedOrcsCount++}";
        initializedOrcs.Enqueue(newOrcID);
        Vector3 pos = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 5.0f), 0);
        GameObject newOrc = Instantiate(orcPrefab, pos, Quaternion.Euler(Vector3.zero));
        newOrc.name = newOrcID;
        enemyCount++;
    }

    public static void destoyOrc(string orcID) {
        GameObject orcObject = GameObject.Find(orcID);
        Destroy(orcObject);
        enemyCount--;
        killCount++;
        totalKills++;
        Debug.Log(totalKills);

        if (killCount == levelGoals[currentLevel, 0]) {
            killCount = 0;
            enemyCount = 0;
            currentLevel++;
            if (currentLevel == levelGoals.GetLength(0))
            {
                return;
            }
        }

        if (killCount < levelGoals[currentLevel, 0] && enemyCount == 0)
        {
            for (int i=0; i<levelGoals[currentLevel, 1]; i++) {
                spawnOrc();
            }
        }
        if(totalKills == 35)
        {
            Debug.Log("You win");
        }
    }

    public static string getOrcId() {
        return initializedOrcs.Dequeue();
    }

    public static GameObject getPlayer() {
        return playerObj;
    }

    public static void spawnArrow(Vector3 position, Quaternion rotation, Vector3 direction) {
        string newArrowID = $"Arrow_{initializedArrowsCount++}";
        initializedArrows.Enqueue(newArrowID);
        GameObject newArrow = Instantiate(arrowPrefab, position, rotation);
        newArrow.name = newArrowID;
        newArrow.GetComponent<Rigidbody2D>().velocity = direction * arrowLaunchSpeed * Utility.getDirection();
    }

    public static void destroyArrow(string arrowID, float delay) {
        GameObject arrowObject = GameObject.Find(arrowID);
        Destroy(arrowObject, delay);
    }

    public static string getArrowId() {
        return initializedArrows.Dequeue();
    }
}
