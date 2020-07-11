using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * This is the game object that actually looks at and evaluates all of the components in the scene for correctness.
 * It has a set of children that represent each type of evaluation to perform. It auto-picks up active children with
 * evaluators, so they don't need to be added manually here. 
 * - Zack
 */

public class ObjectCorrectnessTracker : MonoBehaviour
{

    private List<EvaluationCandidateComponent> evaluationCandidates;
    private List<ObjectEvaluator> evaluators;

    [SerializeField] private int currentScore = 0;
    
    void Start()
    {
        evaluators = GetComponentsInChildren<ObjectEvaluator>().ToList();
        evaluationCandidates = FindObjectsOfType<EvaluationCandidateComponent>().ToList();
    }

    /**
     * TODO: Set this to not evaluate every frame once evaluation is proven to work.
     */
    void Update()
    {
        EvaluateAll();
    }

    public void EvaluateAll()
    {
        currentScore = 0;
        foreach (EvaluationCandidateComponent evaluatableObject in evaluationCandidates)
        {
            int objectScore = 0;
            foreach (ObjectEvaluator evaluator in evaluators)
            {
                if (ShouldBeEvaluated(evaluatableObject, evaluator))
                {
                    objectScore += evaluator.Evaluate(evaluatableObject);
                }
            }
            Debug.LogFormat("Scored {0} as {1}", evaluatableObject.gameObject.name, objectScore);
            currentScore += objectScore;
        }
    }

    public bool ShouldBeEvaluated(EvaluationCandidateComponent evaluationCandidate, ObjectEvaluator evaluator)
    {
        Type criteriaType = evaluator.Criteria;
        foreach (EvaluationCriteria criteria in evaluationCandidate.criteria)
        {
            if (criteriaType == criteria.GetType())
            {
                return true;
            }
        }
        return false;
    }
}
