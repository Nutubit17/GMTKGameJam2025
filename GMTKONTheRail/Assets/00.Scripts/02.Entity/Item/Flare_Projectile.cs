using UnityEngine;

public class Flare_Projectile : MonoBehaviour
{
    private float _lifetime = 5;

    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        Destroy(gameObject,_lifetime);
    }

}
