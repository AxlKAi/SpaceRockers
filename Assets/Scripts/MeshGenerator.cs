using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private GroundMesh _groundMesh;

    private int _xSize = 150;
    private int _zSize = 1;
    private float _xLength = 10f;
    private float _zLength = 12f;
    private float _zLastPoint = 0f;
    private int _pointsInSector = 6;
    private float _amplitudeAmpfiller = 4500f;

    private int _groundMeshMaxLength = 20;

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

        CreateMesh();

        StartCoroutine(CreateNewLine());
    }

    private IEnumerator CreateNewLine()
    {
        for (int i = 0; i < 10000; i++)
        {
            AddNewLineToMesh();
            yield return new WaitForSeconds(.05f);
        }
    }

    private void CreateMesh()
    {
        var mesh = Instantiate(_groundMesh, Vector3.zero, Quaternion.identity);
        mesh.transform.parent = transform;
        mesh.transform.position = Vector3.zero;

        _mesh = new Mesh();
        mesh.GetComponent<MeshFilter>().mesh = _mesh;

        _verrticles = new Vector3[(_xSize + 1) * (_zSize + 1)];

        for (int i = 0, z = 0; z <= _zSize; z++)
        {
            for (int x = 0; x <= _xSize; x++)
            {
                _verrticles[i] = new Vector3(x * _xLength, 0, _zLastPoint);
                i++;
            }

            _zLastPoint += _zLength;
        }

        int triangleMultiplier = 0;
        int vertexDisplacment = 0;

        _triangles = new int[_xSize * _zSize * _pointsInSector];

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

                // _verrticles[vertexDisplacment].y = _audioPeer.FrequiencyBand[verrticleNumber] * _amplitudeAmpfiller; //TODO хардкод
                _verrticles[vertexDisplacment].y = _audioPeer.Samples[verrticleNumber] * _amplitudeAmpfiller;

                vertexDisplacment++;
                triangleMultiplier += _pointsInSector;
            }

            vertexDisplacment++;
        }

        UpdateMesh();
    }

    private void AddNewLineToMesh()
    {
        if (_zSize > _groundMeshMaxLength)
        {
            _zSize = 1;
            _zLastPoint -= _zLength;
            CreateMesh();
            UpdateMesh();
            return;
        }

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
        _triangles = new int[_xSize * _zSize * _pointsInSector];

        System.Array.Copy(_mesh.vertices, _verrticles, oldVerticelsArrayLength);
        System.Array.Copy(_mesh.triangles, _triangles, oldTriangleArrayLength);

        // добавляем одну строку в конец архива

        _verrticles[oldVerticelsArrayLength] = new Vector3(0, 0, _zLastPoint); //TODO хардкод
        _verrticles[oldVerticelsArrayLength + _xSize] = new Vector3(_xSize * _xLength, 0, _zLastPoint); //TODO хардкод

        for (int i = 1; i < _xSize; i++)
        {
            //_verrticles[oldVerticelsArrayLength + i] = new Vector3(i * _xLength, _audioPeer.FrequiencyBand[i] * _amplitudeAmpfiller, _zLastPoint); //TODO хардкод
            _verrticles[oldVerticelsArrayLength + i] = new Vector3(i * _xLength, _audioPeer.Samples[i] * _amplitudeAmpfiller, _zLastPoint); //TODO хардкод
        }

        _zLastPoint += _zLength;

        int trianlgesIndex = oldTriangleArrayLength;
        int vertexIndex = oldVerticelsArrayLength - _xSize - 1;

        for (int x = 0; x < _xSize; x++)
        {
            _triangles[trianlgesIndex + 0] = vertexIndex + x;
            _triangles[trianlgesIndex + 1] = vertexIndex + x + _xSize + 1;
            _triangles[trianlgesIndex + 2] = vertexIndex + x + 1;
            _triangles[trianlgesIndex + 3] = vertexIndex + x + 1;
            _triangles[trianlgesIndex + 4] = vertexIndex + x + _xSize + 1;
            _triangles[trianlgesIndex + 5] = vertexIndex + x + _xSize + 2;

            trianlgesIndex += _pointsInSector;
        }

        UpdateMesh();
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
        /*
        if (_verrticles != null)
            foreach (Vector3 point in _verrticles)
                Gizmos.DrawSphere(point, .1f);
        */
    }
}
