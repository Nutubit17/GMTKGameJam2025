using UnityEngine;

public class MoveCompo : MonoBehaviour, IGetCompoable
{
    protected Entity _agent;

    public Rigidbody rigidCompo;
    public virtual void Init(Entity agent)
    {
        _agent = agent;
    }


}
