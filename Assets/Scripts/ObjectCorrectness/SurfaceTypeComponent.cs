using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceTypeComponent : MonoBehaviour
{

    public SurfaceType[] types;

    // Start is called before the first frame update
    void Start()
    {
        if (types == null || types.Length <= 0)
        {
            Debug.LogWarningFormat("Object {0} has been given a surface type component with no surface type. Will be treated as typeless.");
        }
    }
}
