using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMovement : MonoBehaviour
{
    [SerializeField] Transform bow;
    public GameObject arrow;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       rotateBow();   
        if (Input.GetMouseButtonDown(0)){
            GameLevel.spawnArrow(shotPoint.position, shotPoint.rotation, transform.right);
        } 
    }

    void rotateBow(){
        float angle = Utility.getAngleTowardsMouse(bow.position);
        bow.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
