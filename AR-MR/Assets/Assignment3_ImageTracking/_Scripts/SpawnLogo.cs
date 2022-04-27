using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnLogo : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager _imageManager;
    [SerializeField]

    private GameObject _scanMode;
    [SerializeField]
    private GameObject _visitWebSitePanel;

    [SerializeField]
    private TMP_Text _btnText;
    private string _url;

    [SerializeField]
    private GameObject[] _prefabsToBeSpawned;
    [SerializeField]
    private Dictionary<string, GameObject> _spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _imageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (GameObject prefab in _prefabsToBeSpawned)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            _spawnedPrefabs.Add(prefab.name, newPrefab);
        }        
    }
    private void OnEnable()
    {
        _imageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= ImageChanged;
        
    }

    void Update()
    {       
        if (_imageManager.trackables.count <= 0)
        {
            _visitWebSitePanel.SetActive(false);
            _scanMode.SetActive(true);
        }
        else
        {
            _scanMode.SetActive(false);
            _visitWebSitePanel.SetActive(true);
        }
        
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            InstantiateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            InstantiateImage(trackedImage);
           
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _spawnedPrefabs[trackedImage.name].SetActive(false);
            _visitWebSitePanel.SetActive(false);
            _scanMode.SetActive(true);
           
        }
    }

    private void InstantiateImage(ARTrackedImage image)
    {
        string name = image.referenceImage.name;
        Vector3 imagePosition = image.transform.position;

        switch (name)
        {
            case "JAMK_LOGO":
                _btnText.text = "Visit JAMK's Website";
                _url = "http://jamk.fi";
                break;
            case "Hahmotin":
                _btnText.text = "Hahmotin";
                _url = "http://google.com";
                break;
            default:
                break;
        }
        GameObject prefab = _spawnedPrefabs[name];
        prefab.transform.position = imagePosition;
        prefab.SetActive(true);
        

        foreach (GameObject go in _spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
    }

    public void OpenWebsite()
    {
        Application.OpenURL(_url);
    }

}
