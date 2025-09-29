using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using UnityEngine.Pool;

public class NoteSpawner : MonoBehaviour
{
    [EventID]
    public string eventID;

    [SerializeField]
    private Note _spawnebleActorPrefab;
    [SerializeField]
    private GameObject _pointToArrived;
    [SerializeField]
    private GameObject _destroyPoint;
    [SerializeField]
    float randomX = 1.5f;
    [SerializeField]
    float randomY = 1.5f;

    private TunelPathCurve _pathCurve;
    private Player _player;
    private MusicSheet _musicSheet;

    private int _defaultNotesCapacity = 15;
    private int _maxNotesCapacity = 15;
    private ObjectPool<Note> _notes;

    public TunelPathCurve PathCurve
    {
        get => _pathCurve;
    }

    public Player Player
    {
        get => _player;
    }

    public MusicSheet MusicSheet
    {
        get => _musicSheet;
    }

    public void Initialize(Player player, MusicSheet musicSheet, TunelPathCurve curve)
    {
        _player = player;
        _musicSheet = musicSheet;
        _pathCurve = curve;
    }

    public float CatchNearNote()
    {
        float distance = _musicSheet.PoorNoteDistance;

        foreach (Transform child in transform)
        {
            Note note;

            if (child.TryGetComponent<Note>(out note))
            {
                if (child.gameObject.activeSelf)
                {
                    float delta = child.position.z - _musicSheet.transform.position.z;

                    if (delta < distance)
                    {
                        distance = delta;
                    }
                }
            }

            if (note != null && distance < _musicSheet.PoorNoteDistance)
            {
                note.Caught();

                Debug.Log($"Left note catched at time {Time.realtimeSinceStartup}");
            }
        }

        return distance;
    }

    private void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnEventAction);
    }

    private void Start()
    {
        _notes = new ObjectPool<Note>(
            createFunc: CreateNote,
            actionOnGet: GetNote,
            actionOnRelease: (obj) => obj.transform.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: _defaultNotesCapacity,
            maxSize: _maxNotesCapacity);
    }

    private Note CreateNote()
    {
        var note = Instantiate(_spawnebleActorPrefab, transform.position, Quaternion.identity, transform);

        note.Player = _player;
        note.Spawner = this as NoteSpawner;
        note.PathCurve = _pathCurve;
        note.RemoveNote += RemoveNote;
        note.EndPosition = _destroyPoint;

        return note;    
    }

    private void GetNote(Note obj)
    {
        obj.gameObject.SetActive(true);
        obj.transform.localScale = Vector3.one;
        obj.ResetTimers();
    }

    private void OnEventAction(KoreographyEvent evt)
    {
        if (_spawnebleActorPrefab == null || _pointToArrived == null)
            return;

        var newObject = _notes.Get();

        newObject.gameObject.SetActive(true);
        
        newObject.StartPosition = transform.position;
        float displacementDeltaX = Random.Range(-randomX, +randomX);
        float displacementDeltaY = Random.Range(-randomY, +randomY);
        Vector3 displacement = new(transform.localPosition.x+ displacementDeltaX, transform.localPosition.y+ displacementDeltaY, 0);
        newObject.Displacement = displacement;
    }

    private void RemoveNote(Note note)
    {
        _notes.Release(note);
    }
}
