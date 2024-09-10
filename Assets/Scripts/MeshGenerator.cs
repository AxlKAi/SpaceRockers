using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    private int xSize = 512;
    private int zSize = 1;

    private Mesh _mesh;
    private Vector3[] _verrticles;
    private int[] _triangles;
    private AudioPeer _audioPeer;

    private void Start()
    {
        _audioPeer = GetComponent<AudioPeer>();
        if (_audioPeer == null)
        {
            Debug.LogError("Can`t find AudioPeer component.");
        }
        else
        {
            xSize = _audioPeer.FrequiencyBandCountGetter;
        }

        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        CreateMesh();
        UpdateMesh();

        StartCoroutine(CreateNewLine());
    }

    private void Update()
    {

    }

    private IEnumerator CreateNewLine()
    {
        for (int i = 0; i < 100; i++)
        {
            zSize++;
            CreateMesh();
            UpdateMesh();
            yield return new WaitForSeconds(.5f);
        }
    }

    private void CreateMesh()
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
            for (int verrticleNumber = 0, x = 0; x < xSize; x++, verrticleNumber++)
            {
                _triangles[triangleMultiplier + 0] = vertexDisplacment;
                _triangles[triangleMultiplier + 1] = vertexDisplacment + xSize + 1;
                _triangles[triangleMultiplier + 2] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 3] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 4] = vertexDisplacment + xSize + 1;
                _triangles[triangleMultiplier + 5] = vertexDisplacment + xSize + 2;

                _verrticles[vertexDisplacment].y = _audioPeer.FrequiencyBand[verrticleNumber] * 500; //TODO хардкод

                vertexDisplacment++;
                triangleMultiplier += pointsInSector;
            }

            vertexDisplacment++;
        }
    }

    private void AddNewLineToMesh()
    {
        int oldVerticelsArrayLength = _mesh.vertices.Length;
        int newVerticelsArrayLength = (xSize + 1) * (zSize + 1);

        if (oldVerticelsArrayLength > newVerticelsArrayLength)
        {
            Debug.Log("new line in plane must be grater than older one");
            return;
        }

        _verrticles = new Vector3[newVerticelsArrayLength];

        System.Array.Copy(_mesh.vertices, _verrticles, oldVerticelsArrayLength);

        // добавляем одну строку в конец архива
        for (int i = oldVerticelsArrayLength; i <= newVerticelsArrayLength; i++)
        {
            _verrticles[i] = new Vector3(x, 0, z);
            i++;
        }

        for (int verrticleNumber = 0, x = 0; x < xSize; x++, verrticleNumber++)
        {
            _triangles[triangleMultiplier + 0] = vertexDisplacment;
            _triangles[triangleMultiplier + 1] = vertexDisplacment + xSize + 1;
            _triangles[triangleMultiplier + 2] = vertexDisplacment + 1;
            _triangles[triangleMultiplier + 3] = vertexDisplacment + 1;
            _triangles[triangleMultiplier + 4] = vertexDisplacment + xSize + 1;
            _triangles[triangleMultiplier + 5] = vertexDisplacment + xSize + 2;

            _verrticles[vertexDisplacment].y = _audioPeer.FrequiencyBand[verrticleNumber] * 500; //TODO хардкод

            vertexDisplacment++;
            triangleMultiplier += pointsInSector;
        }
    }

    private void UpdateMesh()
    {
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

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
