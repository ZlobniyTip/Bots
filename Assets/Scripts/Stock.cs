using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private BotCollector[] _initialBots;
    [SerializeField] private BotCollector _botPrefab;

    private List<BotCollector> _allBots = new List<BotCollector>();
    private Queue<Resource> _resources = new Queue<Resource>();
    private Queue<BotCollector> _freeBots = new Queue<BotCollector>();
    private Coroutine _findFreeBots;
    private Resource _resource;
    private int _resourceCount;
    private int _unitCost = 3;

    private void Start()
    {
        for (int i = 0; i < _initialBots.Length; i++)
        {
            _allBots.Add(_initialBots[i]);
        }

        _findFreeBots = StartCoroutine(FindFreeBots());
        StartCoroutine(SendCollect());
    }

    private void Update()
    {
        _resource = FindObjectOfType<Resource>();

        if (_resourceCount >= _unitCost)
        {
            BuyUnit();
        }

        if (_resource.IsAdded == false)
        {
            _resources.Enqueue(_resource);
            _resource.AddToList();
        }
    }

    private void BuyUnit()
    {
        _resourceCount -= _unitCost;
        var bot = Instantiate(_botPrefab, transform.position, Quaternion.identity);
        bot.GetWarehouse(this);
        StopCoroutine(_findFreeBots);
        _allBots.Add(bot);
        _findFreeBots = StartCoroutine(FindFreeBots());
    }

    private IEnumerator SendCollect()
    {
        while (true)
        {
            if (_resources.Count > 0)
            {
                if (_freeBots.Count > 0)
                {
                    BotCollector currentBot = _freeBots.Dequeue();

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

            yield return null;
        }

    }

    private IEnumerator FindFreeBots()
    {
        while (true)
        {
            foreach (var bot in _allBots)
            {
                if (bot.CarriesResource == false)
                {
                    _freeBots.Enqueue(bot);
                }
            }

            yield return null;
        }
    }

    public void GetResource()
    {
        _resourceCount++;
    }
}