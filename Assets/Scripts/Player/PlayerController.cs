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
  public Animator animator;
  public bool isHolding;

  private Rigidbody headRB;
  public Rigidbody bodyRB;

  void Update()
  {
    Vector3 playerRotation = transform.rotation.eulerAngles;
    Vector3 cameraRotation = playercam.transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(playerRotation.x, cameraRotation.y, cameraRotation.z);

    animator.SetFloat("walkSpeed", Mathf.Abs(Input.GetAxis("Vertical")));

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
    if (isHolding)
    {
      Throw();
    }
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
    bodyRB.AddForce((transform.forward * Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime);
  }

  void BackUp()
  {
    bodyRB.AddForce((transform.forward * Input.GetAxis("Vertical")) * (walkSpeed / 4) * Time.deltaTime);
  }

  void CheckRaycast()
  {
    RaycastHit hit;

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
    isHolding = true;
    animator.SetBool("isHolding", isHolding);
    highlightedObject = go;
    arm.currentProp = go.GetComponent<PuppetMasterProp>();
  }

  void Drop()
  {
    isHolding = false;
    animator.SetBool("isHolding", isHolding);
    highlightedObject = null;
    arm.currentProp = null;
  }

  void Throw()
  {
    highlightedObject.transform.parent.GetComponent<Rigidbody>().AddForce(playercam.transform.forward, ForceMode.Impulse);
    Drop();
  }
}
