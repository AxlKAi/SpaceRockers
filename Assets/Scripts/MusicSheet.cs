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

    [SerializeField]
    private float _poorNoteDistance = 10f;
    [SerializeField]
    private float _severalNoteDistance = 5f;
    [SerializeField]
    private float _goodDistance = 1f;

    [SerializeField]
    private int _poorReward = 1;
    [SerializeField]
    private int _severalReward = 2;
    [SerializeField]
    private int _goodReward = 3;

    private float _startPlayDelay = 2f;

    public float PoorNoteDistance { get => _poorNoteDistance; }

    private void Start()
    {
        StartCoroutine(StartKoreographer());

        _spawnerLeft.Initialize(_player, this, _pathCurve);
        _spawnerRight.Initialize(_player, this, _pathCurve);
        _spawnerMiddle.Initialize(_player, this, _pathCurve);

        _player.PlayerInput.CatchLeftPressed += CatchLeftNote;
        _player.PlayerInput.CatchRightPressed += CatchRightNote;
        _player.PlayerInput.CatchMiddlePressed += CatchMiddleNote;
    }

    private void Update()
    {
        transform.position = _pathCurve.GetWayLinePoint(_player.transform.position.z);
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

    private void CatchRightNote()
    {
        int reward = GetPlayerRewarPoints(_spawnerRight.GetNearNoteDistance());

        if (reward > 0)
            _player.PlayerReward(reward);
    }

    private void CatchLeftNote()
    {
        int reward = GetPlayerRewarPoints(_spawnerLeft.GetNearNoteDistance());

        if (reward > 0)
            _player.PlayerReward(reward);
    }

    private void CatchMiddleNote()
    {
        int reward = GetPlayerRewarPoints(_spawnerMiddle.GetNearNoteDistance());

        if (reward > 0)
            _player.PlayerReward(reward);
    }

    private int GetPlayerRewarPoints(float distance)
    {
        int reward = 0;

        if (Mathf.Abs(distance) < _goodDistance)
        {
            reward = _goodReward;
        }
        else if (distance < _severalNoteDistance)
        {
            reward = _severalReward;
        }
        else if (distance < _poorReward)
        {
            reward = _poorReward;
        }

        return reward;
    }
}
