using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Used to switch from upperCase to lowerCase and vice versa in a keyboard
public class UpperCaseSwitch : MonoBehaviour
{
    public Button upperCase;

    public TextMeshProUGUI a;
    public TextMeshProUGUI b;
    public TextMeshProUGUI c;
    public TextMeshProUGUI d;
    public TextMeshProUGUI e;
    public TextMeshProUGUI f;
    public TextMeshProUGUI g;
    public TextMeshProUGUI h;
    public TextMeshProUGUI i;
    public TextMeshProUGUI j;
    public TextMeshProUGUI k;
    public TextMeshProUGUI l;
    public TextMeshProUGUI m;
    public TextMeshProUGUI n;
    public TextMeshProUGUI o;
    public TextMeshProUGUI p;
    public TextMeshProUGUI q;
    public TextMeshProUGUI r;
    public TextMeshProUGUI s;
    public TextMeshProUGUI t;
    public TextMeshProUGUI u;
    public TextMeshProUGUI v;
    public TextMeshProUGUI w;
    public TextMeshProUGUI x;
    public TextMeshProUGUI y;
    public TextMeshProUGUI z;

    /*
     * Used to switch the case of the physical buttons in the scene. If capital is true in the keyboard, switchDown() is called.
     * if it is false, switchUp is called. In both cases, the bool of the capital is set to its opposite value.
     */

    public void switchCase()
    {
        if (gameObject.GetComponent<KeyboardFunctionality>().capital)
        {
            switchDown(a);
            switchDown(b);
            switchDown(c);
            switchDown(d);
            switchDown(e);
            switchDown(f);
            switchDown(g);
            switchDown(h);
            switchDown(i);
            switchDown(j);
            switchDown(k);
            switchDown(l);
            switchDown(m);
            switchDown(n);
            switchDown(o);
            switchDown(p);
            switchDown(q);
            switchDown(r);
            switchDown(s);
            switchDown(t);
            switchDown(u);
            switchDown(v);
            switchDown(w);
            switchDown(x);
            switchDown(y);
            switchDown(z);
            gameObject.GetComponent<KeyboardFunctionality>().capital = false;
            upperCase.image.color = new Color(255, 255, 255);
        }
        else
        {
            switchUp(a);
            switchUp(b);
            switchUp(c);
            switchUp(d);
            switchUp(e);
            switchUp(f);
            switchUp(g);
            switchUp(h);
            switchUp(i);
            switchUp(j);
            switchUp(k);
            switchUp(l);
            switchUp(m);
            switchUp(n);
            switchUp(o);
            switchUp(p);
            switchUp(q);
            switchUp(r);
            switchUp(s);
            switchUp(t);
            switchUp(u);
            switchUp(v);
            switchUp(w);
            switchUp(x);
            switchUp(y);
            switchUp(z);
            gameObject.GetComponent<KeyboardFunctionality>().capital = true;
            upperCase.image.color = new Color(238, 0, 0);
        }
        
    }

    //Switches character from lowerCase to upperCase.
    void switchUp(TextMeshProUGUI letter)
    {
        string letterPrior = letter.text;
        string letterAfter = letterPrior.ToUpper();
        letter.text = letterAfter;
    }

    //Switches character from upperCase to lowerCase.
    void switchDown(TextMeshProUGUI letter)
    {
        string letterPrior = letter.text;
        string letterAfter = letterPrior.ToLower();
        letter.text = letterAfter;
    }
}
