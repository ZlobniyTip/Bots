using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Camera _camera;

    private GameObject _currentFlag;

    public GameObject CurrentFlag => _currentFlag;

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
        if (_currentFlag != null)
        {
            Destroy(_currentFlag);
        }

        _currentFlag = Instantiate(_flagPrefab, placeForFlag, Quaternion.identity);
    }

    public void DestroyFlag()
    {
        Destroy(_currentFlag);
    }
}