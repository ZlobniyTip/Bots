using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Camera _camera;

    public GameObject CurrentFlag { get; private set; } = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Ground")
                {
                    SetFlag(hit.point);
                }
            }
        }
    }

    private void SetFlag(Vector3 placeForFlag)
    {
        if (CurrentFlag != null)
        {
            Destroy(CurrentFlag);
        }

        CurrentFlag = Instantiate(_flagPrefab, placeForFlag, Quaternion.identity);
    }

    public void DestroyFlag()
    {
        Destroy(CurrentFlag);
    }
}