using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetcodePlayer : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsHost)
            return;

        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;

    }

    private void Singleton_OnClientConnectedCallback(ulong id)
    {
        Debug.Log($"Client connected with ID: {id}");
    }
}
