using UnityEngine;

public class ShakeDetector : MonoBehaviour
{

    public float ShakeDetectionThreshold = 3.6f;
    public float MinShakeInterval = 0.2f;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    PlayerController player;
    

    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        player = GameObject.FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {

            player.ResetPosition();
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }
        if(Input.GetKey("space"))
        {
            player.ResetPosition();
        }

    }
}