using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * To create a new metric to evaluate objects on, implement this class.
 * - Zack
 */
public abstract class ObjectEvaluator : MonoBehaviour
{
    public abstract int Evaluate(EvaluationCandidateComponent evaluationCandidate);

    public abstract Type Criteria { get; }

    public EvaluationCriteria GetEvaluationCriteria(EvaluationCandidateComponent evaluationCandidate)
    {
        foreach (EvaluationCriteria evalCriteria in evaluationCandidate.criteria)
        {
            if (evalCriteria.GetType() == Criteria)
            {
                return evalCriteria;
            }
        }

        return null;
    }
}
