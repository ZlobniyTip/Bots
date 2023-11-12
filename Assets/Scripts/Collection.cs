using UnityEngine;

public class Collection : MonoBehaviour
{
    [SerializeField] private BotCollector _botCollector;
    [SerializeField] private Stock _stock;

    public void PickUp()
    {
        _botCollector.TargetResource.transform.position = default;
        _botCollector.TargetResource.transform.SetParent(transform, worldPositionStays: false);
        _botCollector.TargetResource.transform.localPosition += new Vector3(0, 0, 1);

        _botCollector.RaiseResource();
    }

    public void Drop()
    {
        _botCollector.TargetResource.transform.parent = null;
        _botCollector.Expect();
        _stock.GetResource();
    }
}