using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool isHit = false;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (!isHit)
        {
            float angle = Utility.getAngle2D(rb.velocity);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        stopMotion();
    }

    private void stopMotion() {
        isHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
