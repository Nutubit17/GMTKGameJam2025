using UnityEngine;

public class MouseLock : MonoBehaviour
{

    public bool SetLock = true;
    void Start()
    {
        bool isLocked = SetLock;

        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // 화면 중앙 고정
            Cursor.visible = false;                   // 커서 숨기기
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;   // 자유 이동
            Cursor.visible = true;                    // 커서 보이기
        }
    }
}
