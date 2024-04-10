using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    private float xOffset = 0;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsHost)
        {
            gameObject.SetActive(false);
            return;
        }

    }

    public void SpawnPlayer(ulong clientID)
    {
        xOffset = clientID * 2f;

        NetworkObject player = NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject;
        player.gameObject.transform.position = new Vector3(xOffset, 0, 0);
    }
}
