using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField]
    private Vector3 _fromDir = Vector3.forward;
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(_fromDir, Camera.main.transform.position - transform.position);
    }
}
