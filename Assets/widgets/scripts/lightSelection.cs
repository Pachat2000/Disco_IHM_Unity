using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class lightSelection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Dropdown m_Dropdown;

    public Slider slider_intensity;

    private List<Light> allLights = new List<Light>();

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene is '" + scene.name + "'.");

        if (!scene.isLoaded)
        {
            Debug.LogError("La scène n'est pas chargée !");
            return;
        }


        //Debug.Log("List GameObject on");

        // Récupère les objets racines
        GameObject[] roots = scene.GetRootGameObjects();
        //Debug.Log("GameObject[] on");

        foreach (GameObject root in roots)
        {
            // récupère toutes les lights dans le root + enfants
            Light[] lights = root.GetComponentsInChildren<Light>(true);

            allLights.AddRange(lights);
        }

        // Dropdown
        List<string> names = new List<string>();
        foreach (Light l in allLights)
        {
            names.Add(l.name);
        }

        m_Dropdown.AddOptions(names);
        slider_intensity.onValueChanged.AddListener((float val) => changeIntensity(val));
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
            Debug.Log(dropdownValue);

            Light selected_light;

            //Ne garde que la dernière light avec le bon nom
            foreach(Light l in allLights)
            {
                if (l.name.Equals(dropdownValue))
                {
                    selected_light = l;
                    selected_light.intensity = intensity;
                }
            }

        }
    }
}
