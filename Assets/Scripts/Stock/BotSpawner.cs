using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private BotCollector _botPrefab;

    public BotCollector SpawnBot()
    {
        var bot = Instantiate(_botPrefab, transform.position, Quaternion.identity);
        return bot;
    }
}