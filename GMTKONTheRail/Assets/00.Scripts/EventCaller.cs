using UnityEngine;
using UnityEngine.Events;

public class EventCaller : MonoBehaviour
{
    public UnityEvent[] BashEvents;
    public void RunEvent(int idx)
    {
        BashEvents[idx]?.Invoke();
    }

}
