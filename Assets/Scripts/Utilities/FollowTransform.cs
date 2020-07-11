using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

  public Transform attachedObject;
  public Vector3 offset;

  private void Update()
  {
    if (attachedObject)
    {
      transform.position = attachedObject.position + offset;
    }
  }
}
