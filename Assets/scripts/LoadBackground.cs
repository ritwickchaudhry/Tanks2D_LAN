using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class LoadBackground : NetworkBehaviour
{
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject DownWall;
    public GameObject UpWall;

    // Use this for initialization
    public override void OnStartServer () {

        var left = Instantiate(LeftWall);
        var right = Instantiate(RightWall);
        var down = Instantiate(DownWall);
        var up = Instantiate(UpWall);

        NetworkServer.Spawn(left);
        NetworkServer.Spawn(right);
        NetworkServer.Spawn(up);
        NetworkServer.Spawn(down);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
