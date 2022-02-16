using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _aRRaycastManager;
    [SerializeField] private TMP_Text _uiText;
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
            _uiText.text = hits[0].sessionRelativeDistance.ToString();
        }
        else
        {
            _uiText.text = "NO hits man!!";
        }
        
    }
}
