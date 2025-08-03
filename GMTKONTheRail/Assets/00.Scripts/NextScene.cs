using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField] private String _nextSceneName;
    [SerializeField] private Image _image;


    public IEnumerator routine()
    {
        float percent = 0;
        var group = _image.GetComponent<CanvasGroup>();

        while (percent <= 1)
        {
            group.alpha = percent;
            _image.rectTransform.position += Vector3.up * 4 * Time.deltaTime;

            percent += Time.deltaTime * 0.75f;
            yield return null;
        }

        SceneManager.LoadScene(_nextSceneName);
    }

    public void NextSceneMingMing()
    {
        StartCoroutine(routine());
    }

    public void Exit()
    {
        Application.Quit();
    }
}
