using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    public System.Action OnAnimationEnd;

    private void AnimationEndTrigger()
    {
        OnAnimationEnd?.Invoke();
    }

    private void SoundEffectTrigger(AudioClip audioClip)
    {
        
    }

    private void AttackTrigger()
    {

    }
}
