using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankShoot : NetworkBehaviour {

    public GameObject bulletPrefab;             // The bullet to be fired from the tank
    public float bulletVelocity = 5f;           // Velocity with which bullet leaves the tank
    public float bulletRelPosition = 1.25f;     // Starting postion of bullet relative wrt to tank 
    public float fireRate = 0.5f;               // Rate of firing. Min time to wait to fire next shot

    private float nextFireTime;

	// Use this for initialization
	void Start () {

        nextFireTime = Time.time;   // Initialize the nextFireTime
	}
	

    //Function to Fire Bullets
    //Command is a keyword that runs the command(Function) on server
    //Also the preix of the function name has to be Cmd
    [Command]
    void CmdFireBullet()
    {
        float tankAngle = transform.eulerAngles.z * Mathf.Deg2Rad;  // Angle of shooting

        // Define position of bullet
        Vector2 tankPosition = transform.position;
        Vector3 pos = new Vector3(tankPosition.x + bulletRelPosition * Mathf.Cos(tankAngle), tankPosition.y + bulletRelPosition * Mathf.Sin(tankAngle), transform.position.z);

        // Define the velocity of bullet
        Vector2 vel = new Vector2(bulletVelocity * Mathf.Cos(tankAngle), bulletVelocity * Mathf.Sin(tankAngle));

        // Instantiate the gameobject at the position with the given velocity locally
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, pos, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = vel;

        // Properties of the bullet. Can be changed to make better gameplay.
        bullet.GetComponent<bulletController>().parent = gameObject;
        bullet.GetComponent<bulletController>().damage = 5.0f;
        bullet.GetComponent<bulletController>().radius = 0.18f;

        //Spawn the bullet on all clients
        NetworkServer.Spawn(bullet);

        // Update the the next time available for a fire.
        nextFireTime = Time.time + fireRate;
    }
    
	// Update is called once per frame
	void Update () {
        //if (!isLocalPlayer) return;

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            CmdFireBullet();
        }

    }
}
