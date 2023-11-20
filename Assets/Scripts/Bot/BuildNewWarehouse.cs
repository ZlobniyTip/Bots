using UnityEngine;

public class BuildNewWarehouse : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private BotCollector _botCollector;

    private Vector3 _constructionSite;

    public Vector3 ConstructionSite => _constructionSite;

    public void BuildWarehouse()
    {
        Stock stock = Instantiate(_stock, _constructionSite, Quaternion.identity);
        stock.GetLinkPlayer(_stock.Player);
        stock.ResetAmountResources();
        stock.AddBotToList(_botCollector);
        _stock.Player.DestroyFlag();
    }

    public void GetConstructionInstructions(Vector3 constructionSite)
    {
        _constructionSite = constructionSite;
    }

    public void GetLink(Stock stock)
    {
        _stock = stock;
    }
}
