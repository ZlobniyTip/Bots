using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsBooked { get; private set; } = false;
    public bool IsAdded { get; private set; } = false;

    public void AddToList()
    {
        IsAdded = true;
    }

    public void Reserve()
    {
        IsBooked = true;
    }

    public void DestroyResource()
    {
        Destroy(gameObject);
    }
}