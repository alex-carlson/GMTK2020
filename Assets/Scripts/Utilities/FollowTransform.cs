using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

  public Transform attachedObject;
  public Vector3 offset;

  private void Start()
  {
    if (!attachedObject) attachedObject = GameObject.Find("Head").transform;
  }

  private void Update()
  {
    transform.position = attachedObject.position + offset;
  }
}
