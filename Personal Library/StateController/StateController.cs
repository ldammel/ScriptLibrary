using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public Transform eyes;

    public List<Transform> wayPointList;
    
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public int nextWayPoint;

    private bool _aiActive;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetupAI(bool aiActivation, List<Transform> wayPoints)
    {
        wayPointList = wayPoints;
        _aiActive = aiActivation;
    }

    private void Update()
    {
        if (!_aiActive) return;
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
}
