using UnityEngine;

public class BuildNewWarehouse : MonoBehaviour
{
    [SerializeField] private BotCollector _botCollector;

    private Vector3 _constructionSite;

    public Vector3 ConstructionSite => _constructionSite;

    public void BuildWarehouse(Stock stock)
    {
        Stock newStock = Instantiate(stock, _constructionSite, Quaternion.identity);
        _botCollector.transform.parent = null;
        newStock.GetLinkPlayer(stock.Player);
        stock.RelocateCollector(_botCollector);
        _botCollector.GetWarehouse(newStock);
        newStock.AddBotToList(_botCollector);
        newStock.FirstStartCoroutine();
        newStock.Player.DestroyFlag();
    }

    public void GetConstructionInstructions(Vector3 constructionSite)
    {
        _constructionSite = constructionSite;
    }
}