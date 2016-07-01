using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// THE SCRIPT ATTACHED TO NETWORK MANAGER
// HELPS TO TWEAK THINGS IN THE NETWORK AS WE WANT TO
public class Session : NetworkManager {

    // Manually spawning tanks on server and replicating them on clients.
    // Wanted to set the name of the tanks and then spawn them.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab);
        player.name = "Tank" + numPlayers;
        player.GetComponent<TankProp>().name = "Tank" + numPlayers;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // OnClientConnect runs on the client side and calls the above function which is run on the server
    // This function can be used to assign custom player names
    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn,0);
    }

}
