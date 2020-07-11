using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] UIManager _uiManager;

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
        setCycle();
    yield return new WaitForSeconds(cycleTime);
    StartCoroutine("NightCycle");
  }

  IEnumerator NightCycle()
  {

        if (SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(daytimeSceneIndex);
        if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(nighttimeSceneIndex, LoadSceneMode.Additive);
        setCycle();
        yield return new WaitForSeconds(cycleTime);
    StartCoroutine("DayCycle");
  }
}
