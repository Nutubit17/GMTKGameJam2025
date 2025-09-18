using UnityEngine;

public class BlockingBar : MonoBehaviour
{
    [SerializeField]
    private Animation _animatiion;
    [SerializeField]
    private AnimationClip _clip;

    public void PlayAnim()
    {
        _animatiion.Play(_clip.name);
    }
}
