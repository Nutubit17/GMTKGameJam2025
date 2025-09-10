using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _targetItem;

    public void SpawnItem()
    {
        int idx = 0;
        if(_targetItem.Length > 1)
            idx = Random.Range(0, _targetItem.Length);
        if (_targetItem[idx] == null)
            return;
        Instantiate(_targetItem[idx], transform.position,transform.rotation);
    }
}
