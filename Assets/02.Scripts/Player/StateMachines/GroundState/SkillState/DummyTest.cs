using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(LineRenderer))]
public class DummyTest : MonoBehaviour
{
    public float radius = 5f;
    public float angle = 90f;
    public int segments = 20;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private LineRenderer lineRenderer;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        CreateFanMesh();
        CreateFanOutline();
    }

    void CreateFanOutline()
    {
        lineRenderer.positionCount = segments + 2;

        float angleIncrement = angle / segments;
        float currentAngle = -angle / 2f;

        for (int i = 0; i <= segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;

            Vector3 point = new Vector3(x, 0f, z);
            lineRenderer.SetPosition(i, transform.TransformPoint(point));

            currentAngle += angleIncrement;
        }

        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Standard"));
    }
    void CreateFanMesh()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        float angleIncrement = angle / segments;
        float currentAngle = -angle / 2f;

        for (int i = 1; i <= segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;

            vertices[i] = new Vector3(x, 0f, z);

            if (i > 1)
            {
                int triIndex = (i - 2) * 3;
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = i - 1;
                triangles[triIndex + 2] = i;
            }

            currentAngle += angleIncrement;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshRenderer.material = new Material(Shader.Find("Standard"));
    }
}
