using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    public float width = 1;
    public float height = 1;
    public Material thing;

    public void Start()
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = thing;

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

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
            new Vector3((float)-17.83, (float)-8.97, (float)-0.34),
            new Vector3((float)-18.08886, (float)-7.668871, (float)3.614162),
            new Vector3((float)-19.45072, (float)4.434328, (float)-0.7160384),
            new Vector3((float)-19.30861, (float)3.616677, (float)3.657468)
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
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
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

        meshFilter.mesh = mesh;

        
    }
}