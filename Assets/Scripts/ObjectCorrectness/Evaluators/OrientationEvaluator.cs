using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationEvaluator : ObjectEvaluator
{
    public override Type Criteria => typeof(OrientationCriteria);

    public override int Evaluate(EvaluationCandidateComponent evaluationCandidate)
    {
        EvaluationCriteria genericCriteria = GetEvaluationCriteria(evaluationCandidate);
        if (genericCriteria == null)
        {
            Debug.LogError("Should be evaluating an Orientation, but couldn't find Orientation criteria.");
            return 0;
        }

        OrientationCriteria objectCriteria = (OrientationCriteria) genericCriteria;
        
        Vector3 candidateTransform = evaluationCandidate.transform.rotation.eulerAngles;
        foreach (OrientationCriteria.Rotations r in objectCriteria.rotations)
        {
            Vector3 ct = candidateTransform;
            if (r.xMin <= ct.x && ct.x <= r.xMax &&
                r.yMin <= ct.y && ct.y <= r.yMax &&
                r.zMin <= ct.z && ct.z <= r.zMax)
            {
                return r.cost;
            }
        }

        return objectCriteria.defaultCost;
    }
}
