using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour{
    public GameObject startPanel;
    public GameObject mapPanel;
    public GameObject shipPanel;

    // Method to show the map panel
    public void ShowMapPanel()
    {
        startPanel.SetActive(false);
        mapPanel.SetActive(true);
        shipPanel.SetActive(false);
    }

    // Method to show the ship panel
    public void ShowShipPanel()
    {
        startPanel.SetActive(false);
        mapPanel.SetActive(false);
        shipPanel.SetActive(true);
    }

    // Method to go back to the start panel
    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        mapPanel.SetActive(false);
        shipPanel.SetActive(false);
    }
}
