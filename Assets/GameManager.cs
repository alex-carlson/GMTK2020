using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  UIManager _uiManager;

  public int DaytimeCounterSeconds = 120;
  public int NighttimeCounterSeconds = 60;
  public int daytimeSceneIndex = 1;
  public int nighttimeSceneIndex = 2;
  public int propsSceneIndex = 3;

  [SerializeField] float minPhaseTime = 60f;
  [SerializeField] float maxPhaseTime = 120f;
  public float cycleTime = 10f;

  [SerializeField] int daysLeft = 4;
  void Start()
  {
    GameObject.Find("UI Manager").GetComponent<UIManager>();
    StartCoroutine("DayCycle");
  }

  float getPhaseTime()
  {
    float phaseTime = Random.Range(minPhaseTime, maxPhaseTime);
    return phaseTime;
  }

  IEnumerator DayCycle()
  {
    daysLeft--;
    if (SceneManager.GetSceneByBuildIndex(nighttimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(nighttimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    if (!SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.LoadSceneAsync(daytimeSceneIndex, LoadSceneMode.Additive);
        cycleTime = getPhaseTime();
        Debug.Log(cycleTime);
        StartCoroutine(_uiManager.rotateTimer(cycleTime));
    yield return new WaitForSeconds(cycleTime);
    StartCoroutine("NightCycle");
  }

  IEnumerator NightCycle()
  {

    if (SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(daytimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(nighttimeSceneIndex, LoadSceneMode.Additive);
    yield return new WaitForSeconds(NighttimeCounterSeconds);
    StartCoroutine("DayCycle");
  }
}
