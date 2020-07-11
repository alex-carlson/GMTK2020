using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public int DaytimeCounterSeconds = 120;
  public int NighttimeCounterSeconds = 60;
  public int daytimeSceneIndex = 1;
  public int nighttimeSceneIndex = 2;
  public int propsSceneIndex = 3;

    [SerializeField] float minPhaseTime = 5f;
    [SerializeField] float maxPhaseTime = 40f;
  void Start()
  {
    StartCoroutine("DayCycle");
  }

  float getPhaseTime()
    {
        float phaseTime = Random.Range(minPhaseTime, maxPhaseTime);
        return phaseTime;
    }

  IEnumerator DayCycle()
  {
    if (SceneManager.GetSceneByBuildIndex(nighttimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(nighttimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(daytimeSceneIndex, LoadSceneMode.Additive);
    yield return new WaitForSeconds(DaytimeCounterSeconds);
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
