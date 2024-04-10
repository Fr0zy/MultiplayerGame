using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private int maxPlayers = 1;

    [SerializeField] private PlayerSpawner playerSpawner;
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

    private int _playerLost = 0;
    [Rpc(SendTo.Server)]
    public void PlayerLostRPC()
    {
        _playerLost++;

        if (_playerLost >= maxPlayers)
        {
            Debug.Log("The game should reset");

            foreach (var clientID in NetworkManager.ConnectedClientsIds)
            {
                playerSpawner.SpawnPlayer(clientID);
            }

            enemySpawner.Reset();

            _playerLost = 0;

        }
    }
}
