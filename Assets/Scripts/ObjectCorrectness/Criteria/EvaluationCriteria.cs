using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Abstract representation of a 'Criteria' that an evaluator might judge an object on.
 * To make varients on pre-existing criteria, make a new instance of that pre-existing criteria.
 * To make new criteria, extend this class. You'll also want to make an evaluator for it and
 * add that evaluator to the ObjectCorrectnessTracker.
 * -Zack
 */

public abstract class EvaluationCriteria : ScriptableObject
{
    public string name;
    public string description;
}
