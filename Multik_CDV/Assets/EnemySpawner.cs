using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float enemySpeed = 1f;
    private float _counter = 0f;
    private bool _initialized = false;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsHost)
        {
            gameObject.SetActive(false);
            return;
        }

        _initialized = true;
    }

    public void Update()
    {
        if(_counter < spawnRate)
        {
            _counter += Time.deltaTime;
        }
        else
        {
            _counter = 0;
            var childIndex = Random.Range(0, transform.childCount);
            var child = transform.GetChild(Random.Range(0, transform.childCount));

            var spawned = NetworkManager.SpawnManager.InstantiateAndSpawn(enemyPrefab, NetworkManager.LocalClientId);
            spawned.transform.position = child.position;
            Rigidbody childRb = spawned.GetComponent<Rigidbody>();
            
            childRb.isKinematic = false;
            childRb.velocity = (childIndex == 0 ? Vector3.right : Vector3.left) * enemySpeed;
            Destroy(childRb.gameObject, 5f);
        }
    }
}
