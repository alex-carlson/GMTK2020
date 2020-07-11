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
  public float throwForce = 8;

  private Rigidbody headRB;
  public Rigidbody bodyRB;
  private float moveSpeed = 0;

  void Update()
  {
    Vector3 playerRotation = transform.rotation.eulerAngles;
    Vector3 cameraRotation = playercam.transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(playerRotation.x, cameraRotation.y, cameraRotation.z);

    animator.SetFloat("walkSpeed", Mathf.Abs(Input.GetAxis("Vertical")));

    Move();

    if (Input.GetButtonDown("Fire1"))
    {
      DoLeftClick();
    }

    if (Input.GetButtonDown("Fire2"))
    {
      DoRightClick();
    }

    CheckRaycast();
  }

  void DoLeftClick()
  {
    if (isHolding)
    {
      Drop();
    }
    if (highlightedObject)
    {
      Grab(highlightedObject);
    }
  }

  void DoRightClick()
  {
    if (isHolding && highlightedObject) Throw();
  }

  void Move()
  {
    bodyRB.velocity =
      ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")))
        * walkSpeed
        * Time.deltaTime;
  }

  void CheckRaycast()
  {
    RaycastHit hit;
    if (isHolding) return;

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
    StartCoroutine("ThrowItem");
  }

  IEnumerator ThrowItem()
  {
    arm.currentProp = null;
    isHolding = false;
    animator.SetBool("isHolding", false);
    yield return new WaitForSeconds(0.1f);
    highlightedObject.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * throwForce, ForceMode.Impulse);
    yield return new WaitForSeconds(0.1f);
    highlightedObject = null;
  }
}
