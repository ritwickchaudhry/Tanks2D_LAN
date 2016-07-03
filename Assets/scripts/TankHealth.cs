using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

// Manages health and changing health bar in tanks
public class TankHealth : NetworkBehaviour {

    public GameObject health;
    public float relativeDownDistance = 1f;

    [SyncVar]
    private float actualHealth;
    private Scrollbar healthBar;

    private float maxHealth;

    // Use this for initialization
    void Start () {

        healthBar = health.GetComponentInChildren<Scrollbar>();
        maxHealth = gameObject.GetComponent<TankProp>().maxHealth;

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
	
    // Tank's health to be reduced by the given amount
    public void takeDamage(float damage)
    {
        actualHealth -= damage;

        // If tank's health <= 0, Kill tank
        if (actualHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    // LateUpdating the health bar position
    void LateUpdate()
    {

        var rotation = health.transform.rotation.eulerAngles;
        rotation.z = 0;
        health.transform.rotation = Quaternion.Euler(rotation);

        health.transform.position = transform.position - new Vector3(0, relativeDownDistance, 0);

        healthBar.size = Mathf.Max(actualHealth / maxHealth, 0f);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
