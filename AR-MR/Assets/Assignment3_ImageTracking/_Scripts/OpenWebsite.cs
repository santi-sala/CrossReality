using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsite : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWebsiteBtn()
    {
        Application.OpenURL("http://jamk.fi");
    }
}
