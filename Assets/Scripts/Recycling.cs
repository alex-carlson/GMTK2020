using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycling : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Recycleable")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Trash") Destroy(other.gameObject);
    }
}
