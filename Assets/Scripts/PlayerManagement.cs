using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Camera _camera;

    private Flag _currentFlag;

    public Flag CurrentFlag => _currentFlag;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<ResourceGenerator>(out ResourceGenerator ground))
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
            Destroy(_currentFlag.gameObject);
        }

        _currentFlag = Instantiate(_flagPrefab, placeForFlag, Quaternion.identity);
    }

    public void DestroyFlag()
    {
        Destroy(_currentFlag.gameObject);
    }
}