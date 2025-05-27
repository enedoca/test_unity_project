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

        GenerateForkShape(); // 포크 타겟 좌표 생성
        StartCoroutine(Morph());
    }

    void GenerateForkShape()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 v = originalVertices[i];
            Vector3 t = v;

            // scale 0.2를 고려한 포크형 변형
            if (i % segments >= segments * 3 / 8 && i % segments <= segments * 5 / 8)
                t.y += 0.1f; // 가운데 뿔
            else if (i % segments < segments / 4)
                t.x -= 0.04f; // 왼쪽 뿔
            else if (i % segments > segments * 3 / 4)
                t.x += 0.04f; // 오른쪽 뿔

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
            yield return new WaitForEndOfFrame(); // 프레임 단위로 실행
        }
    }
}
