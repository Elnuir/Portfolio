using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchTargetProvider : TargetProviderBase
{
    public enum PriorityD
    {
        NearestWithDistance,
        NearestWithAngle,
        FarthestWithDistance,
        FarthestWithAngle,
        Any
    }

    public TargetsPool Searcher;

    public string[] Tags;
    public float MaxDistance = 10.0f;
    public float MinDistance = 0.0f;
    public float MaxAngle = 90.0f;
    public Transform CalcDistanceFrom;

    public PriorityD Priority;

    private List<GameObject> _candidatesBuffer = new List<GameObject>();

    void Start()
    {
        if (CalcDistanceFrom == null)
            CalcDistanceFrom = transform;
    }

    private float GetAngleTo(GameObject target)
    {
        Vector2 myDirection = transform.right;
        float myAngle = Mathf.Atan2(myDirection.y, myDirection.x) * Mathf.Rad2Deg;
        
        Vector2 targetDirection = target.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        return Observer.GetAngleDelta(myAngle, targetAngle);
    }

    private float GetDistanceTo(GameObject target)
    {
        return Vector2.Distance(target.transform.position, CalcDistanceFrom.position);
    }

    private IEnumerable<GameObject> GetSortedGameObjects(List<GameObject> objects)
    {
        bool Filter(GameObject o)
        {
            float distance = GetDistanceTo(o);
            return distance <= MaxDistance && distance >= MinDistance && GetAngleTo(o) <= MaxAngle;
        }

        switch (Priority)
        {
            case PriorityD.Any:
                return objects.Where(Filter);

            case PriorityD.NearestWithDistance:
                return objects.Where(Filter).OrderBy(GetDistanceTo);

            case PriorityD.NearestWithAngle:
                return objects.Where(Filter).OrderBy(GetAngleTo);

            case PriorityD.FarthestWithDistance:
                return objects.Where(Filter).OrderByDescending(GetDistanceTo);

            case PriorityD.FarthestWithAngle:
                return objects.Where(Filter).OrderByDescending(GetAngleTo);
            
            default:
                throw new ArgumentException($"Priority {Priority} is not supported");
        }
    }

    public override Transform GetTarget()
    {
        _candidatesBuffer.Clear();
        
        foreach (var targetTag in Tags)
        {
            var candidate = GetSortedGameObjects(Searcher.GetObjectsByTagNotNull(targetTag)).FirstOrDefault();

            if (candidate != null)
                _candidatesBuffer.Add(candidate);
        }

        return GetSortedGameObjects(_candidatesBuffer).FirstOrDefault()?.transform;
    }
}