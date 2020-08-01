using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

  private void Awake()
  {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }
  public void NewGame()
  {
    SceneManager.LoadScene(1);
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }
}
