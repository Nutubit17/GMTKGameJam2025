using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeadManager : MonoBehaviour
{
   [SerializeField] private GameObject _image;
   [SerializeField] private String _introSceneName;

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
            SceneManager.LoadScene(_introSceneName);
         }
      }
   }
}
