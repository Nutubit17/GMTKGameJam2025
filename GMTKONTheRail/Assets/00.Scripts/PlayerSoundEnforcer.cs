using UnityEngine;

public class PlayerSoundEnforcer : MonoBehaviour,IGetCompoable
{
    [SerializeField]
    private AudioSource _audioSource;
    public Entity Mom;

    public void Init(Entity agent)
    {
        Mom = agent;
    }
    private void Start()
    {
        if(_audioSource != null)
            _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio, float volume)
    {
        _audioSource.PlayOneShot(audio, volume);
    }
}
