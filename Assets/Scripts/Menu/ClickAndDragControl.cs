using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndDragControl : MonoBehaviour {
    public GameObject swapLeft;
    public GameObject swapRight;
    public GameObject swapIn;
    public GameObject swapOut;

    private AbstractItemListControl control;

    public void Start()
    {
        control = GetComponentInParent<AbstractItemListControl>();
    }

    public void Hover()
    {
        var childCount = gameObject.transform.parent.childCount;
        var siblingIndex = gameObject.transform.GetSiblingIndex();
        if (control.allowSwapSideways)
        {
            if (siblingIndex != 0)
            {
                swapLeft.SetActive(true);
            }
            if (siblingIndex < childCount - 1)
            {
                swapRight.SetActive(true);
            }
        }
        if (control.allowSwapUp)
        {
            swapIn.SetActive(true);
        }
        if (control.allowSwapDown)
        {
            swapOut.SetActive(true);
        }
    }

    public void SwapLeft()
    {
        var siblingIndex = gameObject.transform.GetSiblingIndex();
        control.SwapLeft(siblingIndex);
    }

    public void SwapRight()
    {
        var siblingIndex = gameObject.transform.GetSiblingIndex();
        control.SwapRight(siblingIndex);
    }

    public void SwapIn()
    {
        var siblingIndex = gameObject.transform.GetSiblingIndex();
        control.SwapIn(siblingIndex);
    }

    public void SwapOut()
    {
        var siblingIndex = gameObject.transform.GetSiblingIndex();
        control.SwapOut(siblingIndex);
    }

    public void UnHover()
    {
        swapLeft.SetActive(false);
        swapRight.SetActive(false);
        swapIn.SetActive(false);
        swapOut.SetActive(false);
    }
}
