using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeadManager : MonoBehaviour
{
   [SerializeField] private GameObject _image;
   [SerializeField] private String _introSceneName;
   [SerializeField] private GameObject _loadingObj;

   private bool bCanPressSpace = false;
   
   
   private void Awake()
   {
      _image.SetActive(false);
   }

   public void Dead()
   {
      _image.SetActive(true);
      
      bCanPressSpace = true;
   }


   private void Update()
   {
      if (bCanPressSpace)
      {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            _loadingObj?.SetActive(true);
            SceneManager.LoadScene(_introSceneName);
         }
      }
   }
}
