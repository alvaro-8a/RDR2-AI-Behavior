using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class TaskRoam : Node
{
	public event EventHandler OnAIMoving;
	public event EventHandler OnAIStop;

	// AI Object Transform
	private Transform transform;
	private NavMeshAgent navMeshAgent;
	private float roamingRangeX = 15f;
	private float roamingRangeZ = 15f;
	// Time left to move to a new position
	private float roamingTimer = 0;
	// Time to wait to move to another position
	private float roamingTimerMax = 1f;

	// Since it's not a MonoBehaviour we have to pass it the transform and navMeshAgent
	public TaskRoam(Transform transform, NavMeshAgent navMeshAgent, EventHandler OnAIMoving, EventHandler OnAIStop)
	{
		this.transform = transform;
		this.navMeshAgent = navMeshAgent;
		this.OnAIMoving = OnAIMoving;
		this.OnAIStop = OnAIStop;
	}

	public override NodeState Evaluate()
	{
		if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
		{
			roamingTimer -= Time.deltaTime;
			if (roamingTimer <= 0)
			{
				// Choose a new position to move
				MoveToNewPosition();

				// Set the waiting timer for when it reaches the position 
				roamingTimer = roamingTimerMax;
			}
			else
			{
				// Invoke event to stop the walk animation
				OnAIStop?.Invoke(this, EventArgs.Empty);
			}
		}

		state = NodeState.RUNNING;
		return state;
	}

	private void MoveToNewPosition()
	{
		Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-roamingRangeX, roamingRangeX), 0f, UnityEngine.Random.Range(-roamingRangeZ, roamingRangeZ));
		Vector3 destination = transform.position + randomOffset;

		// Set new destination to the navMeshAgent
		navMeshAgent.SetDestination(destination);

		// Invoke event to start the walk animation
		OnAIMoving?.Invoke(this, EventArgs.Empty);
	}
}