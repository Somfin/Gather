using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPanelUtilities : MonoBehaviour {
    private ControlPanelControl control;

    private void Start()
    {
        control = GetComponentInParent<ControlPanelControl>();
    }

    public void SetPanel(int panel)
    {
        control.SetPanelActive(panel);
    }

    public void StepPanel(bool positive)
    {
        control.StepPanel(positive);
    }
}
