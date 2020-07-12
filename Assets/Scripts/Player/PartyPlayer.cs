using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartyPlayer : MonoBehaviour
{
  public GameObject[] props;
  public int TargetChangeWaitTime = 5;
  public GameObject target = null;
  public float currentMoveSpeed = 0;
  public Animator animator;
  public FixedJoint hand;
  public float ThrowStrength = 15;
  public float currentDistance;
  public bool isThrowing = false;
  public NavMeshAgent agent;
  public AudioSource audioSource;

  private void Start()
  {
    props = GameObject.FindGameObjectsWithTag("Prop");
    StartCoroutine("ChangeTarget");
  }

  void Update()
  {
    if (!target) return;
    currentMoveSpeed = agent.velocity.magnitude;
    animator.SetFloat("walkSpeed", currentMoveSpeed);
    currentDistance = Vector3.Distance(transform.position, target.transform.position);
    if (currentDistance < 3)
    {
      StartCoroutine("GrabAndThrow");
    }
  }

  IEnumerator GrabAndThrow()
  {
    if (target && !isThrowing)
    {
      StopCoroutine("ChangeTarget");
      isThrowing = true;
      animator.SetBool("isHolding", true);
      target.transform.position = hand.transform.position;
      hand.connectedBody = target.GetComponent<Rigidbody>();
      yield return new WaitForSeconds(0.5f);
      audioSource.Play();
      hand.connectedBody = null;
      target.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * ThrowStrength, ForceMode.Impulse);
      target = null;
      isThrowing = false;
      animator.SetBool("isHolding", false);
      StartCoroutine("ChangeTarget");
    }
  }

  IEnumerator ChangeTarget()
  {
    target = props[Mathf.RoundToInt(Random.Range(0, props.Length - 1))].gameObject;
    agent.SetDestination(target.transform.position);
    yield return new WaitForSeconds(TargetChangeWaitTime);
    StartCoroutine("ChangeTarget");
  }
}
