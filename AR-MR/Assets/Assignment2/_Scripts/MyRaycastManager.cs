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
    [SerializeField] private TMP_Text _markText;
    [SerializeField] private GameObject _hitMark;
    // Start is called before the first frame update
    void Start()
    {
        
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
            _uiText.text = hits[0].trackable.transform.ToString();
            _hitMark.transform.position = new Vector3(hits[0].trackable.transform.position.x, hits[0].trackable.transform.position.y, hits[0].trackable.transform.position.z);
            _markText.text = _hitMark.transform.position.ToString();
        }
        else
        {
            _uiText.text = "NO hits man!!";
            _hitMark.SetActive(false);
        }
        
    }
}
