using UnityEngine;
using SonicBloom.Koreo;

public class SpawnByEvent : MonoBehaviour
{
    [SerializeField]
    private NoteObject spawnebleActorPrefab;

    [SerializeField]
    private GameObject pointToArrived;

    [EventID]
    public string eventID;

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
    }
}
