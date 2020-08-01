using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
  [SerializeField] UIManager _uiManager;
  public int daytimeSceneIndex = 1;
  public int nighttimeSceneIndex = 2;
  public int endGameSceneIndex = 4;
  public int propsSceneIndex = 3;

  [SerializeField] float minPhaseTime = 30f;
  [SerializeField] float maxPhaseTime = 45f;
  public float cycleTime = 10f;
  public int daysLeft = 4;
  [Space]
  public TextMeshProUGUI daysRemainingText;
  public RawImage reticle;
  public CanvasGroup TransitionGraphic;
  public TextMeshProUGUI scoreText;
  public Image scoreBar;
  public ObjectCorrectnessTracker tracker;
  public Material[] roomMaterials;
  public Color highlightColor;
  [Space]
  public UnityEvent Switch = new UnityEvent();
  [Space(20)]
  public int baseScore = 9;
  public int minScore = 0;
  public int maxScore = 60;
  public float score;
  public float normalized;
  void Start()
  {
    StartCoroutine(DayCycle());
    InvokeRepeating("UpdateScore", 1, 1);
    daysRemainingText.text = daysLeft.ToString();
    ChangeTint(false);
  }

  void setCycle()
  {
    cycleTime = Random.Range(minPhaseTime, maxPhaseTime);
    StartCoroutine(_uiManager.rotateTimer(cycleTime));
  }

  void UpdateScore()
  {
    tracker.UpdateCandidates();
    score = tracker.currentScore - baseScore;
    normalized = Mathf.Clamp(score / maxScore, 0, 1);

    scoreText.text = "Score: " + tracker.currentScore;
    scoreBar.fillAmount = 1 - normalized;
    scoreBar.color = new Color(normalized, 1 - normalized, 0);
  }

  IEnumerator DayCycle()
  {
    tracker.UpdateCandidates();
    if (SceneManager.GetSceneByBuildIndex(nighttimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(nighttimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    if (!SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.LoadSceneAsync(daytimeSceneIndex, LoadSceneMode.Additive);
    reticle.enabled = true;
    setCycle();
    yield return new WaitForSeconds(cycleTime);
    TransitionGraphic.DOFade(1, 1);
    yield return new WaitForSeconds(4);
    ChangeTint(true);
    TransitionGraphic.DOFade(0, 1);
    RemoveDay();
    if (daysLeft <= 0)
    {
      StopCoroutine("NightCycle");
      StopCoroutine("DayCycle");
      yield return null;
    }
    StartCoroutine("NightCycle");
  }

  IEnumerator NightCycle()
  {
    tracker.UpdateCandidates();
    if (SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(daytimeSceneIndex);
    if (!SceneManager.GetSceneByBuildIndex(propsSceneIndex).isLoaded) SceneManager.LoadSceneAsync(propsSceneIndex, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(nighttimeSceneIndex, LoadSceneMode.Additive);
    reticle.enabled = false;
    setCycle();
    yield return new WaitForSeconds(cycleTime / 2);
    TransitionGraphic.DOFade(1, 1);
    ChangeTint(false);
    yield return new WaitForSeconds(4);
    TransitionGraphic.DOFade(0, 1);
    StartCoroutine("DayCycle");
  }

  void ChangeTint(bool darken = true)
  {
    foreach (Material m in roomMaterials)
    {
      Color newColor;

      if (darken)
      {
        newColor = highlightColor;
      }
      else
      {
        newColor = Color.white;
      }
      m.SetColor("Color_57D43A16", newColor);
    }
  }
  void RemoveDay()
  {
    daysLeft--;
    daysRemainingText.text = daysLeft.ToString();
    if (daysLeft <= 0)
    {
      Debug.Log("end game");
      StopCoroutine("NightCycle");
      StopCoroutine("DayCycle");
      //end the game
      if (SceneManager.GetSceneByBuildIndex(nighttimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(nighttimeSceneIndex);
      if (SceneManager.GetSceneByBuildIndex(daytimeSceneIndex).isLoaded) SceneManager.UnloadSceneAsync(daytimeSceneIndex);
      SceneManager.LoadSceneAsync(endGameSceneIndex, LoadSceneMode.Additive);
      Invoke("ReturnToMenu", 10);
    }
  }

  void ReturnToMenu()
  {
    SceneManager.LoadScene(0);
  }
}
