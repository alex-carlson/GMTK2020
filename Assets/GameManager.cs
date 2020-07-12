using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
  [SerializeField] UIManager _uiManager;
  public int daytimeSceneIndex = 1;
  public int nighttimeSceneIndex = 2;
  public int propsSceneIndex = 3;

  [SerializeField] float minPhaseTime = 60f;
  [SerializeField] float maxPhaseTime = 120f;
  public float cycleTime = 10f;
  public TextMeshProUGUI daysRemainingText;
  public RawImage reticle;
  public CanvasGroup TransitionGraphic;
  void Start()
  {
    StartCoroutine(DayCycle());
  }

  void setCycle()
  {
    cycleTime = Random.Range(minPhaseTime, maxPhaseTime);
    StartCoroutine(_uiManager.rotateTimer(cycleTime));
  }

  IEnumerator DayCycle()
  {
    if (SceneManager.GetSceneByBuildIndex(nighttimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(nighttimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    if (!SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.LoadSceneAsync(daytimeSceneIndex, LoadSceneMode.Additive);
    reticle.enabled = true;
    setCycle();
    yield return new WaitForSeconds(cycleTime);
    TransitionGraphic.DOFade(1, 1);
    yield return new WaitForSeconds(4);
    TransitionGraphic.DOFade(0, 1);
    StartCoroutine("NightCycle");
  }

  IEnumerator NightCycle()
  {

    if (SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(daytimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(nighttimeSceneIndex, LoadSceneMode.Additive);
    reticle.enabled = false;
    setCycle();
    yield return new WaitForSeconds(cycleTime);
    TransitionGraphic.DOFade(1, 1);
    yield return new WaitForSeconds(4);
    TransitionGraphic.DOFade(0, 1);
    StartCoroutine("DayCycle");
  }
}
