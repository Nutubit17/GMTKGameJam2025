using UnityEngine;

public class MoveCompo : MonoBehaviour, IGetCompoable
{
    protected Agent _agent;

    public Rigidbody rigidCompo;
    public virtual void Init(Agent agent)
    {
        _agent = agent;
    }


}
