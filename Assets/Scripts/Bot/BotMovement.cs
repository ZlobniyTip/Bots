using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private BotCollector _botCollector;
    [SerializeField] private Collection _collection;
    [SerializeField] private BuildNewWarehouse _buildNewWarehouse;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_botCollector.IsBuilder == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _buildNewWarehouse.ConstructionSite, _speed * Time.deltaTime);

            if (transform.position == _buildNewWarehouse.ConstructionSite)
            {
                _buildNewWarehouse.BuildWarehouse(_stock);

                _botCollector.BecomeCollector();
            }
        }
        else if (_botCollector.CarriesResource == false)
        {
            if (_botCollector.TargetResource != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _botCollector.TargetResource.transform.position, _speed * Time.deltaTime);

                if (transform.position == _botCollector.TargetResource.transform.position)
                {
                    _collection.PickUp();
                }
            }
        }
        else if (_botCollector.CarriesResource == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _stock.transform.position, _speed * Time.deltaTime);

            if (transform.position == _stock.transform.position)
            {
                _collection.Drop();
            }
        }
    }

    public void GetLink(Stock stock)
    {
        _stock = stock;
    }
}