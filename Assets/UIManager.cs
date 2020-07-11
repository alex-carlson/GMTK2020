using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // instruction UIs
    [SerializeField] GameObject instructUI;
    [SerializeField] GameObject timerUI;
    [SerializeField] TextMeshProUGUI daysLeft;

    float timerRotate = 180;
    Vector3 rotateTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void toggleInstructions()
    {
        //activate or deactivate the daytime control instructions
        instructUI.SetActive(!instructUI.activeSelf);
    }

    public IEnumerator rotateTimer(float time)
    {
        //DOTween.To(() => timerRotation, x => timerRotation = x, toRotation, time);
        transform.DORotate(new Vector3(0, 0, timerRotate), time);
        yield return new WaitForSeconds(time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
