using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _resource;
    [SerializeField] private float _delayBetweenSpawns;
    [SerializeField] private float _maxDistanceX;
    [SerializeField] private float _maxDistanceZ;

    private Vector3 _spawnPosition;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var delayBeetweenSpawns = new WaitForSeconds(_delayBetweenSpawns);

        while (true)
        {
            _spawnPosition = new Vector3(Random.Range(0, _maxDistanceX), -0.45f, Random.Range(0, _maxDistanceZ));
            Instantiate(_resource, _spawnPosition, Quaternion.identity);

            yield return delayBeetweenSpawns;
        }
    }
}
