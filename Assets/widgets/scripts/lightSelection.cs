using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class lightSelection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Dropdown m_Dropdown; //liste des lights
    public Dropdown type_light_Dropdown; //types des lights

    public Slider slider_intensity;
    public Slider slider_temperature;

    private List<Light> allLights = new List<Light>();

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene is '" + scene.name + "'.");

        if (!scene.isLoaded)
        {
            Debug.LogError("La scéne n'est pas chargée !");
            return;
        }


        // Récupére les objets racines
        GameObject[] roots = scene.GetRootGameObjects();

        foreach (GameObject root in roots)
        {
            // récupére toutes les lights dans le root + enfants
            Light[] lights = root.GetComponentsInChildren<Light>(true);

            allLights.AddRange(lights);
        }

        // Dropdown light liste
        List<string> names = new List<string>();
        foreach (Light l in allLights)
        {
            names.Add(l.name);
        }
        m_Dropdown.AddOptions(names);

        // Dropdown light_type list
        //type_light_Dropdown.ClearOptions();
        List<string> types = new List<string>()
        {
            "Point",
            "Directional",
            "Spot",
            "Area"
        };

        type_light_Dropdown.AddOptions(types);

        
        slider_intensity.onValueChanged.AddListener((float val) => changeIntensity(val));
        slider_temperature.onValueChanged.AddListener(val => changeTemperature(val));
        type_light_Dropdown.onValueChanged.AddListener(ChangeLightType);
        m_Dropdown.onValueChanged.AddListener(OnLightSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void changeIntensity(float intensity)
    {
        if (m_Dropdown != null)
        {
            string dropdownValue = m_Dropdown.options[m_Dropdown.value].text;

            //Ne garde que la derni�re light avec le bon nom
            foreach(Light l in allLights)
            {
                if (l.name.Equals(dropdownValue))
                {
                    l.intensity = intensity;
                }
            }

        }
    }

    public void changeTemperature(float temp)
    {
        if (m_Dropdown != null)
        {
            string dropdownValue = m_Dropdown.options[m_Dropdown.value].text;

            //Ne garde que la derni�re light avec le bon nom
            foreach (Light l in allLights)
            {
                if (l.name.Equals(dropdownValue))
                {
                    l.colorTemperature = temp;
                }
            }

        }
    }

    public void ChangeLightType(int index)
    {
        if (m_Dropdown != null && allLights.Count > 0)
        {
            Light selectedLight = allLights[m_Dropdown.value];

            switch (index)
            {
                case 0:
                    selectedLight.type = LightType.Point;
                    break;
                case 1:
                    selectedLight.type = LightType.Directional;
                    break;
                case 2:
                    selectedLight.type = LightType.Spot;
                    break;
                case 3:
                    selectedLight.type = LightType.Rectangle; //représente Area car Area est déprécié
                    break;
            }
        }
    }

    void OnLightSelected(int index)
    {
        Light l = allLights[index];

        switch (l.type)
        {
            case LightType.Point: type_light_Dropdown.value = 0; break;
            case LightType.Directional: type_light_Dropdown.value = 1; break;
            case LightType.Spot: type_light_Dropdown.value = 2; break;
            case LightType.Rectangle: type_light_Dropdown.value = 3; break;
        }

        slider_intensity.value = l.intensity;
        slider_temperature.value = l.colorTemperature;
    }
}
