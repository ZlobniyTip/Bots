using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private BotCollector[] _initialBots;
    [SerializeField] private PlayerManagement _player;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private float _delayBetweenAttempts;

    private List<BotCollector> _allBots = new List<BotCollector>();
    private Queue<Resource> _resources = new Queue<Resource>();
    private Queue<BotCollector> _freeBots = new Queue<BotCollector>();
    private Coroutine _findFreeBots;
    private Resource _resource;
    private bool _isStartCoroutine = false;
    private int _resourceCount;
    private int _maxBotsCount = 5;
    private int _unitCost = 3;
    private bool _isReserve = false;
    private int _warehouseCost = 5;

    public PlayerManagement Player => _player;

    private void Start()
    {
        _isReserve = false;

        for (int i = 0; i < _initialBots.Length; i++)
        {
            _allBots.Add(_initialBots[i]);
        }

        _findFreeBots = StartCoroutine(FindUnemployedBots());
        _isStartCoroutine = true;

        StartCoroutine(SendCollect());
        StartCoroutine(GetScannerData());
        StartCoroutine(TryCreateNewWarehouse());
        StartCoroutine(BuyUnit());
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
        if (_isStartCoroutine == true)
        {
            StopCoroutine(_findFreeBots);
            _isStartCoroutine = false;
        }

        foreach (var botDel in _allBots)
        {
            _allBots.Remove(botDel);
        }

        _allBots.Add(bot);

        _findFreeBots = StartCoroutine(FindUnemployedBots());
        _isStartCoroutine = true;
    }

    public void FirstStartCoroutine()
    {
        _findFreeBots = StartCoroutine(FindUnemployedBots());
        StartCoroutine(SendCollect());
    }

    private IEnumerator FindUnemployedBots()
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

    private IEnumerator BuyUnit()
    {
        while (true)
        {
            if (_resourceCount >= _unitCost)
            {
                if (_allBots.Count < _maxBotsCount)
                {
                    _resourceCount -= _unitCost;
                    BotCollector newBot = _botSpawner.SpawnBot();
                    newBot.GetWarehouse(this);

                    if (_isStartCoroutine == true)
                    {
                        StopCoroutine(_findFreeBots);
                        _isStartCoroutine = false;

                        _allBots.Add(newBot);

                        _findFreeBots = StartCoroutine(FindUnemployedBots());
                        _isStartCoroutine = true;
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator TryCreateNewWarehouse()
    {
        var delayBeetweenAttempts = new WaitForSeconds(_delayBetweenAttempts);

        while (true)
        {
            if (_resourceCount >= _warehouseCost)
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

            yield return delayBeetweenAttempts;
        }
    }

    private IEnumerator GetScannerData()
    {
        while (true)
        {
            if (_scanner.FoundResourceCount > 0)
            {
                _resources.Enqueue(_scanner.ShowResourceCoordinates());
            }

            yield return null;
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
                        _resource = _resources.Dequeue();
                        currentBot.GetInstructions(_resource);
                    }
                }
            }

            yield return null;
        }

    }
}