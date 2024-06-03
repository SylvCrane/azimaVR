using UnityEngine;
using TMPro;
using Oculus.Interaction.Surfaces;
using Oculus.Interaction;

//Used to generate the portal
public class Portal 
{
    public Vector3 bottomLeft; //The bottom left vertex
    public Vector3 bottomRight; //The bottom right vertex
    public Vector3 topLeft; //The top left vertex
    public Vector3 topRight; //The top right vertex
    public Material color; //The color of the portal
    public string destination; //Where the portal is going to
    public GameObject portal; //The GameObject portal this portal is attached to

    /*
     * Assigns the vertices to the portal variables
     * 
     * params)
     * - bottomLeft) The bottom left vertex
     * - bottomRight) The bottom right vertex
     * - topLeft) The top left vertex
     * - TopRight) THe top right vertex
     */
    public void assignVertices(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
    {
        this.bottomLeft = bottomLeft;
        this.bottomRight = bottomRight;
        this.topLeft = topLeft;
        this.topRight = topRight;
    }

    /*
     * Splits the vertex up into its three location varables and appends to a Vector3.
     * 
     * Returns the vertor3.
     * 
     * params)
     * - vertex) The vertex we will be referencing
     */
    public Vector3 splitVertex(string vertex)
    {
        //Declare empty strings for appending
        string xString = "";
        string yString = "";
        string zString = "";

        //Intervals are set to move through string
        bool firstInterval = false;
        bool secondInterval = false;
        bool lastInterval = false;

        int i = 0;

        //While the i is less than the vertex length, move through the vertex and append the values to the three location strings.
        //When all of the first location has been set, start appending to the next one.
        while (i < vertex.Length)
        {
            if (!firstInterval)
            {
                //If first space is encountered, move on to appending to yString
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
                //If first space is encountered, move on to appending to zString
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

        //Parse as floats, and return a new vector.
        float xFloat = float.Parse(xString);
        float yFloat = float.Parse(yString);
        float zFloat = float.Parse(zString);

        return new Vector3(xFloat, yFloat, zFloat);
    }

    /*
     * Generates the text of the portal in the scene.
     * 
     * Params)
     * - textLocation) The location of the text
     * - rotationX) the x-rotation of the text
     * - rotationY) The y-rotation of the text
     * - rotationZ) The z-rotation of the text
     */
    public void GenerateText(Vector3 textLocation, double rotationX, double rotationY, double rotationZ)
    {
        //Set new GameObject to represent text, and change name to the destination.
        GameObject destinationText = new GameObject();
        destinationText.name = destination;

        //At TMP component, append destination to it.
        destinationText.AddComponent<TextMeshPro>();
        destinationText.GetComponent<TextMeshPro>().text = destination;
        destinationText.GetComponent<TextMeshPro>().fontSize = 10;
        destinationText.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;

        //Change to negative z to display on the same location of the portal.
        textLocation.z = -textLocation.z;

        destinationText.transform.position = textLocation;

        float rotateFloatX = (float)rotationX;
        float rotateFloatY = (float)rotationY;
        float rotatFloatZ = (float)rotationZ;

        //Set rotation of text and set this portal as the parent.
        destinationText.transform.rotation = Quaternion.Euler(rotateFloatX, rotateFloatY, rotatFloatZ);
        destinationText.transform.SetParent(portal.transform);

    }

    /*
     * Set the parent of the portal as the portalContainer(In this instance, it is called the portalStorageContainer)
     * 
     * Params)
     * - portalStorageContainer) The portalContainer
     */
    public void setParentOfPortal(GameObject portalStorageContainer)
    {
        portal.transform.SetParent(portalStorageContainer.transform);
    }

    /*
     * Generates the portal in the scene by creating a mesh and quad and applying this to the 
     * portal GameObject.
     * 
     * Params)
     * - location) Where the portal is located
     * - color) The color of the portal.
     */
    public void GeneratePortal(string location, string color)
    {
        //Set name of portal as the location
        portal.name = location;

        //Invert portal on Z axis to display correctly
        portal.transform.localScale = new Vector3(1, 1, -1);

        portal.GetComponent<PortalHold>().destination = destination;

        //Setting the string to get the materials for the portal's color from the Resources folder.
        string portalName = "PortalMaterials/" + color;
        string portalOpaqueName = "PortalMaterials/" + color + "Opaque";

        //Load materials for the portal
        portal.GetComponent<PortalHold>().portalColor = Resources.Load<Material>(portalName);
        portal.GetComponent<PortalHold>().portalOpaque = Resources.Load<Material>(portalOpaqueName);


        portal.GetComponent<PortalHold>().location = location;

        //Set mesh's portal color as the default portal color.
        portal.GetComponent<MeshRenderer>().sharedMaterial = portal.GetComponent<PortalHold>().portalColor;


        Mesh mesh = new Mesh();

        //Vertices for the mesh, based on the vertices created previously.
        Vector3[] vertices = new Vector3[4]
        {
            bottomRight,
            bottomLeft,
            topLeft,
            topRight
        };
        mesh.vertices = vertices;

        //Tris for the mesh, based on the recommended tris from the Unity documentation for quads
        int[] tris = new int[6]
        {
            //lower left
            0,2,1,
            //upper right
            2,3,1
        };
        mesh.triangles = tris;

        //Set the uv of the quad
        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        //Create a portal on the opposite side of the mesh
        Vector3[] reverseVertices = new Vector3[vertices.Length * 2];
        int[] reverseTriangles = new int[tris.Length * 2];

        vertices.CopyTo(reverseVertices, 0);
        tris.CopyTo(reverseTriangles, 0);

        vertices.CopyTo(reverseVertices, 4);

        //Append remaining values in reverse variables as manipulated versions of original mesh variables
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


        portal.GetComponent<MeshFilter>().mesh = mesh;
        portal.GetComponent<MeshCollider>().sharedMesh = mesh;

        //Add RayCast functionality
        portal.GetComponent<RayInteractable>().enabled = true;

        //Add collider surface to the portal
        portal.GetComponent<ColliderSurface>().InjectCollider(portal.GetComponent<MeshCollider>());
        portal.GetComponent<RayInteractable>().InjectSurface(portal.GetComponent<ColliderSurface>());
    }
}
