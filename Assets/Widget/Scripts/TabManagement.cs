using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManagement : MonoBehaviour
{
    
    public GameObject tabPanelPrefab;
    public GameObject ButtonOngletPrefab;
    public Transform tabBar;
    public Transform tabPanels;

    private List<GameObject> panels = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> tabsWidgets;

    private int inputField;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField = tabsWidgets.Count;
        //Debug.Log(inputField);
        GenerateTabs();
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void GenerateTabs()
    {
        if (inputField > 0)
        {
            this.CreateTabs(inputField);
        }
    }

    public void CreateTabs(int count)
    {

        for (int i = 0; i < count; i++)
        {
            int index = i;

            // Créer un bouton et son contenu
            GameObject btnObj = Instantiate(ButtonOngletPrefab, tabBar);
            btnObj.GetComponentInChildren<Text>().text = "" + (i + 1);
            btnObj.AddComponent<LayoutElement>();
            btnObj.GetComponent<LayoutElement>().minHeight = 10;
            btnObj.GetComponent<LayoutElement>().minWidth = 10;
            btnObj.GetComponent<LayoutElement>().preferredHeight = 40;
            btnObj.GetComponent<LayoutElement>().preferredWidth = 100;

            buttons.Add(btnObj);

            // Créer panel contenant un widget
            GameObject panel = Instantiate(tabPanelPrefab, tabPanels);
            if (tabsWidgets[i] != null)
            {
                //Debug.Log("Il y a un widget");
                GameObject widget = Instantiate(tabsWidgets[i], panel.transform);
                //Debug.Log("Widget charge");
            }
                
            panel.SetActive(false);

            // Ajouter comportement clic
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                //Debug.Log("Onglet " + index + " cliqué");
                SelectTab(index);
            });

            panels.Add(panel);

            
        }

        // Activer le premier onglet par défaut
        if (panels.Count > 0)
            SelectTab(0);
    }

    void SelectTab(int index)
    {
        //active le panel à la position index
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == index);
        }
    }

}
