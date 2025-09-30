using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR;
using System.Threading;
using System.Diagnostics; //for communicating with Quest Controllers. 

/*
 
Select to spawn
where object spawns
Cooldown period
Input button
hand
 

*/

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; //object to spawn
    public Transform spawnPoint; //where it spanws
    public XRNode controllerNode = XRNode.RightHand;
    public float spawnCooldown = 1.0f; // Need a coroutine
    private bool canSpawn = true; // Time in seconds between spawns
    
    
    
    

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && IsAButtonPressed())
        {

            StartCoroutine(SpawnOjbectWithCooldown());

        }
    }


    bool IsAButtonPressed()
    { 
    
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool buttonPressed = false;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed) // primaryButton is "a" or "x" button on
        {

            return true;

        }

        return false;

    }

    IEnumerator SpawnOjbectWithCooldown()
    {
        canSpawn = false; //Prevent immediate respawn
        SpawnObject();
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true; // allow us to spawn again. 
    }

    void SpawnObject()
    {

        if (objectPrefab != null && spawnPoint != null)
        {
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);

        }

       

    }
}
