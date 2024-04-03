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
        if(_counter < spawnRate)
        {
            _counter += Time.deltaTime;
        }
        else
        {
            _counter = 0;
            var childIndex = Random.Range(0, transform.childCount - 1);
            var child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            Rigidbody childRb = Instantiate(enemyPrefab, child).GetComponent<Rigidbody>();
            childRb.isKinematic = false;
            childRb.velocity = (childIndex == 0 ? Vector3.right : Vector3.left) * enemySpeed;
            Destroy(childRb.gameObject, 5f);
        }
    }

}
