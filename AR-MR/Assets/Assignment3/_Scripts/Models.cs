using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Accesories
{
    Hats, Glasses, Nose, Mustache
}

public class Models : MonoBehaviour
{
    [SerializeField]
    private GameObject _face;
    [SerializeField]
    private Accesories _selectedAccesory;
    [SerializeField]
    private List<GameObject> _hats = new List<GameObject>();
    private int _hatIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {        
        _selectedAccesory = Accesories.Hats;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextModel()
    {

    }

    public void GetSelectedItem(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("k pasa");
                _selectedAccesory = Accesories.Hats;
                break;
            case 1:
                Debug.Log("k lo k");
                _selectedAccesory = Accesories.Glasses;
                break;
            case 2:
                Debug.Log("Nose tiu");
                _selectedAccesory = Accesories.Nose;
                break ;
            case 3:
                Debug.Log("Come on");
                _selectedAccesory = Accesories.Mustache;
                
                break;
            default:
                break;
        }
    }    

    public void ChangeAccesory(int index)
    {

        switch (_selectedAccesory)        {
            case Accesories.Hats:

                GameObject _currentHat = FindGameObjectInChildWithTag(_face, "Hat");
                Destroy(_currentHat);

                Debug.Log("Hat count is: " + _hats.Count);
                Debug.Log("Current hat index is:  " + _hatIndex);
                _hatIndex += index;

                if (_hatIndex < 0)
                {
                    _hatIndex = _hats.Count - 1;
                }
                else if (_hatIndex > _hats.Count - 1)
                {
                    _hatIndex = 0;
                }
                Debug.Log("NEW hat index is:  " + _hatIndex);


                GameObject _newHat = Instantiate(_hats[_hatIndex]);
                _newHat.transform.SetParent(_face.transform, false);
                Debug.Log("We have Hats");

                break;
            case Accesories.Glasses:
                Debug.Log("We have Glasses");
                break;
            case Accesories.Nose:
                Debug.Log("We have Nose");
                break;
            case Accesories.Mustache:
                Debug.Log("We have Mustache");
                break;
            default:
                break;
        }

    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
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
}
