using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

public class PartyPlayer : MonoBehaviour
{
  public GameObject[] props;
  public int TargetChangeWaitTime = 5;
  public GameObject target = null;
  public float MoveSpeed = 5;
  public float maxSpeedMultiplier = 25;
  public Rigidbody rb;
  public float moveSpeed = 0;
  public Animator animator;

  private void Start()
  {
    props = GameObject.FindGameObjectsWithTag("Prop");
    StartCoroutine("ChangeTarget");
  }

  void Update()
  {
    if (!target) return;
    moveSpeed = rb.velocity.magnitude;
    animator.SetFloat("walkSpeed", MoveSpeed);
    MoveTowards();
  }

  void MoveTowards()
  {
    float distance = Vector3.Distance(transform.position, target.transform.position);
    transform.LookAt(target.transform, Vector3.up);
    rb.AddForce(transform.forward * MoveSpeed * Mathf.Clamp(0.5f, maxSpeedMultiplier, distance), ForceMode.Acceleration);
  }

  IEnumerator ChangeTarget()
  {
    target = props[Mathf.RoundToInt(Random.Range(0, props.Length - 1))].gameObject;
    yield return new WaitForSeconds(TargetChangeWaitTime);
    StartCoroutine("ChangeTarget");
  }
}
