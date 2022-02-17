using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MyRaycastManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _aRRaycastManager;
    [SerializeField] private TMP_Text _uiText;
    [SerializeField] private TMP_Text _toSpawnObjectName;
    [SerializeField] private GameObject _hitMark;
    [SerializeField] private GameObject _spawnPivot;

    private bool _markActive = false;
    private int _spawnsIndex = 0;
    [SerializeField] private GameObject[] _spawns = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        InsertObjectName();
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    public void CastRay()
    {
        Ray ray = new Ray(transform.localPosition, transform.forward);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (_aRRaycastManager.Raycast(ray, hits, TrackableType.AllTypes))
        {
            //_uiText.text = hits[0].sessionRelativeDistance.ToString();
            _hitMark.SetActive(true);
            _hitMark.transform.position = new Vector3(hits[0].trackable.transform.position.x, hits[0].trackable.transform.position.y, hits[0].trackable.transform.position.z);
            _uiText.text = "Mark position is :" + _hitMark.transform.position.ToString();
            //_toSpawnObjectName.text = _hitMark.transform.position.ToString();
            _markActive = true;
        }
        else
        {
            _uiText.text = "NO hits man!!";
            _hitMark.SetActive(false);
            _markActive = false;
        }
        
    }
    public void SpawnObject()
    {
        if (_markActive)
        {
            Instantiate(_spawns[_spawnsIndex], _hitMark.transform.position, _hitMark.transform.rotation, _spawnPivot.transform );
        }
        else
        {
            return;
        }
    }

    public void IncreaseIndex()
    { 
        _spawnsIndex++;

        if (_spawnsIndex > _spawns.Length -1)
        {
            _spawnsIndex = 0;
        }
        InsertObjectName();
    }

    public void DecreaseIndex()
    {
        _spawnsIndex--;

        if (_spawnsIndex < 0)
        {
            _spawnsIndex = _spawns.Length - 1;
        }
        InsertObjectName();
    }

    public void InsertObjectName()
    {
        switch (_spawnsIndex)
        {
            case 0:
                _toSpawnObjectName.text = "Cube";
                break;
            case 1:
                _toSpawnObjectName.text = "Sphere";
                break;
            case 2:
                _toSpawnObjectName.text = "Capsule";
                break;         
        }

    }
} 
