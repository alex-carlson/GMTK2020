using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestingSurfaceCriteria", menuName = "Criteria/Resting Surface")]
public class RestingSurfaceCriteria : EvaluationCriteria
{
    [Serializable]
    public struct SurfaceCost
    {
        public SurfaceType surface;
        public int cost;
    }

    public SurfaceCost[] surfaces;
    public int defaultCost;
}
