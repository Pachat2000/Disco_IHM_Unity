using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManagement : MonoBehaviour
{
    
    public GameObject tabPanelPrefab;
    public GameObject ButtonOngletPrefab;
    public Transform tabBar;

    //public Transform contentArea;

    public int inputField;
    
    //private TabManagement tabManager;

    private List<GameObject> panels = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTabs();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void GenerateTabs()
    {
        //int count;
        if (inputField > 0)
        {
            this.CreateTabs(inputField);
        }
    }

    // Devrait être dans le start
    public void CreateTabs(int count)
    {

        //ClearTabs();

        for (int i = 0; i < count; i++)
        {
            int index = i;

            // Créer bouton
            GameObject btnObj = Instantiate(ButtonOngletPrefab, tabBar);
            btnObj.GetComponentInChildren<Text>().text = "Onglet " + (i + 1);

            buttons.Add(btnObj);

            // Créer panel
            GameObject panel = Instantiate(tabPanelPrefab); //, contentArea
            
                   
            panel.SetActive(false);

            // Ajouter comportement clic
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Onglet " + index + " cliqué");
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
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == index);
        }
    }

    void ClearTabs()
    {
        foreach (Transform child in tabBar)
            Destroy(child.gameObject);

        //foreach (Transform child in contentArea)
            //Destroy(child.gameObject);

        panels.Clear();
    }
}
