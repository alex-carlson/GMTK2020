using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

public class PlayerController : MonoBehaviour
{
  public Transform Head;
  public Transform ArmRight;
  public Transform ArmLeft;
  public Camera playercam;
  public float turnSpeed = 5;
  public float walkSpeed = 5;
  public float armLength = 4;
  public LayerMask grabbableObjects;
  public GameObject highlightedObject;
  public PuppetMaster puppet;
  public PropMuscle arm;

  private Rigidbody headRB;

  void Update()
  {
    Vector3 playerRotation = transform.rotation.eulerAngles;
    Vector3 cameraRotation = playercam.transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(playerRotation.x, cameraRotation.y, cameraRotation.z);

    if (Input.GetAxis("Vertical") > 0)
    {
      Forward();
    }

    if (Input.GetAxis("Vertical") < 0)
    {
      BackUp();
    }

    if (Input.GetButtonDown("Fire1"))
    {
      DoAction();
    }

    CheckRaycast();
  }

  void DoAction()
  {
    Debug.Log("Clicked the mouse");
    if (highlightedObject)
    {
      Grab(highlightedObject);
    }
  }

  void Turn()
  {
    transform.Rotate((Vector3.up * Input.GetAxis("Horizontal")) * turnSpeed * Time.deltaTime);
  }

  void Forward()
  {
    transform.Translate((Vector3.forward * Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime);
  }

  void BackUp()
  {
    transform.Translate((Vector3.forward * Input.GetAxis("Vertical")) * (walkSpeed / 4) * Time.deltaTime);
  }

  void CheckRaycast()
  {
    RaycastHit hit;

    Debug.DrawRay(playercam.transform.position, playercam.transform.forward, Color.magenta, Time.deltaTime);

    if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out hit, armLength, grabbableObjects))
    {

      if (hit.transform.GetComponent<PuppetMasterProp>())
      {
        highlightedObject = hit.transform.gameObject;
      }
    }
  }

  void Grab(GameObject go)
  {
    highlightedObject = go;
    arm.currentProp = go.GetComponent<PuppetMasterProp>();
  }

  void Drop()
  {

  }
}
