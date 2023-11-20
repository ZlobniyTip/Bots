using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private BotMovement _botMovement;
    [SerializeField] private BuildNewWarehouse _buildNewWarehouse;
    [SerializeField] private Collection _collection;

    public Stock Stock { get; private set; }
    public bool CarriesResource { get; private set; } = false;
    public Resource TargetResource { get; private set; }
    public bool IsBuilder { get; private set; } = false;

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
        _buildNewWarehouse.GetLink(Stock);
    }

    public void GoBuild(Vector3 constructionSite)
    {
        BecomeBuilder();
        _buildNewWarehouse.GetConstructionInstructions(constructionSite);
    }

    public void BecomeBuilder()
    {
        IsBuilder = true;
    }

    public void BecomeCollector()
    {
        IsBuilder = false;
    }
}