using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;     
public class CameraSelection : MonoBehaviour
{
    public Dropdown cameraSelection;
    private List<Camera> allCamera = new List<Camera>();
    private Camera currentCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(!scene.isLoaded){
            Debug.LogError("La scène n'est pas chargée !");
            return;
        }

        GameObject[] roots = scene.GetRootGameObjects();

        foreach(GameObject root in roots){
            Camera[] cameras = root.GetComponentsInChildren<Camera>(true);
            allCamera.AddRange(cameras);
        }

        List<string> names = new List<string>();
        foreach(Camera camera in allCamera){
            camera.enabled = false;
            names.Add(camera.name);
        }   
        cameraSelection.AddOptions(names);

        cameraSelection.onValueChanged.AddListener(OncameraSelected);
        OncameraSelected(cameraSelection.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OncameraSelected(int index){
        if(allCamera.Count == 0) return;
        if(currentCamera != null){
            currentCamera.enabled = false;
            if(currentCamera.GetComponent<AudioListener>() != null)currentCamera.GetComponent<AudioListener>().enabled = false;
        }  
        Camera cameraSelected = allCamera[index];
        cameraSelected.enabled = true;
        if(cameraSelected.GetComponent<AudioListener>() != false) cameraSelected.GetComponent<AudioListener>().enabled = true;
        currentCamera = cameraSelected;
    }
}
