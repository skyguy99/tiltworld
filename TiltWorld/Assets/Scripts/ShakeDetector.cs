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

    void ResetCurrentWorld()
    {
        foreach (WorldController w in worldControllers)
        {
            if (w.num == player.selectedWorld)
            {
                w.ResetWorld();
                //and move away
            }
        }
    }

    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {

            player.character.LeaveWorld();
            ResetCurrentWorld();
        
            
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }

        //TEST EDITOR
        if(Input.GetKey("space"))
        {
            player.character.LeaveWorld();
            Invoke("ResetCurrentWorld", 0.5f);

            print("SHAKE!");
        }
    }
}