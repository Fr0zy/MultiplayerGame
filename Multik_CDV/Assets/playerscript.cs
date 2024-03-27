using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerscript : NetworkBehaviour
{
    [SerializeField] private string message;

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
    }

    [Rpc(target: SendTo.ClientsAndHost)] 

    private void SendMessageRpc(string msg)
    {
        Debug.Log(msg);
    }
}
