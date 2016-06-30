using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour {

    public GameObject parent;           // The tank which has fired this bullet
    public float damage;                // Damage that this bullet can do
    public float radius;                // Radius of the bullet
    public Vector3 currVelocity;
    private float prevScale;

    // Use this for initialization
    void Start ()
    {
        // Change the radius to the given radius
        gameObject.transform.localScale = new Vector3(1,1,1) * radius;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Destroy the bullet if it collides with any tank (other than the one firing), wall or another bullet.

        if (coll.gameObject.tag == "Tank")                  // Collision with tank
        {
            if (coll.gameObject.name != parent.name)        // If not parent tank, destroy bullet. 
            {
                Destroy(gameObject);
            }
            else                                            // If parent tank, don't destory
            {
                //Shouldn't we Destory the bullet here as well? And we need to find a go around here
                //Collision is always there at the time of spawning of the bullet
                //Also if we don't destroy it then we need to let it go through the parent tank
                //As it is imparting a velocity to the tank that stays after the collision after moving also     :P 
                //Destroy(gameObject);
            }
        }
        else if (coll.gameObject.tag == "Bullet")           // Collision with bullet
        {
            if (coll.gameObject.GetComponent<bulletController>().parent.name != parent.name)
            {
                Destroy(gameObject);
            }
        }
        else if (coll.gameObject.tag == "Wall")             // Collision with wall
        {
            //Destroy(gameObject)
            //If we want to bounce back the bullets from the walls----------------------------
            if (coll.gameObject.name == "DownWall" || coll.gameObject.name == "UpWall")
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = currVelocity;
                currVelocity.y = -1 * currVelocity.y;
                gameObject.GetComponent<Rigidbody2D>().velocity = currVelocity;
            }
            if (coll.gameObject.name == "LeftWall" || coll.gameObject.name == "RightWall")
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = currVelocity;
                currVelocity.x = -1 * currVelocity.x;
                gameObject.GetComponent<Rigidbody2D>().velocity = currVelocity;
            }
            //--------------------------------------------------------------------------------
        }
    }
    // Update is called once per frame
    void Update () {
        //Maintain the current velocity of the bullet
        currVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
	}
}
