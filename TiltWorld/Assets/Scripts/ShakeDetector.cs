using UnityEngine;

public class ShakeDetector : MonoBehaviour
{

    public float ShakeDetectionThreshold = 3.6f;
    public float MinShakeInterval = 0.2f;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    WorldController worldController;

    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        worldController = GetComponent<WorldController>();
    }

    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            //worldController.ShakeandReset(Input.acceleration);
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }
    }
}