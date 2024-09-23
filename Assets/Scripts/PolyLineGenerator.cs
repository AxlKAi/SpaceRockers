using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyLineGenerator : MonoBehaviour
{
    [SerializeField]
    private AudioPeer _audioPeer;
    [SerializeField]
    private AudioPolyline _audioLinePrefab;
    [SerializeField]
    private float _lineStep = 1f;
    [SerializeField]
    private float _newLineCreateInterval = .4f;
    [SerializeField]
    private float _renderPositionZ = 0;

    private Vector3 _currentRenderPosion = Vector3.zero;
    private bool _isGeneratorActive = true;

    private void Start()
    {
        if (_audioPeer == null)
            Debug.LogError("Can`t find AudioPeer component.");

        if (_audioLinePrefab == null)
            Debug.LogError("Can`t find AudioLine prefab.");

        StartCoroutine(CreateNewLine());
    }

    private IEnumerator CreateNewLine()
    {
        while (_isGeneratorActive)
        {
            _currentRenderPosion.z = _renderPositionZ;
            _renderPositionZ += _lineStep;

            var newLine = Instantiate(_audioLinePrefab, _currentRenderPosion, Quaternion.identity);
            newLine.SetAudioPeer(_audioPeer);
            newLine.SetPositionZ(_renderPositionZ);
            newLine.transform.parent = transform;
            yield return new WaitForSeconds(_newLineCreateInterval);
        }
    }
}
