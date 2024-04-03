using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JetBrains.Rider.Unity.Editor;

public class playerscript : NetworkBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private string message;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner)
        {
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void OnClientConnected(ulong id)
    {
        Debug.Log($"Player joined. ID: {id}");
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageRpc(message);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpRpc(NetworkManager.LocalClientId);
        }
    }

    [Rpc(target: SendTo.Server)]
    public void JumpRpc(ulong clientID)
    {
        NetworkObject playerObject = NetworkManager.ConnectedClients[clientID].PlayerObject;
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
    }


    [Rpc(target: SendTo.ClientsAndHost)] 

    private void SendMessageRpc(string msg)
    {
        Debug.Log(msg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsHost) return;
        NetworkManager.Singleton.DisconnectClient(NetworkManager.LocalClientId);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PlayerLostRPC(ulong clientID)
    {
        NetworkManager.ConnectedClients[clientID].PlayerObject.transform.position = new Vector3(0f, -30f, 0f);
    }
}
