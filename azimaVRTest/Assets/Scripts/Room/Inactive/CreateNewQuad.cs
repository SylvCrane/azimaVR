using UnityEngine;

public class CreateNewQuad : MonoBehaviour
{
    public float width = 1;
    public float height = 1;
    public Material thing;
    // Start is called before the first frame update
    public void Start()
    {
        GameObject newQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Destroy(newQuad.GetComponent<MeshCollider>());
        newQuad.transform.localScale = new Vector3(1, 1, -1);

        //Destroy(newQuad.GetComponent<MeshRenderer>());

        newQuad.GetComponent<MeshRenderer>().sharedMaterial = thing;

        //MeshRenderer meshRenderer = newQuad.AddComponent<MeshRenderer>();
        //meshRenderer.material = thing;

        //Destroy(newQuad.GetComponent<MeshFilter>());

      //  MeshFilter meshFilter = newQuad.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        //Vector3[] vertices = new Vector3[4]
        //{
        //    new Vector3((float)-0.34, (float)-8.97, (float)-17.83),
        //    new Vector3((float)3.614162, (float)-7.668871, (float)-18.08886), 
        //    new Vector3((float)-0.7160384, (float)4.434328, (float)-19.45072),
        //    new Vector3((float)3.657468, (float)3.616677, (float)-19.30861)
        //};
        Vector3[] vertices = new Vector3[4]
        {
            
            new Vector3((float)13.58, (float)7.40, (float)12.61),
            new Vector3((float)16.50, (float)8.79, (float)6.99),
            new Vector3((float)12.83, (float)-9.64, (float)11.87),
            new Vector3((float)15.01, (float)-11.53, (float)6.35)
            
            
            
            
        };
        mesh.vertices = vertices;
        // transform.localPosition = vertices[0];

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };
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


        newQuad.GetComponent<MeshFilter>().mesh = mesh;
     //   meshFilter.mesh = mesh;
    }
}
