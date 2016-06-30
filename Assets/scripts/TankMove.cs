using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankMove : NetworkBehaviour {

    public float speed = 1.0f;          // Speed at which the tank moves.

	// Use this for initialization
	void Start () {

	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
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
}
