using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Color_Picker : MonoBehaviour
{
    public Slider red;
    public Slider green;
    public Slider blue;
    public RawImage color;

    public Toggle objectColorToggle;
    public Toggle lightColorToggle;

    private bool activatedMeshColor;

    private bool activatedLightColor;

    void Start()
    {
        objectColorToggle.onValueChanged.AddListener(ActivateSwitchMeshColor);
        lightColorToggle.onValueChanged.AddListener(ActivateSwitchLight);
    }

    // Update is called once per frame
    void Update()
    {
        activatedMeshColor = objectColorToggle.isOn;
        activatedLightColor = lightColorToggle.isOn;
        float valueR = red.value;
        float valueG = green.value;
        float valueB = blue.value;
        Color newColor = new Color(valueR, valueG, valueB);
        color.color = new Color(valueR, valueG, valueB);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {   
            if(activatedMeshColor){
                ApplyColor(newColor);
            }
            if(activatedLightColor){
                ApplyLightColor(newColor);
            }
        }
                
    }

    void ApplyColor(Color nouvelleCouleur)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            if (renderer != null) {

                // Change color directly
                renderer.material.color = color.color; 
                
            }
        }   
        
    }


    void ApplyLightColor(Color nouvelleCouleur)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Light lumiere =  hit.collider.GetComponent<Light>();

            if (lumiere != null)
            {
                lumiere.color = color.color;
            }

        }   
        
    }

    public void ActivateSwitchMeshColor(bool isOn)
    {
        activatedMeshColor = isOn;
    }

    public void ActivateSwitchLight(bool isOn)
    {
        activatedLightColor = isOn;
    }
}
