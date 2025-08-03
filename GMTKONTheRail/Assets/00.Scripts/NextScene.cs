using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private String _nextSceneName;
    
    public void NextSceneMingMing()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
