using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
  public int tutorialBubbleStartTime = 1;
  public int tutorialBubbleStayTime = 8;
  private int index = 0;

  private void Start()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
    int days = FindObjectOfType<GameManager>().daysLeft;
    if (days != 4) return;
    StartCoroutine("ShowSlide");
  }

  IEnumerator ShowSlide()
  {
    yield return new WaitForSeconds(tutorialBubbleStartTime);
    if (index != 0)
    {
      transform.GetChild(index - 1).gameObject.SetActive(false);
    }
    transform.GetChild(index).gameObject.SetActive(true);
    index++;
    yield return new WaitForSeconds(tutorialBubbleStayTime);
    if (index <= transform.childCount) StartCoroutine("ShowSlide");
  }
}
