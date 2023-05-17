using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameLevel : MonoBehaviour
{
    public GameObject initialOrcPrefab;
    public GameObject initialArrowPrefab;

    public GameObject initialPlayerObj;
    public TextMeshProUGUI myText;
    public TextMeshProUGUI levelCounter;
    public AudioSource src; 
    public AudioClip clip1; 

    private static AudioSource initSrc; 
    private static AudioClip initClip; 
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
    private static float orcMovementSpeed = 3.0f;
    private static int totalKills = 0; 
    
    public static readonly int[,] levelGoals = {
        {10, 1},
        {10, 2},
        {15, 3},
    };

    // Start is called before the first frame update
    void Start()
    {
        initSrc = src; 
        initClip = clip1; 
        orcPrefab = initialOrcPrefab;
        arrowPrefab = initialArrowPrefab;
        playerObj = initialPlayerObj;
        spawnOrc();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = string.Format("kills = {0}", totalKills);  
        levelCounter.text = string.Format("level = {0}", currentLevel+1);  
    }

    public static void spawnOrc() {
        string newOrcID = $"Orc_{initializedOrcsCount++}";
        initializedOrcs.Enqueue(newOrcID);
        Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 9.0f), 0);
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
            orcMovementSpeed += 0.75f;
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
        if(totalKills >= 34)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public static string getOrcId() {
        return initializedOrcs.Dequeue();
    }

    public static float getOrcMovementSpeed() {
        return orcMovementSpeed;
    }

    public static GameObject getPlayer() {
        return playerObj;
    }

    public static void spawnArrow(Vector3 position, Quaternion rotation, Vector3 direction) {
        string newArrowID = $"Arrow_{initializedArrowsCount++}";
        initSrc.clip = initClip; 
        initSrc.Play();
        initializedArrows.Enqueue(newArrowID);
        GameObject newArrow = Instantiate(arrowPrefab, position, rotation);
        newArrow.name = newArrowID;
        if (Utility.isFlipped) {
        Transform transform = newArrow.GetComponent<Transform>(); 
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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
