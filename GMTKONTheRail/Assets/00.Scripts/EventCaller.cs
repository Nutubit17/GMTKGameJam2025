using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class AudioClipAndVolume
{

    public AudioClip Clip;
    public float Volume;


}


public class EventCaller : MonoBehaviour,IGetCompoable
{
    public UnityEvent[] BashEvents;

    public Entity Mom;
    private PlayerSoundEnforcer _plse;

    public float Volume =1;

    public void RunEvent(int idx)
    {
        BashEvents[idx]?.Invoke();
    }

    public void PlaySound(AudioClip clip)
    {

        _plse.PlaySound(clip,Volume);
    }

    public void SetVolume(float volume)
    {
        this.Volume = volume;
    }

    private void Start()
    {
        _plse = Mom.GetCompo<PlayerSoundEnforcer>();
    }

    public void Init(Entity agent)
    {
        Mom = agent;
    }
}
