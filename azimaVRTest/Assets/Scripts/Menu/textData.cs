using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//The text associated with a portal and its rotation. 
[System.Serializable]
public class textData
{
    public string value; //The text itself, usually the portal's destination
    public string position; //The location of the text
    public Rotation rotation; //The text's rotation
}
