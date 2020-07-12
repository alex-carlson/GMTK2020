using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchMaterial : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager managerRef;

    [SerializeField] Material day;
    [SerializeField] Material night;
    Material myMaterial;

    
    void Start()
    {
        managerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
        managerRef.Switch.AddListener(DaySwitch);
        managerRef.Switch.AddListener(NightSwitch);
        myMaterial = gameObject.GetComponent<Material>();
    }

    void DaySwitch()
    {
        
    }

    void NightSwitch()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
