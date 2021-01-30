using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CollectibleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject collectible;
    [SerializeField] private int minimumSpawnTime = 45;
    [SerializeField] private int maximumSpawnTime = 75;
    [SerializeField] private float spawnRange = 4f;
    private bool _spawning = false;

    // Start is called before the first frame update
    private void Start()
    {
        Instantiate(collectible, transform);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_spawning || transform.childCount != 0) return;
        _spawning = true;
        StartCoroutine(SpawnNewCollectible());
    }

    private IEnumerator SpawnNewCollectible()
    {
        yield return new WaitForSeconds(new Random().Next(minimumSpawnTime, maximumSpawnTime));
        Instantiate(collectible, transform);
        _spawning = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
