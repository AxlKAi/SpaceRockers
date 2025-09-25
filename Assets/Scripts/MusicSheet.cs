using System.Collections;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    // TODO этот класс подписывается на even-ы игрока
    // нот он в себе не хранит, они хранятся в спавнерах

    [SerializeField]
    private Player _player;
    [SerializeField]
    private Koreography _koreographyAsset;
    [SerializeField]
    private SimpleMusicPlayer _musicPlayer;

    [SerializeField]
    private TunelPathCurve _pathCurve;

    [SerializeField]
    private NoteSpawner _spawnerLeft;
    [SerializeField]
    private NoteSpawner _spawnerRight;
    [SerializeField]
    private NoteSpawner _spawnerMiddle;

    private float _startPlayDelay = 2f;


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StartKoreographer());

        _spawnerLeft.PathCurve = _pathCurve;
        _spawnerRight.PathCurve = _pathCurve;
        _spawnerMiddle.PathCurve = _pathCurve;

        _spawnerLeft.Player = _player;
        _spawnerRight.Player = _player;
        _spawnerMiddle.Player = _player;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _player.transform.position.z;
        transform.position = newPosition;


    }

    private IEnumerator StartKoreographer()
    {
        yield return new WaitForSeconds(_startPlayDelay);

        if (_koreographyAsset != null && _musicPlayer != null)
        {
            Koreographer.Instance.LoadKoreography(_koreographyAsset);

            //TODO LoadSong только для старта с определённой точки трека
            // _musicPlayer.LoadSong(_koreographyAsset, 999990, false);
            _musicPlayer.Play();
        }
        else
        {
            Debug.LogError("Koreography or SimpleMusicPlayer not assigned!");
        }
    }
}
