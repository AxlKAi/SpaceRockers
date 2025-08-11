using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;
using System.Collections;

public class KoreographerPlayerController : MonoBehaviour
{
    [SerializeField]
    private Koreography _koreographyAsset;
    [SerializeField]
    private SimpleMusicPlayer _musicPlayer;
    [SerializeField]
    private float _startDelay = 2f;

    void Start()
    {
        StartCoroutine(StartKoreographer());
    }

    private IEnumerator StartKoreographer()
    {
        yield return new WaitForSeconds(_startDelay);

        if (_koreographyAsset != null && _musicPlayer != null)
        {
            Koreographer.Instance.LoadKoreography(_koreographyAsset);
            _musicPlayer.Play();
        }
        else
        {
            Debug.LogError("Koreography or SimpleMusicPlayer not assigned!");
        }
    }
}
