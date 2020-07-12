using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  // instruction UIs
  GameObject pickUP;
  GameObject Throw;
  GameObject timerUI;
  TextMeshProUGUI daysLeft;

  float timerRotate = 0;
  Vector3 rotateTarget = Vector3.zero;

  bool spunUP = false;

  private void loadObjects()
  {
    pickUP = GameObject.Find("pickup");
    Throw = GameObject.Find("throw");
    timerUI = GameObject.Find("Time Dial");
  }

    public void toggleInstructions()
    {
        //activate or deactivate the daytime control instructions
        pickUP.SetActive(!pickUP.activeSelf);
        Throw.SetActive(!Throw.activeSelf);
    }


  public IEnumerator rotateTimer(float time)
  {
    if (!spunUP)
    {
      loadObjects();
      timerRotate = timerUI.transform.rotation.z;
      spunUP = true;
    }

    timerRotate += 180;
        Debug.Log(timerRotate);
    timerUI.transform.DOLocalRotate(new Vector3(0, 0, timerRotate), time - 1).SetEase(Ease.Linear);
    yield return new WaitForSeconds(time);
  }
}
