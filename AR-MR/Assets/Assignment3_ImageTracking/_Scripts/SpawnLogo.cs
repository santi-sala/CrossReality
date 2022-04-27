using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

    private bool _trackedImage = false;

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
        if (_imageManager.trackables.count > 0)
        {
            foreach (var image in _imageManager.trackables)
            {
                if (image.trackingState == TrackingState.Tracking)
                {
                    _trackedImage = true;
                }
                else
                {
                    _trackedImage = false;
                }
            }
        }

        if (_trackedImage)
        {
            _scanMode.SetActive(false);
            _visitWebSitePanel.SetActive(true);
        }
        else
        {
            if (_spawnedPrefabs.Values != null)
            {
                foreach (var spawnedPrefab in _spawnedPrefabs.Values)
                {
                    spawnedPrefab.SetActive(false);
                }
            }
            _visitWebSitePanel.SetActive(false);
            _scanMode.SetActive(true);

        }
        //if (_imageManager.trackables.count <= 0)
        //{
        //    _visitWebSitePanel.SetActive(false);
        //    _scanMode.SetActive(true);
        //}
        //else
        //{
        //    _scanMode.SetActive(false);
        //    _visitWebSitePanel.SetActive(true);
        //}

    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            Debug.Log("ADDED Started");
            InstantiateImage(trackedImage);
            Debug.Log("ADDED Ended");
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            Debug.Log("UPDATED Started");
            InstantiateImage(trackedImage);
            Debug.Log("UPDATED Ended");

        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Debug.Log("REMOVED Started");
            _spawnedPrefabs[trackedImage.name].SetActive(false);
            _visitWebSitePanel.SetActive(false);
            _scanMode.SetActive(true);
            Debug.Log("REMOVED Ended");

        }
    }

    private void InstantiateImage(ARTrackedImage image)
    {   
        string name = image.referenceImage.name;
        Vector3 imagePosition = image.transform.position;

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
                
    }

    public void OpenWebsite()
    {
        Application.OpenURL(_url);
    }

    //it  just doesnt work i cannot manage to remove my image trackables to zero.
    //public void BackToScanMode()
    //{
    //    //Debug.Log("Kpasa 1: " + _imageManager.trackables.count.ToString());
    //    //foreach (var trackedImage in _imageManager.trackables)
    //    //{
    //    //    Debug.Log("NOSE: " + _imageManager.trackables.count.ToString());
    //    //    //trackedImage.gameObject.SetActive(false);
    //    //    trackedImage.destroyOnRemoval = true;
    //    //    //trackedImage.enabled = false;
    //    //    Destroy(trackedImage);
    //    //    Debug.Log("NOSE 1****: " + _imageManager.trackables.count.ToString());
    //    //}

    //    foreach (var spawnedPrefab in _spawnedPrefabs.Values)
    //    {
    //        spawnedPrefab.SetActive(false );
    //    }
    //    //Debug.Log("Kpasa 1****: " + _imageManager.trackables.count.ToString());

    //    _btnText.text = "";
    //    _url = "";
    //    _visitWebSitePanel.SetActive(false);
    //    _scanMode.SetActive(true);



    //}
}
