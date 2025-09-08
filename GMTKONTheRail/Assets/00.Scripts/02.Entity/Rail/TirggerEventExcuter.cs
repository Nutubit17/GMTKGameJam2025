using UnityEngine;
using UnityEngine.Events;

public class TirggerEventExcuter : MonoBehaviour
{
    [SerializeField]
    private string _targetTag = "Cart";

    public UnityEvent OnTriggerEvnet;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _targetTag)
        {
            OnTriggerEvnet?.Invoke();
        }
    }
}
