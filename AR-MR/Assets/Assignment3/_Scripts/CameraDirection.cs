using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraDirection : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager _cameraView;
    //[SerializeField]
    //public TMP_Text _toggleCameraText;

    // Start is called before the first frame update
    void Start()
    {
        _cameraView = GetComponent<ARCameraManager>();
    }

    public void ChangeCameraDirection()
    {
        //Debug.Log("Current view: " + _cameraView.currentFacingDirection.ToString());
        if (_cameraView.requestedFacingDirection == CameraFacingDirection.User)
        {
            _cameraView.requestedFacingDirection = CameraFacingDirection.World;
            //Debug.Log("To User " + _cameraView.currentFacingDirection.ToString());

            //_toggleCameraText.text = "To REAR Camera";
           
        }
        else
        {
            _cameraView.requestedFacingDirection = CameraFacingDirection.User;
            //Debug.Log("To rear " + _cameraView.currentFacingDirection.ToString());
            
            //_toggleCameraText.text = "To FRONT Camera";
        }
    }
}
