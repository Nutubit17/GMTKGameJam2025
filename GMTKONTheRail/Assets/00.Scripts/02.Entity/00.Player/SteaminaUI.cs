using UnityEngine;
using UnityEngine.UI;

public class SteaminaUI : MonoBehaviour
{
    [SerializeField]
    private Image _bar;
    [SerializeField]
    private PlayerSatus _playerSatus;
    void Update()
    {
        //if()
        {
            _bar.enabled = (transform.localScale.x <= _playerSatus.MaxStemina/_playerSatus.MaxMaxStemina) || 
                (_playerSatus.CurrentStemina < _playerSatus.MaxStemina);
        }
        transform.localScale = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.localScale.x, _playerSatus.CurrentStemina / _playerSatus.MaxMaxStemina,0.2f) + 0.01f,0,1), 1,1);
    }
}
