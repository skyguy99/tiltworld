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
                player.character.LeaveWorld(w);
                //and move away
            }
        }
    }

    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
                   && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {


            ResetCurrentWorld();
        
            
            print("SHAKE!");
            timeSinceLastShake = Time.unscaledTime;
        }

        //TEST EDITOR
        if(Input.GetKey("space"))
        {

            ResetCurrentWorld();
            print("SHAKE!");
        }
    }
}