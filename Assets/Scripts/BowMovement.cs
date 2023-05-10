using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMovement : MonoBehaviour
{
    [SerializeField] Transform bow;
    public GameObject arrow; 
    public float launch; 
    public Transform shotPoint; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RotateBow(){
        float angle = Utility.AngleTowardsMouse(bow.position);
        bow.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    // Update is called once per frame
    void Update()
    {
       RotateBow();   
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        } 
    }

    void Shoot(){
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launch;
        Destroy(newArrow, 2);
    }
}
