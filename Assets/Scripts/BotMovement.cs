using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private BotCollector _botCollector;
    [SerializeField] private Collection _collection;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_botCollector.CarriesResource == false)
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

        if (_botCollector.CarriesResource == true)
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