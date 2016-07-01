using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankProp : NetworkBehaviour
{

    public float maxHealth = 100f;
    public GameObject health;
    public float relativeDownDistance = 1f;

    [SyncVar]
    private float actualHealth;
    private Scrollbar healthBar;

    // Use this for initialization
    void Start()
    {

        healthBar = health.GetComponentInChildren<Scrollbar>();

        // Don't rotate HealthBar with tank. Keep the rotation zero.
        var rotation = health.transform.rotation.eulerAngles;
        rotation.z = 0;
        health.transform.rotation = Quaternion.Euler(rotation);

        // Keep the HealthBar below the tank.
        health.transform.position = transform.position - new Vector3(0, relativeDownDistance, 0);

        healthBar.size = 1f;

        if (!isServer)
        {
            return;
        }

        // Initializing the tank with full health
        actualHealth = maxHealth;
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
            //if (coll.gameObject.GetComponent<bulletController>().parent.name != gameObject.name)
            {

                float bulletDamage = coll.gameObject.GetComponent<bulletController>().damage;
                actualHealth -= bulletDamage;

                // If tank's health <= 0, Kill tank
                if (actualHealth <= 0)
                {
                    Destroy(gameObject);
                }

            }
            // If the colliding bullet is of tank firing it, destory bullet. No damage to tank.
            //else
            {
                // Bullet is destroyed on it's own. It's bullet's property.
            }

        }
        else if (coll.gameObject.tag == "Wall")   // Collision with Walls
        {
            // Do nothing if collision with walls
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        var rotation = health.transform.rotation.eulerAngles;
        rotation.z = 0;
        health.transform.rotation = Quaternion.Euler(rotation);

        health.transform.position = transform.position - new Vector3(0, relativeDownDistance, 0);

        healthBar.size = Mathf.Max(actualHealth / maxHealth, 0f);

    }
}
