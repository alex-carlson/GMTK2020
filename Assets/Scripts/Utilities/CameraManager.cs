using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public Camera playerCamera;
  public Camera OverheadCamera;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      OverheadCamera.enabled = false;
      playerCamera.enabled = true;
    }

    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      playerCamera.enabled = false;
      OverheadCamera.enabled = true;
    }
  }
}
