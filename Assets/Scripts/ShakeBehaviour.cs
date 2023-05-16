using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    //Credit to https://medium.com/nice-things-ios-android-development/basic-2d-screen-shake-in-unity-9c27b56b516
    // Start is called before the first frame update
     // Transform of the GameObject you want to shake
    private Transform transform;
 
    // Desired duration of the shake effect
    public static float shakeDuration;
 
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.7f;
 
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
 
    // The initial position of the GameObject
    Vector3 initialPosition;
    void Awake()
    {
        if (transform == null)
        { 
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
   
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public static void TriggerShake() {
        shakeDuration = 0.5f;
    }


}
