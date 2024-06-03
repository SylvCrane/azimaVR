using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//The triangles associated with a portal. Each portal has four.
[System.Serializable]
public class Triangles 
{
    public string vertexA; //The first vertex, contains x, y and z
    public string vertexB; //The second vertex, contains x, y and z
    public string vertexC; //The third vertex, contains x, y and z
    public string color; //The color of the portal
}
