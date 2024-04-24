using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal 
{
    public Vector3 bottomLeft;
    public Vector3 bottomRight;
    public Vector3 topLeft;
    public Vector3 topRight;
    public Material color;
    public string destination;
    public GameObject portal;

    public void assignVertices(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
    {
        this.bottomLeft = bottomLeft;
        this.bottomRight = bottomRight;
        this.topLeft = topLeft;
        this.topRight = topRight;
    }

    public Vector3 splitVertex(string vertex)
    {
        string xString = "";
        string yString = "";
        string zString = "";

        bool firstInterval = false;
        bool secondInterval = false;
        bool lastInterval = false;

        int i = 0;

        while (i < vertex.Length)
        {
            if (!firstInterval)
            {
                if (vertex[i] != ' ')
                {
                    xString += vertex[i];
                }
                else
                {
                    firstInterval = true;
                }
            }
            else if (!secondInterval)
            {
                if (vertex[i] != ' ')
                {
                    yString += vertex[i];
                }
                else
                {
                    secondInterval = true;
                }
            }
            else if (!lastInterval)
            {
                if (vertex[i] != ' ')
                {
                    zString += vertex[i];
                }
            }

            i++;
        }

        float xFloat = float.Parse(xString);
        float yFloat = float.Parse(yString);
        float zFloat = float.Parse(zString);

        return new Vector3(xFloat, yFloat, zFloat);
    }

    public void GenerateText(Vector3 textLocation, double rotationX, double rotationY, double rotationZ)
    {
        GameObject destinationText = new GameObject();
        destinationText.name = destination;
        destinationText.AddComponent<TextMeshPro>();
        destinationText.GetComponent<TextMeshPro>().text = destination;
        destinationText.GetComponent<TextMeshPro>().fontSize = 10;
        destinationText.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
        textLocation.z = -textLocation.z;
        destinationText.transform.position = textLocation;

        float rotateFloatX = (float)rotationX;
        float rotateFloatY = (float)rotationY;
        float rotatFloatZ = (float)rotationZ;

        destinationText.transform.rotation = Quaternion.Euler(rotateFloatX, rotateFloatY, rotatFloatZ);
        destinationText.transform.SetParent(portal.transform);

    }

    public void setParentOfPortal(GameObject portalStorageContainer)
    {
        portal.transform.SetParent(portalStorageContainer.transform);
    }

    public void GeneratePortal(string location, string color)
    {
        //Create portal and assign 
        GameObject newPortal = GameObject.CreatePrimitive(PrimitiveType.Quad);
        newPortal.name = location;
        newPortal.transform.localScale = new Vector3(1, 1, -1);
        newPortal.AddComponent<PortalHold>();
        newPortal.GetComponent<PortalHold>().destination = destination;

        string portalName = "PortalMaterials/" + color;
        string portalOpaqueName = "PortalMaterials/" + color + "Opaque";

        newPortal.GetComponent<PortalHold>().portalColor = Resources.Load<Material>(portalName);
        newPortal.GetComponent<PortalHold>().portalOpaque = Resources.Load<Material>(portalOpaqueName);
        newPortal.GetComponent<PortalHold>().location = location;
        newPortal.GetComponent<MeshRenderer>().sharedMaterial = newPortal.GetComponent<PortalHold>().portalColor;

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            bottomRight,
            bottomLeft,
            topLeft,
            topRight
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            //lower left
            0,2,1,
            //upper right
            2,3,1
        };

        mesh.triangles = tris;

        //calculating the normal for when the portal spawns in a 360 view
        float zAverage = (bottomLeft.z+bottomRight.z+topLeft.z+topRight.z)/4;

        Debug.Log(zAverage);


        bool CameraFront = 0 > zAverage;

        Vector3[] normals = new Vector3[4];

        for (int i = 0; i < 4; i++)
        {
            normals[i] = CameraFront ? Vector3.forward : -Vector3.forward;
        }
        
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        Vector3[] reverseVertices = new Vector3[vertices.Length * 2];
        int[] reverseTriangles = new int[tris.Length * 2];

        vertices.CopyTo(reverseVertices, 0);
        tris.CopyTo(reverseTriangles, 0);

        vertices.CopyTo(reverseVertices, 4);

        for (int i = 0; i < 6; i += 3)
        {
            int index = vertices.Length;
            reverseTriangles[tris.Length + i] = index + tris[i];
            reverseTriangles[tris.Length + i + 1] = index + tris[i + 2];
            reverseTriangles[tris.Length + i + 2] = index + tris[i + 1];
        }

        mesh.vertices = reverseVertices;
        mesh.triangles = reverseTriangles;
        mesh.RecalculateNormals();


        newPortal.GetComponent<MeshFilter>().mesh = mesh;
        newPortal.GetComponent<MeshCollider>().sharedMesh = mesh;

        portal = newPortal;
    }
}
