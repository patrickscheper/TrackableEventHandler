using UnityEngine;
using UnityEngine.Events;
using Vuforia;

[RequireComponent(typeof(TrackableBehaviour))]
public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;

    public UnityEvent OnTrackingFound;
    public UnityEvent OnTrackingLost;

    private void Awake()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound?.Invoke();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost?.Invoke();
        }
        else
            OnTrackingLost?.Invoke();
    }
}
