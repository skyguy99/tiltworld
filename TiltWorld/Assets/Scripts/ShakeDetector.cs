using UnityEngine;

public class ShakeDetector : MonoBehaviour
{

    public float ShakeDetectionThreshold = 3.6f;
    public float MinShakeInterval = 0.2f;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    WorldController[] worldControllers;
    PlayerController player;
    

    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        worldControllers = GameObject.FindObjectsOfType<WorldController>();
        player = GameObject.FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
        
            
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }

    }
}