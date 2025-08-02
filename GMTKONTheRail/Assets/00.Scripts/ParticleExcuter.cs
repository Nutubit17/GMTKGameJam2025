using UnityEngine;

public class ParticleExcuter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    public void RunParticle()
    {
        _particleSystem.Play();
    }
}
