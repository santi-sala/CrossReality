using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackImage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabsToBeSpawn;
    [SerializeField]    
    private GameObject _mainPanel;
    //private bool _setCanvas = false;

   
    private Dictionary<string, GameObject> _spawnedPrefabs = new Dictionary<string, GameObject>();
    
    private ARTrackedImageManager _trackedImageManager;

    private void Awake()
    {
        
        _trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (GameObject prefabToBeSpawned in _prefabsToBeSpawn)
        {
            GameObject newPrefab = Instantiate(prefabToBeSpawned, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefabToBeSpawned.name;
            _spawnedPrefabs.Add(prefabToBeSpawned.name, newPrefab);
        }
    }

    private void Start()
    {        
        _mainPanel.SetActive(false);
    }



    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
            _mainPanel.SetActive(false);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _spawnedPrefabs[trackedImage.name].SetActive(false);
            //_canvas.enabled = false;
            _mainPanel.SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        Vector3 imagePosition = trackedImage.transform.position;

        GameObject prefab = _spawnedPrefabs[imageName];
        prefab.transform.position = imagePosition;
        prefab.SetActive(true);
        //_canvas.enabled = true;
        _mainPanel.SetActive(true);

        foreach (GameObject go in _spawnedPrefabs.Values)
        {
            if (go.name != imageName )
            {
                go.SetActive(false);
            }
        }
    }
}
