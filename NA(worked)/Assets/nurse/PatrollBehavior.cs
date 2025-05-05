using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrollBehavior : StateMachineBehaviour
{
    [Header("���������")]
    public float moveSpeed = 3.5f;
    public float pointReachDistance = 0.5f;
    public string pointsTag = "Points";

    private NavMeshAgent agent;
    private List<Transform> patrolPoints = new List<Transform>();
    private int currentPointIndex = 0;
    private bool isMovingToPoint = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = pointReachDistance;
        agent.autoBraking = false;

        // ������ ����������������� �����
        patrolPoints.Clear();
        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag(pointsTag);
        foreach (var point in pointObjects)
        {
            if (point != null) patrolPoints.Add(point.transform);
        }

        if (patrolPoints.Count > 1) // ����� ������� 2 �����
        {
            currentPointIndex = 0;
            MoveToNextPoint();
        }
        else
        {
            Debug.LogError($"����� ������� 2 ����� � ����� '{pointsTag}'!");
        }
    }

    private void MoveToNextPoint()
    {
        isMovingToPoint = true;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
        Debug.Log($"�������� � ����� {currentPointIndex} ({patrolPoints[currentPointIndex].position})");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (patrolPoints.Count < 2 || !isMovingToPoint) return;

        // ����� ������ �������� ���������� �����
        if (agent.remainingDistance <= agent.stoppingDistance &&
            !agent.pathPending &&
            agent.velocity.sqrMagnitude == 0f)
        {
            Debug.Log($"����� {currentPointIndex} ����������");
            isMovingToPoint = false;
            MoveToNextPoint();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.ResetPath();
        }
    }

    // ������������
    private void OnDrawGizmosSelected()
    {
        if (agent != null && patrolPoints.Count > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(agent.transform.position, patrolPoints[currentPointIndex].position);
            Gizmos.DrawSphere(patrolPoints[currentPointIndex].position, 0.5f);
        }
    }
}