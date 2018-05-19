using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {

	public GameObject playerPrefab;

	public override void OnStartLocalPlayer() {
		CmdSpawnPlayer();
	}

	[Command]
	void CmdSpawnPlayer() {
		GameObject player = Instantiate(playerPrefab);
		NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
		player.GetComponent<Player>().playerConnection = gameObject;
	}
}
