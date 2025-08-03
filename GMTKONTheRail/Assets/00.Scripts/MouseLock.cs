using UnityEngine;

public class MouseLock : MonoBehaviour
{

    public bool SetLock = true;
    void Start()
    {
        bool isLocked = SetLock;

        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // ȭ�� �߾� ����
            Cursor.visible = false;                   // Ŀ�� �����
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;   // ���� �̵�
            Cursor.visible = true;                    // Ŀ�� ���̱�
        }
    }
}
