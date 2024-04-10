using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private Transform enemiesParent;
    
    private float _counter = 0f;
    public bool Isinitialized = false;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsHost)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void Update()
    {
        if (!Isinitialized) return;

        _counter += Time.deltaTime;

        if(_counter >= spawnRate) {
        
            _counter = 0f;
            int spawnPointIndex =Random.Range(0, transform.childCount);
            Transform spawnPoint = transform.GetChild(spawnPointIndex);

            NetworkObject spawnedEnemy = NetworkManager.SpawnManager.InstantiateAndSpawn(enemyPrefab);
            spawnedEnemy.transform.SetParent(enemiesParent);
            spawnedEnemy.transform.position = spawnPoint.position;
            spawnedEnemy.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            Destroy(spawnedEnemy.gameObject, 5f);

            Rigidbody enemyRb = spawnedEnemy.GetComponent<Rigidbody>();
            enemyRb.velocity = (new Vector3(0, 0.5f, 0) - spawnedEnemy.transform.position).normalized * enemySpeed;
        }
    }

    public void Reset()
    {
        _counter = 0f;
        foreach (Transform child in enemiesParent) Destroy(child.gameObject);
    }
}
