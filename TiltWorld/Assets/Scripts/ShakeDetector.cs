using UnityEngine;
using UnityEngine.SceneManagement;

public class ShakeDetector : MonoBehaviour
{

    public float ShakeDetectionThreshold = 3.6f;
    public float MinShakeInterval = 0.2f;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    public float ShakeForceMultiplier = 3f;
    PlayerController player;
    CharController character;
    UIManager uIManager;

    void Awake()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        player = GameObject.FindObjectOfType<PlayerController>();
        character = GameObject.FindObjectOfType<CharController>();
    }

    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        foreach(ObjectController o in GameObject.FindObjectsOfType<ObjectController>())
        {
            o.rb.AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode.Impulse);
        }

        character.GetComponent<Rigidbody>().AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            //ShakeRigidbodies(Input.acceleration);
            uIManager.TriggerAskForReset();
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }
      

    }
}