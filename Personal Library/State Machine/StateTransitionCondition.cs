using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateTransitionCondition : MonoBehaviour
{
    public abstract bool IsMet();
}

public static class ConditionExtensions
{
    public static bool AreMet(this IEnumerable<StateTransitionCondition> conditions)
    {
        return conditions.All(x => x.IsMet());
    }
}