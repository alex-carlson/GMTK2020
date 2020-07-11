using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This acts as a container for a set of evaluation criteria. If an object has one of these it's assumed
 * that it should be evaluated for 'correctness' in some respect.
 * - Zack
 */
public class EvaluationCandidateComponent : MonoBehaviour
{
    public List<EvaluationCriteria> criteria;

    void Start()
    {
        if (criteria == null || criteria.Count <= 0)
        {
            Debug.LogWarningFormat("Object {0} should be evaluated but has no criteria.", this.name);
        }
    }
}
