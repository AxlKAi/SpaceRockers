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
    [SerializeField]
    private SpawnByEvent _spawner1;
    [SerializeField]
    private SpawnByEvent _spawner2;
    [SerializeField]
    private SpawnByEvent _spawner3;

    void Start()
    {
        StartCoroutine(StartKoreographer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            Debug.Log("'I' distance =" + _spawner1.GetDeltaTime());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("'O' distance =" + _spawner2.GetDeltaTime());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("'P' distance =" + _spawner3.GetDeltaTime());
        }
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
