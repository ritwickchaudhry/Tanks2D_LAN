using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// THIS SCRIPT HANDLES THE MOVEMENT AND COLLISIONS OF TANK
public class TankMove : NetworkBehaviour {

    private float speed;          // Speed at which the tank moves.

    static public int playerNumber;

	// Use this for initialization
	void Start () {

        speed = gameObject.GetComponent<TankProp>().speed;
	}

    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer) return;
       
        // WASD Movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        // MOUSE Movement
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mouse_pos - transform.position) * Quaternion.Euler(0,0,90);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.tag);

        if (coll.gameObject.tag == "Tank")              // Collision with tank.
        {
            // Code for damage to tanks.
        }

        else if (coll.gameObject.tag == "Bullet")       // Collision with bullet
        {

            if (!isServer) return;

            // If the colliding bullet is not the tank firing it, then destroy bullet and take damage equal to that of bullet.
            if (coll.gameObject.GetComponent<bulletController>().parentName != gameObject.GetComponent<TankProp>().name)
            {

                float bulletDamage = coll.gameObject.GetComponent<bulletController>().damage;
                gameObject.GetComponent<TankHealth>().takeDamage(bulletDamage);

            }
            // If the colliding bullet is of tank firing it, destory bullet. No damage to tank.
            else
            {
                // Bullet is destroyed on it's own. It's bullet's property.
            }

        }
        else if (coll.gameObject.tag == "Wall")   // Collision with Walls
        {
            // Do nothing if collision with walls
        }

    }
}
