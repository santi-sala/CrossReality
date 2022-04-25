using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
	[SerializeField]
	private AudioClip _screenshotSound;
	[SerializeField]
	private GameObject[] _UiElements = new GameObject[2];
	
	private AudioSource _cameraSFX;

    private void Start()
    {
       _cameraSFX = GetComponent<AudioSource>();
    }
    public void StartScreenshot()
	{
		StartCoroutine(TakeScreenShot());
	}

	IEnumerator TakeScreenShot()
	{
		_cameraSFX.clip = _screenshotSound;
		_cameraSFX.Play();

        foreach (var uiElement in _UiElements)
        {
			uiElement.SetActive(false);
        }

		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		ScreenCapture.CaptureScreenshot(pathToSave);
		//yield return new WaitForEndOfFrame();
		Debug.Log("yup");
		yield return new WaitForSeconds(2);
		Debug.Log("klok");
		foreach (var uiElement in _UiElements)
		{
			uiElement.SetActive(true);
			Debug.Log("yes");
		}
		Debug.Log("done");

	}
}
