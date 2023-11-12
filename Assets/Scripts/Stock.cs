using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] BotCollector[] _initialBots;

    private Queue<BotCollector> _bots;
    private Queue<Resource> _resources;
    private Resource _resource;
    private int _resourceCount;

    private void Start()
    {
        _bots = new Queue<BotCollector>();
        _resources = new Queue<Resource>();

        for (int i = 0; i < _initialBots.Length; i++)
        {
            _bots.Enqueue(_initialBots[i]);
        }

        StartCoroutine(FindFreeBots());
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

    private IEnumerator FindFreeBots()
    {
        while (true)
        {
            for (int i = 0; i < _initialBots.Length; i++)
            {
                if (_initialBots[i].CarriesResource == false)
                {
                    PutInQueue(_initialBots[i]);
                }
            }

            yield return null;
        }
    }

    private void SendCollect()
    {
        if (_bots.Count > 0)
        {
            BotCollector currentBot = _bots.Dequeue();

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

    private void PutInQueue(BotCollector bot)
    {
        _bots.Enqueue(bot);
    }

    public void GetResource()
    {
        _resourceCount++;
    }
}