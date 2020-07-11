using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class SurfaceEvaluator : ObjectEvaluator
{
    public override Type Criteria => typeof(RestingSurfaceCriteria);

    public override int Evaluate(EvaluationCandidateComponent evaluationCandidate, EvaluationCriteria criteria)
    {
        RestingSurfaceCriteria rsc = (RestingSurfaceCriteria) criteria;
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = new Ray(evaluationCandidate.transform.position, Vector3.down);
        // Debug.LogFormat("Sending out ray from {0} in the direction of {1}", evaluationCandidate.transform.position, Vector3.down);
        if (!Physics.Raycast(ray, out hitInfo, 1f))
        {
            return rsc.defaultCost;
        }


        SurfaceTypeComponent foundSurface = hitInfo.collider.GetComponent<SurfaceTypeComponent>();
        if (foundSurface == null || foundSurface.types.Length <= 0)
        {
            return rsc.defaultCost;
        }

        int currentCost = 0;

        foreach (SurfaceType surfaceType in foundSurface.types)
        {
            foreach (RestingSurfaceCriteria.SurfaceCost surfaceCost in rsc.surfaces)
            {
                if (surfaceCost.surface == surfaceType)
                {
                    // Debug.LogFormat("Object {0} was found to hit surface type {1} from object {2}", evaluationCandidate.name, surfaceType.name, hitInfo.collider.gameObject.name);
                    currentCost += surfaceCost.cost;
                }
            }
        }
        return currentCost;
    }
}
