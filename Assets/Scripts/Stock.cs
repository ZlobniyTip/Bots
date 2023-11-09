using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] Bot[] _initialBots;

    private Queue<Bot> _bots;
    private Queue<Resource> _resources;
    private Resource _resource;
    private int _resourceCount;

    private void Start()
    {
        _bots = new Queue<Bot>();
        _resources = new Queue<Resource>();

        for (int i = 0; i < _initialBots.Length; i++)
        {
            _bots.Enqueue(_initialBots[i]);
        }
    }

    private void Update()
    {
        _resource = FindObjectOfType<Resource>();

        if (_resource.IsAdded == false)
        {
            _resources.Enqueue(_resource);
            _resource.AddToList();
        }

        if (_resources.Count > 0)
        {
            SendCollect();
        }
    }

    private void SendCollect()
    {
        if (_bots.Count > 0)
        {
            Bot currentBot = _bots.Dequeue();

            if (currentBot.TargetResource == null)
            {
                if (_resource.IsBooked == false)
                {
                    currentBot.GetInstructions(_resources.Dequeue());
                    _resource.Reserve();
                }
            }
        }
    }

    public void AddResource()
    {
        _resourceCount++;
    }

    public void PutInQueue(Bot bot)
    {
        _bots.Enqueue(bot);
    }
}