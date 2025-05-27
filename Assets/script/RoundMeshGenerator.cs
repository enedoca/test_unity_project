// RoundMeshGenerator.cs
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RoundMeshGenerator : MonoBehaviour
{
    public int segments = 32;
    public float radius = 1f;

    void Start()
    {
        GenerateCircle();
    }

    void GenerateCircle()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        // ¡ﬂΩ…¡°
        vertices[0] = Vector3.zero;

        float angleStep = 360f / segments;

        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * (i - 1);
            vertices[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        }

        for (int i = 0; i < segments; i++)
        {
            int start = i + 1;
            int next = (i + 1) % segments + 1;

            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = next;
            triangles[i * 3 + 2] = start;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
