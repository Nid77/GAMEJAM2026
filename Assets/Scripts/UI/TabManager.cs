using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabManager : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button button;
        public GameObject panel;
    }

    [SerializeField] private List<Tab> tabs;

    private void Start()
    {
        foreach (var tab in tabs)
        {
            Tab capturedTab = tab;
            tab.button.onClick.AddListener(() => ShowTab(capturedTab));
        }

        ShowTab(tabs[0]);
    }

    private void ShowTab(Tab selected)
    {
        foreach (var tab in tabs){
            
            if (tab == selected){
                tab.panel.SetActive(true);
                tab.button.interactable = false;
            }
            else{
                tab.panel.SetActive(false);
                tab.button.interactable = true;
            }

        }
            
    }
}