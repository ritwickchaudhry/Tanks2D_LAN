using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


// THIS SCRIPT SHOULD STORE JUST ALL THE PROPERTIES OF THE TANK
public class TankProp : NetworkBehaviour
{
    // SynVar helps to sync this variable to the same value on all clients and server
    // Basically, the default gameobject name wasn't being synced. So, created one.
    [SyncVar]
    public string name;                     // The name of the tank
    public float maxHealth;                 // The maximum health that the tank can have
    public float speed;                     // The speed with which the tank moves around
    public float bulletVelocity;            // Velocity with which bullet leaves the tank
    public float fireRate;                  // Rate of firing. Min time to wait to fire next shot

    // Use this for initialization
    void Start()
    {

    }

}
