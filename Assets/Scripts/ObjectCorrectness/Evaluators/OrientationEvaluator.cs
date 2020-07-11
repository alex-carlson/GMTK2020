using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Note that this evaluates the orientation criteria against the direction the top of the object is
 * facing, so requiring objects to be facing a horizontal direction requires a bit of finessing.
 * - Zack
 */
public class OrientationEvaluator : ObjectEvaluator
{
    public override Type Criteria => typeof(OrientationCriteria);

    public override int Evaluate(EvaluationCandidateComponent evaluationCandidate, EvaluationCriteria genericCriteria)
    {

        OrientationCriteria orientationCriteria = (OrientationCriteria) genericCriteria;
        
        Vector3 candidateDirection = evaluationCandidate.transform.up;
        foreach (OrientationCriteria.Rotations r in orientationCriteria.rotations)
        {
            Vector3 evaluatingOrientation = r.direction.normalized;
            if (Vector3.Dot(evaluatingOrientation, candidateDirection) > .9)
            {
                return r.cost;
            }
        }

        return orientationCriteria.defaultCost;
    }
}
