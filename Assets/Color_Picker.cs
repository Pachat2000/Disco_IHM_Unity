using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Color_Picker : MonoBehaviour
{
    public Slider red;
    public Slider green;
    public Slider blue;
    public RawImage color;
    public float puissanceEblouissement = 20f; 
    // Puissance de la lumière émise sur le sol/décor
    public float intensiteLumineuseSoleil = 100f; 
    // Portée de la lumière du soleil
    public float porteeSoleil = 50f;

    public Toggle myToggle;
    private bool activated;

    void Start()
    {
        myToggle.onValueChanged.AddListener(ActivateWidget);
        activated = myToggle.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        float valueR = red.value;
        float valueG = green.value;
        float valueB = blue.value;
        Color newColor = new Color(valueR, valueG, valueB);
        color.color = new Color(valueR, valueG, valueB);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {   
            if(activated)
                ApplyColor(newColor);
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
            Light lumiere =  hit.collider.GetComponent<Light>();

            if (lumiere != null)
            {
                lumiere.color = color.color;
            }
            else{
                GameObject objetTouche = hit.collider.gameObject;               
                lumiere = objetTouche.AddComponent<Light>();
                lumiere.type = LightType.Point;
                lumiere.range = 5f;             
                lumiere.intensity = 5f;         
                lumiere.color = color.color;
            }
            
        }   
        
    }

    public void ActivateWidget(bool isOn)
    {
        activated = isOn;
    }
}
