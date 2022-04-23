using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public enum Accesories
{
    Hats, Glasses, Nose, Mustache
}

public class Models : MonoBehaviour
{
    [SerializeField]
    private GameObject _face;
    [SerializeField]
    private ARFaceManager _ArSession;
    [SerializeField]
    private Accesories _selectedAccesory;


    [SerializeField]
    private List<GameObject> _hats = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _eyes = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _nose = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _mustache = new List<GameObject>();

    private int _hatIndex = 0;
    private int _eyesIndex = 0;
    private int _noseIndex = 0;
    private int _mustacheIndex = 0;

    // Start is called before the first frame update
    void Start()
    {        
        _selectedAccesory = Accesories.Hats;
    }

    public void GetSelectedItem(int index)
    {
        switch (index)
        {
            case 0:
                _selectedAccesory = Accesories.Hats;
                break;
            case 1:
                _selectedAccesory = Accesories.Glasses;
                break;
            case 2:
                _selectedAccesory = Accesories.Nose;
                break ;
            case 3:
                _selectedAccesory = Accesories.Mustache;
                break;
            default:
                break;
        }
    }    

    public void ChangeAccesory(int nextOrPrevious)
    {
        _ArSession.enabled = false;

        switch (_selectedAccesory)        
        {
            case Accesories.Hats:
                DestroyCurrentAccesory("Hat");

                _hatIndex += nextOrPrevious;
                _hatIndex = CheckCurrentIndex(_hatIndex, _hats.Count);

                CreateNewAccesory(_hats[_hatIndex]);               
                break;

            case Accesories.Glasses:
                DestroyCurrentAccesory("Eyes");

                _eyesIndex += nextOrPrevious;
                _eyesIndex = CheckCurrentIndex(_eyesIndex, _eyes.Count);

                CreateNewAccesory(_eyes[_eyesIndex]);               

                break;

            case Accesories.Nose:
                DestroyCurrentAccesory("Nose");

                _noseIndex += nextOrPrevious;
                _noseIndex = CheckCurrentIndex(_noseIndex, _nose.Count);
                
                CreateNewAccesory(_nose[_noseIndex]);
                
                break;

            case Accesories.Mustache:
                DestroyCurrentAccesory("Mustache");

                _mustacheIndex += nextOrPrevious;
                _mustacheIndex = CheckCurrentIndex(_mustacheIndex, _mustache.Count);

                CreateNewAccesory(_mustache[_mustacheIndex]);
                break;

            default:
                break;
        }
        StartCoroutine(WaitForAWhile());

    }
    private void DestroyCurrentAccesory(string accesory)
    {
        GameObject _currentAccesory = FindGameObjectInChildWithTag(_face, accesory);
        Destroy(_currentAccesory);
    }

    private static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform parentTransform = parent.transform;

        for (var i = 0; i < parentTransform.childCount; i++)
        {
            if (parentTransform.GetChild(i).gameObject.tag == tag)
            {
                return  parentTransform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    private int CheckCurrentIndex(int currentIndex, int listCount)
    {
        int result;

        if (currentIndex < 0)
        {
            result = listCount - 1;
        }
        else if (currentIndex > listCount - 1)
        {
            result = 0;
        }
        else
        {
            result = currentIndex;
        }

        return result;
    }

    private void CreateNewAccesory(GameObject accesory)
    {
        GameObject newAccesory = Instantiate(accesory);
        newAccesory.transform.SetParent(_face.transform, false);
    }

    IEnumerator WaitForAWhile()
    {
        yield return new WaitForSeconds(1);
        _ArSession.enabled = true;
    }
}
