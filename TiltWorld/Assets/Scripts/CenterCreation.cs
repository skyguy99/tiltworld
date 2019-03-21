using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CenterCreation : MonoBehaviour
{
    //NOTE for future: other combos can produce same end masterpiece 

    //spray can (sprays some paint on side), robot head, robot hand = BIG ROBOT
    //flower, robot head, signletter = STEAMPUNK GARDEN CONTRAPTION (like Austin texas hand plus https://www.google.com/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwio-9GL8IrhAhUJuZ4KHUfQC_IQjRx6BAgBEAU&url=https%3A%2F%2Fwww.pinterest.com%2Fpin%2F756393699888812097%2F&psig=AOvVaw0C1WTxdEpr2fH9_zWryB8V&ust=1552970490944711)
    //old car, wheel from pulley (falls off), slingshot = piecemeal spaceship

    UIManager uIManager;
    PlayerController player;
   
    //store object name, then object that it instantiates
    [System.Serializable]
    public class ObjectsThatCollide
    {
        public string[] objectsCombo = new string[3];
        public Transform masterpiece;
    }

    public ObjectsThatCollide[] masterpieceQueue;

    public string[] objectsThatHaveCollided = new string[3]; //certain 3 object combos produce different end center creations
    int objectsEntered;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
    }

    void CheckToInstantiateMasterpiece()
    {
        foreach(ObjectsThatCollide o in masterpieceQueue)
        {
            if(o.objectsCombo.Intersect(objectsThatHaveCollided).Any() && o.masterpiece != null) //Compare arrays regardless of order
            {
                print("INSTANTIATING MASTERPIECE FOR: "+objectsThatHaveCollided);
                Instantiate(o.masterpiece, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                objectsThatHaveCollided = new string[3]; //clear array
                objectsEntered = 0;

                //play audio
                player.audioAccents.PlayOneShot(player.audioClips[2]); //steampunk/gears/rolling success sound
            }
        }
    }

    public void CollidedWith(ObjectController obj)
    {
        //print("Collide!");
        if (obj.renderer.isVisible)
        {
            //Destroy(obj.gameObject);
            objectsThatHaveCollided[objectsEntered] = obj.gameObject.GetComponent<ObjectController>().objName;
            objectsEntered++;
        }
    }

    void TriggerUIFinalAlert()
    {
        uIManager.ShowObjectText(transform, "MASTERPIECE", "You created a new masterpiece", false);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
