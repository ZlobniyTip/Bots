using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private BotMovement _botMovement;
    [SerializeField] private Collection _collection;

    public Stock Stock { get; private set; }
    public bool CarriesResource { get; private set; } = false;
    public Resource TargetResource { get; private set; }

    public void Expect()
    {
        CarriesResource = false;
        TargetResource = null;
    }

    public void RaiseResource()
    {
        CarriesResource = true;
    }

    public void GetInstructions(Resource resource)
    {
        TargetResource = resource;
    }

    public void GetWarehouse(Stock stock)
    {
        Stock = stock;
        _botMovement.GetLink(Stock);
        _collection.GetLink(Stock);
    }
}