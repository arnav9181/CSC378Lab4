using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    
    private Rigidbody2D rb;

    private bool collided;
    
    private string arrowID;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arrowID = GameLevel.getArrowId();
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            float angle = Utility.getAngle2D(rb.velocity);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        }
    }

    private void stopMotion() {
        collided = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        float delay = 1;
        if (!collided && col.gameObject.tag.Contains("ENEMY")) {
            string orcID = col.gameObject.GetComponent<Orcs>().getOrcID();
            GameLevel.destoyOrc(orcID);
            delay = 0.1f;
        }
        stopMotion();
        GameLevel.destroyArrow(arrowID, delay);
        
    }
}