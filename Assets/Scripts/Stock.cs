using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private BotCollector[] _initialBots;
    [SerializeField] private BotCollector _botPrefab;
    [SerializeField] private PlayerManagement _player;

    private List<BotCollector> _allBots = new List<BotCollector>();
    private Queue<Resource> _resources = new Queue<Resource>();
    private Queue<BotCollector> _freeBots = new Queue<BotCollector>();
    private Coroutine _findFreeBots;
    private Resource _resource;
    private bool _startCoroutine = false;
    private bool _isReserve = false;
    private int _resourceCount;
    private int _maxBotsCount = 5;
    private int _warehouseCost = 5;
    private int _unitCost = 3;

    public PlayerManagement Player => _player;

    private void Start()
    {
        _isReserve = false;

        for (int i = 0; i < _initialBots.Length; i++)
        {
            _allBots.Add(_initialBots[i]);
        }

        _findFreeBots = StartCoroutine(FindFreeBots());
        _startCoroutine = true;

        StartCoroutine(SendCollect());

    }

    private void Update()
    {
        _resource = FindObjectOfType<Resource>();

        if (_resourceCount >= _warehouseCost)
        {
            TryCreateNewWarehouse();
        }

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

    private void TryCreateNewWarehouse()
    {
        if (_player.CurrentFlag != null)
        {
            if (_isReserve == false)
            {
                _isReserve = true;
                _resourceCount -= _warehouseCost;

                var colonizeBot = _freeBots.Dequeue();

                colonizeBot.GoBuild(_player.CurrentFlag.transform.position);
                _isReserve = false;
            }
        }
    }

    private void BuyUnit()
    {
        if (_allBots.Count < _maxBotsCount)
        {
            _resourceCount -= _unitCost;
            var bot = Instantiate(_botPrefab, transform.position, Quaternion.identity);
            bot.GetWarehouse(this);

            if (_startCoroutine == true)
            {
                StopCoroutine(_findFreeBots);
                _startCoroutine = false;

                _allBots.Add(bot);

                _findFreeBots = StartCoroutine(FindFreeBots());
                _startCoroutine = true;
            }
        }
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

    public void RelocateCollector(BotCollector colonizeBot)
    {
        _allBots.Remove(colonizeBot);
    }

    public void GetResource()
    {
        _resourceCount++;
    }

    public void GetLinkPlayer(PlayerManagement player)
    {
        _player = player;
    }

    public void AddBotToList(BotCollector bot)
    {
        if (_startCoroutine == true)
        {
            StopCoroutine(_findFreeBots);
            _startCoroutine = false;
        }

        foreach (var botDel in _allBots)
        {
            _allBots.Remove(botDel);
        }



        _allBots.Add(bot);

        _findFreeBots = StartCoroutine(FindFreeBots());
        _startCoroutine = true;
    }

    public void FirstStartCoroutine()
    {
        _findFreeBots = StartCoroutine(FindFreeBots());
        StartCoroutine(SendCollect());
    }
}