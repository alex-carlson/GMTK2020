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
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float zMin;
        public float zMax;
        public int cost;
    }

    public Rotations[] rotations;

    public int defaultCost;
}
