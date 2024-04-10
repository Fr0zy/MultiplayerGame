using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private int maxPlayers = 3;

    [SerializeField] private playerscript playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsHost)
        {
            gameObject.SetActive(false);
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong id)
    {
        playerSpawner.SpawnPlayer(id);

        if (NetworkManager.ConnectedClientsIds.Count < maxPlayers)
            return;

        enemySpawner.Isinitialized = true;
    }
}
