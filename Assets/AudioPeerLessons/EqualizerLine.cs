using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualizerLine : MonoBehaviour
{
    [SerializeField] private AudioPeer _audioPeer;
    [SerializeField] private float stepLength = 50;
    [SerializeField] private float amplitudeAplicator = 5000f;
    [SerializeField] private float lineRenewDelay = .1f;
    [SerializeField] private float lineLiveDelay = 20f;

    void Start()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Vector3 startPoint, oldPoint;

        while(true)
        {
            startPoint = new Vector3(0, 0, 0);
            oldPoint = new Vector3(0, 0, 0);

            foreach (float fadeLevel in _audioPeer.FrequiencyBand)
            {
                startPoint.x = startPoint.x + stepLength;
                startPoint.y = fadeLevel * amplitudeAplicator;

                Debug.DrawLine(transform.TransformPoint(oldPoint), transform.TransformPoint(startPoint), Color.yellow, lineLiveDelay);

                oldPoint = startPoint;                
            }

            yield return new WaitForSeconds(lineRenewDelay);
        }
    }
}
