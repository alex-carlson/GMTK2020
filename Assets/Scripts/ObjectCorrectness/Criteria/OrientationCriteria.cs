using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrientationCriteria", menuName = "Criteria/Orientation Criteria")]
public class OrientationCriteria : EvaluationCriteria
{
    [Serializable]
    public struct Rotations
    {
        public Vector3 direction;
        public int cost;
    }

    public Rotations[] rotations;

    public int defaultCost;
}
