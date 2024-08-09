using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualizerLine : MonoBehaviour
{
    [SerializeField] private AudioPeer _audioPeer;
    [SerializeField] private float stepLength = 50;
    [SerializeField] private float amplitudeAplicator = 5000f;

    void Start()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Vector3 startPoint, oldPoint;
        startPoint = new Vector3(0, 0, 0);
        oldPoint = new Vector3(0, 0, 0);

        while(true)
        {
            foreach (float fadeLevel in _audioPeer.FrequiencyBand)
            {
                startPoint.x = startPoint.x + stepLength;
                startPoint.y = fadeLevel * amplitudeAplicator;

                Debug.DrawLine(oldPoint, startPoint, Color.yellow, 1f);

                oldPoint = startPoint;                
            }

            yield return new WaitForSeconds(.7f);
        }
    }
}
