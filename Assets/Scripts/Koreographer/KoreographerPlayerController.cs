using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private Vector3 _pointsAchivedPositionDisplacement;

    [SerializeField]
    private PointsAchived _pointsAchivedPrefab;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bpmText;

    [SerializeField]
    private float _lowPointPeriod = .5f;
    [SerializeField]
    private float _middlePointPeriod = .2f;
    [SerializeField]
    private float _hightPointPeriod = .1f;
    [SerializeField]
    private float _lowPointReward = 1f;
    [SerializeField]
    private float _middlePointReward = 2f;
    [SerializeField]
    private float _higthPointReward = 3f;

    private int _totalPoints = 0;
    private int _totalNotesCatched = 0;

    void Start()
    {
        StartCoroutine(StartKoreographer());

        _spawner1.SetMaxDistance(_hightPointPeriod);
        _spawner2.SetMaxDistance(_hightPointPeriod);
        _spawner3.SetMaxDistance(_hightPointPeriod);
    }

    private void Update()
    {
        int points = 0;

        if (Input.GetKeyDown(KeyCode.I))
        {
            points = GetPoints(_spawner1.GetDeltaTime());
            _spawner1.CatchNote();
            Debug.Log("'I' distance =" + points);
            SpawnPoints(_spawner1, points);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            points = GetPoints(_spawner2.GetDeltaTime());
            _spawner2.CatchNote();
            Debug.Log("'O' distance =" + points);
            SpawnPoints(_spawner2, points);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            points = GetPoints(_spawner3.GetDeltaTime());
            _spawner3.CatchNote();
            Debug.Log("'P' distance =" + points);
            SpawnPoints(_spawner3, points);
        }

        if (points > 0)
        {
            ++_totalNotesCatched;

            _bpmText.text = ( _totalNotesCatched / (Time.realtimeSinceStartup / 60f)).ToString("F2"); 
        }

        _totalPoints += points;
        _scoreText.text = _totalPoints.ToString();
    }

    private int GetPoints(float timer)
    {
        int points = 0;

        if (_pointsAchivedPrefab != null)
        {
            if(timer <= _hightPointPeriod)
            {
                points = (int)_higthPointReward;
            }
            else if(timer <= _middlePointPeriod)
            {
                points = (int)_middlePointReward;
            }
            else if (timer <= _lowPointPeriod)
            {
                points = (int)_lowPointReward;
            }
        }

        return points;
    }

    private void SpawnPoints(SpawnByEvent spawner, int points)
    {
        if (points > _higthPointReward)
            points = (int)_higthPointReward;

        if (points < 0)
            points = 0;

        string value = "+" + points.ToString();

        var obj = Instantiate(_pointsAchivedPrefab, spawner.PointsToArrivedPosition + _pointsAchivedPositionDisplacement, Quaternion.identity);
        obj.SetValue(value);
    }

    private IEnumerator StartKoreographer()
    {
        yield return new WaitForSeconds(_startDelay);

        if (_koreographyAsset != null && _musicPlayer != null)
        {
            Koreographer.Instance.LoadKoreography(_koreographyAsset);

            //TODO LoadSong только для старта с определённой точки трека
            _musicPlayer.LoadSong(_koreographyAsset, 999990, false);
            _musicPlayer.Play();
        }
        else
        {
            Debug.LogError("Koreography or SimpleMusicPlayer not assigned!");
        }
    }
}
