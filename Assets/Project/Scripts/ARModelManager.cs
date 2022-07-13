using System;
using UnityEngine;
using UnityEngine.UI;

public class ARModelManager : MonoBehaviour
{
    [SerializeField] private Transform arCameraTransform;
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private Button spawnButton;
    [SerializeField] private Button destroyButton;
    [SerializeField] private Button pushButton;
    [SerializeField] private float forwardDistanceToSpawn;
    [SerializeField] private float pushDistance;

    private GameObject _spawnedObject;

    public event Action<GameObject> OnModelSpawned;
    public event Action OnModelDestroyed;

    public GameObject SpawnedObject => _spawnedObject;

    private void Awake()
    {
        spawnButton.onClick.AddListener(CreateOrUpdateModel);
        destroyButton.onClick.AddListener(DestroyHandler);
        pushButton.onClick.AddListener(PushHandler);
    }

    private void CreateOrUpdateModel()
    {
        if (_spawnedObject == null)
        {
            _spawnedObject = Instantiate(modelPrefab, ObjectNewPosition, Quaternion.identity);
            OnModelSpawned?.Invoke(_spawnedObject);
        }
    }

    private void DestroyHandler()
    {
        Destroy(_spawnedObject);
        _spawnedObject = null;
        OnModelDestroyed?.Invoke();
    }


    private void PushHandler()
    {
        var previousPos = _spawnedObject.transform.position;
        var cameraForward = arCameraTransform.forward;

        var newPos = previousPos + new Vector3(arCameraTransform.forward.x, 0, arCameraTransform.forward.z) * pushDistance;

        Destroy(_spawnedObject);
        _spawnedObject = Instantiate(modelPrefab, newPos, Quaternion.identity);
    }

    private Vector3 ObjectNewPosition => arCameraTransform.position + arCameraTransform.forward * forwardDistanceToSpawn;
}