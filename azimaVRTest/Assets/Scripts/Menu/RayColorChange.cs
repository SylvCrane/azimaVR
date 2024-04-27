using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayColorChange : MonoBehaviour
{
    public void changeColorWhenHovered()
    {
        gameObject.GetComponent<Image>().color = new Color(180, 180, 180);
    }

    public void changeColorWhenUnhovered()
    {
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public void changeColorWhenSelected()
    {
        gameObject.GetComponent<Image>().color = new Color(128, 128, 128);
    }
}
