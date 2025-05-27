// CircleToForkMorpher.cs
using UnityEngine;

public class CircleToForkMorpher : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] targetVertices;
    private int segments;
    private int frameCount = 8;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        targetVertices = new Vector3[originalVertices.Length];
        segments = originalVertices.Length - 1;

        GenerateForkShape(); // ��ũ Ÿ�� ��ǥ ����
        StartCoroutine(Morph());
    }

    void GenerateForkShape()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 v = originalVertices[i];
            Vector3 t = v;

            // scale 0.2�� ����� ��ũ�� ����
            if (i % segments >= segments * 3 / 8 && i % segments <= segments * 5 / 8)
                t.y += 0.1f; // ��� ��
            else if (i % segments < segments / 4)
                t.x -= 0.04f; // ���� ��
            else if (i % segments > segments * 3 / 4)
                t.x += 0.04f; // ������ ��

            targetVertices[i] = t;
        }
    }

    System.Collections.IEnumerator Morph()
    {
        for (int frame = 1; frame <= frameCount; frame++)
        {
            Vector3[] newVerts = new Vector3[originalVertices.Length];
            float t = frame / (float)frameCount;

            for (int i = 0; i < originalVertices.Length; i++)
                newVerts[i] = Vector3.Lerp(originalVertices[i], targetVertices[i], t);

            mesh.vertices = newVerts;
            mesh.RecalculateNormals();
            yield return new WaitForEndOfFrame(); // ������ ������ ����
        }
    }
}
