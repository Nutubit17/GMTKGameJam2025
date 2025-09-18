using UnityEngine;
using UnityEngine.Events;

public class InteractiveEventer : MonoBehaviour, IInteractiveable
{
    [SerializeField]
    private UnityEvent _interactiveEvent;
    public ItemDataAndSO Intreractive()
    {
       _interactiveEvent?.Invoke();
        return null;
    }
}
