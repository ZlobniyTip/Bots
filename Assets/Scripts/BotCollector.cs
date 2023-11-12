using UnityEngine;

public class BotCollector : MonoBehaviour
{
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
}