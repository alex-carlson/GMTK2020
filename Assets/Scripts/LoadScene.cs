using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
  public string sceneToLoad = "Menu";

  public void LoadLevel()
  {
    SceneManager.LoadScene(sceneToLoad);
  }
}
