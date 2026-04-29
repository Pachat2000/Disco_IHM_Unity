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
        // On recupere les valeurs de chaque slider de couleur
        float valueR = red.value;
        float valueG = green.value;
        float valueB = blue.value;
        Color newColor = new Color(valueR, valueG, valueB);

        //on affecte cette couleur à l'image
        color.color = new Color(valueR, valueG, valueB);

        // Si on fait un clique souris
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {   
            // et que si la checkbox pour changer la couleur de la mesh est a vrai
            if(activatedMeshColor){
                ApplyColor(newColor);
            }
            // et que si la checkbox pour changer la couleur de la lumière est a vrai
            if(activatedLightColor){
                ApplyLightColor(newColor);
            }
        }
                
    }

    void ApplyColor(Color nouvelleCouleur)
    {
        // On lance un rayon depuis la position de la souris 
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Si on le rayon touche un objet ayant un BoxCollider
        if (Physics.Raycast(ray, out hit))
        {
            // On récupère la mesh de l'objet toucher et on lui applique sa nouvelle couleur
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            if (renderer != null) {

                // Change color directly
                renderer.material.color = color.color; 
                
            }
        }   
        
    }


    void ApplyLightColor(Color nouvelleCouleur)
    {
        // On lance un rayon depuis la position de la souris
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // On récupère la lumière de l'objet toucher et on lui applique sa nouvelle couleur
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
