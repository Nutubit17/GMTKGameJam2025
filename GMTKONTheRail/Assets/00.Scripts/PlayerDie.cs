using UnityEngine;

public class PlayerDie : MonoBehaviour, IGetCompoable
{
    public Entity Mom;


    public void Init(Entity agent)
    {
        Mom = agent;
    }

    public void DieEvent()
    {

    }

    public void TestDIeEvent()
    {
        Debug.Log("U DIe");
    }
}
