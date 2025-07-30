using System;
using UnityEngine;
using UnityEngine.Events;


public enum AgentPlayerActions
{
    Fire1,
    Fire2,
    Melee,
    Reload,
    Jump,
    Crouch,
    Slide,
    Move,
    Die,
    Dash,
    Fall,
    Skill1,
    Skill2,
    End
    
}

[Serializable]
public struct EventAndAfterAction
{
    
    public UnityEvent PostEvent;
    public UnityEvent AfterEvent;

    public void AddPostListener(UnityAction action)
    {
        PostEvent.AddListener(action);
    }
    public void AddAfterListener(UnityAction action)
    {
        AfterEvent.AddListener(action);
    }

    public void Invoke()
    {
        PostEvent.Invoke();
        AfterEvent.Invoke();
    }
    //public static UnityEvent operator +(UnityAction action)
    //{
    //    PostEvent.AddListener(action);
    //}
}
public class AgentActionEventCompo : MonoBehaviour,IGetCompoable
{
#if UNITY_EDITOR
    [NamedArray(typeof(AgentPlayerActions))]
#endif
    public EventAndAfterAction[] actions = new EventAndAfterAction[(int)AgentPlayerActions.End];

    protected Agent _agent;


    public void Init(Agent agent)
    {
       _agent = agent;
        
    }

    public void InvokeAction(AgentPlayerActions action)
    {
        actions[(int)action].Invoke();
    }
}
