using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    private int xOffset = 0;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsHost)
        {
            gameObject.SetActive(false);
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnOnClientConnected;
    }

    private void OnOnClientConnected(ulong id)
    {
        NetworkObject player = NetworkManager.Singleton.ConnectedClients[id].PlayerObject;
        player.gameObject.transform.position = new Vector3(xOffset, 0, 0);

        xOffset += 2;
    }
}
