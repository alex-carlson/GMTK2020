using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class PlayerController : MonoBehaviour
{
  public Camera playercam;
  public Transform head;
  public float walkSpeed = 5;
  public float armLength = 4;
  public LayerMask grabbableObjects;
  public GameObject highlightedObject;
  private GameObject lastHighlighted;
  public FixedJoint grabPoint;
  public Animator animator;
  public bool isHolding;
  public float throwForce = 8;
  public Color highlightColor = Color.white;

  public Rigidbody bodyRB;
  public LimbIK[] limbs;
  private float moveSpeed = 0;

  void Update()
  {
    if (!playercam) playercam = Camera.main;
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
    Ray ray = playercam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit;

    if (isHolding) return;

    if (Physics.Raycast(ray, out hit, armLength, grabbableObjects))
    {
      highlightedObject = hit.transform.gameObject;
      HighlightObject(hit.transform.gameObject);
    }
    else
    {
      ClearHighlight();
    }
  }

  void HighlightObject(GameObject go)
  {
    if (go != lastHighlighted)
    {
      ClearHighlight();
      go.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_2B2C0B3D", highlightColor);
      lastHighlighted = go;
    }
  }

  void ClearHighlight()
  {
    if (lastHighlighted != null)
    {
      lastHighlighted.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_2B2C0B3D", Color.black);
      lastHighlighted = null;
    }
  }

  void Grab(GameObject go)
  {
    // TODO: remove prop logic, just parent on your own
    foreach (LimbIK limb in limbs)
    {
      limb.solver.IKPositionWeight = 1;
    }
    isHolding = true;
    highlightedObject = go;
    animator.SetBool("isHolding", isHolding);
    go.transform.position = grabPoint.transform.position;
    grabPoint.connectedBody = go.GetComponent<Rigidbody>();
    grabPoint.transform.GetComponent<MeshRenderer>().material.SetFloat("Vector1_237C8C32", 0.5f);
  }

  void Drop()
  {
    isHolding = false;
    animator.SetBool("isHolding", isHolding);
    highlightedObject = null;
    grabPoint.connectedBody = null;
    grabPoint.transform.GetComponent<MeshRenderer>().material.SetFloat("Vector1_237C8C32", 1f);
    foreach (LimbIK limb in limbs)
    {
      limb.solver.IKPositionWeight = 0;
    }
  }

  void Throw()
  {
    StartCoroutine("ThrowItem");
  }

  IEnumerator ThrowItem()
  {
    grabPoint.connectedBody = null;
    isHolding = false;
    animator.SetBool("isHolding", false);
    yield return new WaitForSeconds(0.1f);
    highlightedObject.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * throwForce, ForceMode.Impulse);
    yield return new WaitForSeconds(0.1f);
    highlightedObject = null;
    grabPoint.transform.GetComponent<MeshRenderer>().material.SetFloat("Vector1_237C8C32", 1f);
    foreach (LimbIK limb in limbs)
    {
      limb.solver.IKPositionWeight = 0;
    }
  }
}
