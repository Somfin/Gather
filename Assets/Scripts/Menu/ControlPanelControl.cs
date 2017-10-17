using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPanelControl : MonoBehaviour {
    private List<GameObject> panels;
    private int currentPanel;

    private void Start()
    {
        currentPanel = 0;
    }

    public void SetPanelActive(int index)
    {
        foreach(var p in panels)
        {
            p.SetActive(false);
        }
        panels[index].SetActive(true);
        currentPanel = index;
    }

    public void StepPanel(bool positive)
    {
        var count = panels.Count();
        var newPanel = (currentPanel += (positive ? 1 : -1)) + count;
        SetPanelActive(newPanel % count);
    }

    public int AddPanel(GameObject panel)
    {
        if (panels == null)
        {
            panels = new List<GameObject>();
        }
        var index = panels.Count();
        panels.Add(panel);
        return index;
    }
}
