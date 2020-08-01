using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreUI : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI finalGrade;
  [SerializeField] TextMeshProUGUI parentsMessage;

  GameManager managerRef;

  private string clean = "Clean";
  private string Immaculate = "Disorganized";
  private string Messy = "Messy";
  private string Filthy = "Filthy";

  private string happy = "Your Parents are pleasently surprised.";
  private string ambivilent = "Your parents are not surprised.";
  private string unhappy = "Your parents are not angry, just dissapointed";
  private string mad = "Your parents can't believe the state of things";


  // Start is called before the first frame update
  void Start()
  {
    managerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
    calculateScore();
  }

  void calculateScore()
  {
    //<=10 happy
    if (managerRef.score <= 10)
    {
      finalGrade.text = clean;
      parentsMessage.text = happy;
    }
    // >= 10 ambivilent
    if (managerRef.score > 10 && managerRef.score < 30)
    {
      finalGrade.text = Immaculate;
      parentsMessage.text = ambivilent;
    }
    // >= 30 unhappy
    if (managerRef.score >= 30 && managerRef.score < 50)
    {
      finalGrade.text = Messy;
      parentsMessage.text = unhappy;
    }
    //>= 50 unhappy
    if (managerRef.score >= 50)
    {
      finalGrade.text = Filthy;
      parentsMessage.text = mad;
    }
  }
}
