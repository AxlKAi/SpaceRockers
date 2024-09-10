using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    private int xSize = 17;
    private int zSize = 25;

    private Mesh _mesh;
    private Vector3[] _verrticles;
    private int[] _triangles;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        StartCoroutine(CreateMesh());
        UpdateMesh();
    }

    private void Update()
    {

    }

    private IEnumerator CreateMesh()
    {
        _verrticles = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                _verrticles[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int triangleMultiplier = 0;
        int vertexDisplacment = 0;
        int pointsInSector = 6;

        _triangles = new int[xSize * zSize * pointsInSector];

        for (int z = 0; z < zSize; z++)
        {

            for (int x = 0; x < xSize; x++)
            {
                _triangles[triangleMultiplier + 0] = vertexDisplacment;
                _triangles[triangleMultiplier + 1] = vertexDisplacment + xSize + 1;
                _triangles[triangleMultiplier + 2] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 3] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 4] = vertexDisplacment + xSize + 1;
                _triangles[triangleMultiplier + 5] = vertexDisplacment + xSize + 2;

                vertexDisplacment++;
                triangleMultiplier += pointsInSector;
            }
            vertexDisplacment++;
        }


        yield return new WaitForSeconds(1f);
    }

    private void UpdateMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _verrticles;
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (_verrticles != null)
            foreach (Vector3 point in _verrticles)
                Gizmos.DrawSphere(point, .1f);
    }
}
