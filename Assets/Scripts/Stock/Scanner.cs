using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Resource))]
public class Scanner : MonoBehaviour
{
    private Queue<Resource> _foundResources = new Queue<Resource>();
    private Resource _resource;

    public int FoundResourceCount => _foundResources.Count;

    private void Start()
    {
        _resource = GetComponent<Resource>();
        _resource.AddToList();
        StartCoroutine(ResourceSearch());
    }

    public Resource ShowResourceCoordinates()
    {
        return _foundResources.Dequeue();
    }

    private IEnumerator ResourceSearch()
    {
        while (true)
        {
            _resource = FindObjectOfType<Resource>();

            if (_resource.IsAdded == false)
            {
                _foundResources.Enqueue(_resource);
                _resource.AddToList();
            }

            yield return null;
        }
    }
}
