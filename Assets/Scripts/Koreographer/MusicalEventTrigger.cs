using UnityEngine;
using SonicBloom.Koreo;

public class MusicalEventTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject eventActor;

    [EventID]
    public string eventID;

    private Vector3 startScale;
    private float scaleKoefficient = 1.2f;
    private Vector3 currentScale;
    private float fallofTime = .5f;
    private float falloffTimertCont = 0;

    private void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnEventAction);
    }

    private void Start()
    {
        if(eventActor != null)
        {
            startScale = eventActor.transform.localScale;
        }
    }

    private void Update()
    {
        if(falloffTimertCont > 0)
        {
            falloffTimertCont -= Time.deltaTime;
        }

        SetActorScale();
    }

    private void OnEventAction(KoreographyEvent evt)
    {
        currentScale = startScale * scaleKoefficient;

        falloffTimertCont = fallofTime;
        SetActorScale();
    }

    private void SetActorScale()
    {
        eventActor.transform.localScale = Vector3.Lerp(startScale, startScale * scaleKoefficient , falloffTimertCont / fallofTime); 
    }
}
