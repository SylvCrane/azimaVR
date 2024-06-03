using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Used to save portal data
[System.Serializable]
public class Portals 
{
    public string destination; //The room the portal is going to
    public string location; //The room the portal is located in
    public Triangles[] triangles; //The triangles that make up a portal
    public textData textData; //The text associated with a portal (The destination the portal is going) and its rotation.
}
