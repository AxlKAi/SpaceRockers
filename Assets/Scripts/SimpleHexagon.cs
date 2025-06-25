using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHexagon : MonoBehaviour
{
    [SerializeField]
    private float MaxGlowLevel = 2f;

    [SerializeField]
    private float MinGlowLevel = 0.5f;

    [SerializeField]
    private float GlowLevelChangePeriod = 1f;

    [SerializeField]
    private GameObject _upperLinesObject;

    [SerializeField]
    private GameObject _lowerLinesObject;

    private float GlowLevel;
    private float GlowLevelTimer;

    private Material _upperLinesMaterial = null;
    private Material _lowerLinesMaterial = null;
    private Color _baseColorOfUpperLines;
    private Color _baseColorOfLowerLines;

    // Start is called before the first frame update
    void Start()
    {
        GlowLevel = MaxGlowLevel;
        GlowLevelTimer = GlowLevelChangePeriod;

        if (_upperLinesObject != null)
        {
            if (_upperLinesObject.transform.TryGetComponent(out Renderer rend))
            {
                _upperLinesMaterial = rend.material;
                _baseColorOfUpperLines = _upperLinesMaterial.GetColor("_EmissionColor");
            }
        }

        if (_lowerLinesObject != null)
        {
            if (_lowerLinesObject.transform.TryGetComponent(out Renderer rend))
            {
                _lowerLinesMaterial = rend.material;
                _baseColorOfLowerLines = _lowerLinesMaterial.GetColor("_EmissionColor");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GlowLevelTimer -= Time.deltaTime;

        float glowIntensivity = Mathf.Lerp(MinGlowLevel, MaxGlowLevel, GlowLevelTimer / GlowLevelChangePeriod);

        Color newUpperColor = new Color(
            _baseColorOfUpperLines.r * glowIntensivity,
            _baseColorOfUpperLines.g * glowIntensivity,
            _baseColorOfUpperLines.b * glowIntensivity
         );

        Color newLowerColor = new Color(
            _baseColorOfLowerLines.r * glowIntensivity,
            _baseColorOfLowerLines.g * glowIntensivity,
            _baseColorOfLowerLines.b * glowIntensivity
        );

        Debug.Log(newUpperColor);

        if (_upperLinesMaterial != null)
            _upperLinesMaterial.SetColor("_EmissionColor", newUpperColor);

        if (_lowerLinesMaterial != null)
            _lowerLinesMaterial.SetColor("_EmissionColor", newLowerColor);

        if (GlowLevelTimer <= 0)
        {
            SetNewGlowLevel();
        }
    }

    private void SetNewGlowLevel()
    {
        GlowLevel = Random.Range(MinGlowLevel, MaxGlowLevel);
        Debug.Log(GlowLevel);
        GlowLevelTimer = GlowLevelChangePeriod;
    }
}
