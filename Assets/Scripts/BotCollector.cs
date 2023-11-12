using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private float _speed;

    public Resource TargetResource { get; private set; }

    public bool CarriesResource { get; private set; } = false;


    private void Update()
    {
        if (CarriesResource == false)
        {
            if (TargetResource != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetResource.transform.position, _speed * Time.deltaTime);

                if (transform.position == TargetResource.transform.position)
                {
                    PickUp();
                }
            }

        }

        if (CarriesResource == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _stock.transform.position, _speed * Time.deltaTime);

            if (transform.position == _stock.transform.position)
            {
                Drop();
            }
        }

    }

    private void Expect()
    {
        CarriesResource = false;
        TargetResource = null;
    }

    private void PickUp()
    {
        TargetResource.transform.position = default;
        TargetResource.transform.SetParent(transform, worldPositionStays: false);
        TargetResource.transform.localPosition += new Vector3(0, 0, 1);

        CarriesResource = true;
    }

    private void Drop()
    {
        TargetResource.transform.parent = null;
        Expect();
    }

    public void GetInstructions(Resource resource)
    {
        TargetResource = resource;
    }
}