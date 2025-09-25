using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using UnityEngine.Pool;

public class NoteSpawner : MonoBehaviour
{
    //TODO добавить рандом появления

    [EventID]
    public string eventID;

    [SerializeField]
    private Note _spawnebleActorPrefab;
    [SerializeField]
    private GameObject _pointToArrived;
    [SerializeField]
    float randomX = 1.5f;
    [SerializeField]
    float randomY = 1.5f;

    private TunelPathCurve _pathCurve;
    private Player _player;

    private ObjectPool<Note> _notes;

    public TunelPathCurve PathCurve
    {
        get => _pathCurve;
        set => _pathCurve = value;
    }
    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    private void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnEventAction);
    }

    private void Start()
    {
        _notes = new ObjectPool<Note>(
            createFunc: CreateNote,
            actionOnGet: (obj) => obj.transform.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.transform.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 45,
            maxSize: 50);
    }

    private Note CreateNote()
    {
        var note = Instantiate(_spawnebleActorPrefab, transform.position, Quaternion.identity, transform);

        note.Player = _player;
        note.Spawner = this as NoteSpawner;
        note.PathCurve = _pathCurve;

        return note;    
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
        Vector3 displacement = new Vector3(transform.localPosition.x+ displacementDeltaX, transform.localPosition.y+ displacementDeltaY, 0);
        newObject.Displacement = displacement;

        newObject.RemoveNote += RemoveNote;
    }

    private void RemoveNote(Note note)
    {
        _notes.Release(note);
    }




}
