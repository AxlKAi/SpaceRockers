using UnityEngine;
using SonicBloom.Koreo;
using System.Collections.Generic;

public class SpawnByEvent : MonoBehaviour
{
    [SerializeField]
    private NoteObject spawnebleActorPrefab;

    [SerializeField]
    private GameObject pointToArrived;

    [SerializeField]
    private float _unreachebleTimer = 1f;

    [EventID]
    public string eventID;

    private List<NoteObject> _notes = new List<NoteObject>();

    public Vector3 PointsToArrivedPosition 
    { 
        get { return pointToArrived.transform.position; } 
    }

    public float GetDeltaTime()
    {
        float distance = _unreachebleTimer;

        foreach (var note in _notes)
        {
            var noteDistance = note.GetRemainingTime();

            if (noteDistance < distance)
                distance = noteDistance;
        }

        return distance;
    }

    public NoteObject GetGetNearNote()
    {
        NoteObject catchedNote = null;

        float distance = _unreachebleTimer;

        foreach (var note in _notes)
        {
            var noteDistance = note.GetRemainingTime();

            if (noteDistance < distance)
            {
                distance = noteDistance;
                catchedNote = note;
            }
        }

        return catchedNote;
    }


    public void CatchNote()
    {
        var note = GetGetNearNote();

        if (_notes.Contains(note))
        {
            _notes.Remove(note);
            note.Catched();
        }
    }

    public void DestroyNote(NoteObject note, GameObject obj)
    {
        if (_notes.Contains(note))
        {
            _notes.Remove(note);
            Destroy(obj);
        }
    } 

    public void SetMaxDistance(float time)
    {
        _unreachebleTimer = time;
    }

    private void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnEventAction);
    }

    // Start is called before the first frame update
    private void Start()
    {
           
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnEventAction(KoreographyEvent evt)
    {
        if (spawnebleActorPrefab == null || pointToArrived == null)
            return;

        var newObject = Instantiate(spawnebleActorPrefab, transform.position, Quaternion.identity);
        newObject.StartPosition = transform.position;
        newObject.EndPosition = pointToArrived.transform.position;
        newObject.Spawner = this;
        _notes.Add(newObject);
    }
}
