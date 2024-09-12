using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private int _xSize = 4;
    private int _zSize = 2;
    private float _xLength = 1f;
    private float _zLength = 1f;
    private float _zLastPoint = 0f;
    private int pointsInSector = 6;

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
            //TODO delete ???
            // _xSize = _audioPeer.FrequiencyBandCountGetter;
        }

        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        CreateMesh();
        UpdateMesh();

        StartCoroutine(CreateNewLine());
    }

    private IEnumerator CreateNewLine()
    {
        for (int i = 0; i < 100; i++)
        {
            AddNewLineToMesh();
            yield return new WaitForSeconds(.5f);
        }
    }

    private void CreateMesh()
    {
        _verrticles = new Vector3[(_xSize + 1) * (_zSize + 1)];

        for (int i = 0, z = 0; z <= _zSize; z++)
        {
            for (int x = 0; x <= _xSize; x++)
            {
                _verrticles[i] = new Vector3(x * _xLength, 0, _zLastPoint + _zLength);
                i++;
            }

            _zLastPoint += _zLength;
        }

        int triangleMultiplier = 0;
        int vertexDisplacment = 0;

        _triangles = new int[_xSize * _zSize * pointsInSector];

        for (int z = 0; z < _zSize; z++)
        {
            for (int verrticleNumber = 0, x = 0; x < _xSize; x++, verrticleNumber++)
            {
                _triangles[triangleMultiplier + 0] = vertexDisplacment;
                _triangles[triangleMultiplier + 1] = vertexDisplacment + _xSize + 1;
                _triangles[triangleMultiplier + 2] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 3] = vertexDisplacment + 1;
                _triangles[triangleMultiplier + 4] = vertexDisplacment + _xSize + 1;
                _triangles[triangleMultiplier + 5] = vertexDisplacment + _xSize + 2;

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
        int oldTriangleArrayLength = _mesh.triangles.Length;

        _zSize++;
        int newVerticelsArrayLength = (_xSize + 1) * (_zSize + 1);

        if (oldVerticelsArrayLength > newVerticelsArrayLength)
        {
            Debug.Log("new line in plane must be grater than older one");
            return;
        }

        _verrticles = new Vector3[newVerticelsArrayLength];
        _triangles = new int[_xSize * _zSize * pointsInSector];

        System.Array.Copy(_mesh.vertices, _verrticles, oldVerticelsArrayLength);
        System.Array.Copy(_mesh.triangles, _triangles, oldTriangleArrayLength);

        // добавляем одну строку в конец архива
        _zLastPoint += _zLength;

        for (int i = 0; i < _xSize; i++)
        {
            _verrticles[oldVerticelsArrayLength + i] = new Vector3(i * _xLength, _audioPeer.FrequiencyBand[i] * 500, _zLastPoint); //TODO хардкод
        }

        _verrticles[oldVerticelsArrayLength + _xSize] = new Vector3(_xSize * _xLength, 0, _zLastPoint); //TODO хардкод


        int index = oldTriangleArrayLength;

        for (int x = 0; x < _xSize; x++)
        {
            _triangles[index + 0] = oldVerticelsArrayLength + x;
            _triangles[index + 1] = oldVerticelsArrayLength + x + _xSize + 1;
            _triangles[index + 2] = oldVerticelsArrayLength + x + 1;
            _triangles[index + 3] = oldVerticelsArrayLength + x + 1;
            _triangles[index + 4] = oldVerticelsArrayLength + x + _xSize + 1;
            _triangles[index + 5] = oldVerticelsArrayLength + x + _xSize + 2;

            index += pointsInSector;
        }

        UpdateMesh();
    } 

    private void UpdateMesh()
    {
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        // _mesh.Clear();

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
