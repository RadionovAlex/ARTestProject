using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ARModelManager aRModelManager;

    [SerializeField] private GameObject spawnButton;
    [SerializeField] private GameObject pushButton;
    [SerializeField] private GameObject destroyButton;

    private ARSession arSession;
    private ARSessionOrigin arSessionOrigin;

    private void Awake()
    {
        aRModelManager.OnModelSpawned += ModelSpawnedHandler;
        aRModelManager.OnModelDestroyed += ModelDestroyedHandler;

        spawnButton.gameObject.SetActive(true);

        pushButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
    }

    private void ModelDestroyedHandler()
    {
        spawnButton.gameObject.SetActive(true);

        pushButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
    }

    private void ModelSpawnedHandler(GameObject obj)
    {
        spawnButton.gameObject.SetActive(false);

        pushButton.gameObject.SetActive(true);
        destroyButton.gameObject.SetActive(true);
    }
}