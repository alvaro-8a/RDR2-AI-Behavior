using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using System;

public class TaskRunAway : Node
{
	public event EventHandler OnAIEscape;

	private Transform transform;
	private NavMeshAgent navMeshAgent;

	public TaskRunAway(Transform transform, NavMeshAgent navMeshAgent, EventHandler OnAIEscape)
	{
		this.transform = transform;
		this.navMeshAgent = navMeshAgent;
		this.OnAIEscape = OnAIEscape;
	}

	public override NodeState Evaluate()
	{
		// if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
		// {
		// Get opposite direction to the player
		Vector3 runDirection = transform.position - ThirdPersonShooterController.Instance.transform.position;

		Vector3 newDestination = transform.position + (runDirection * 2.5f);

		navMeshAgent.SetDestination(newDestination);

		OnAIEscape?.Invoke(this, EventArgs.Empty);
		// }

		state = NodeState.RUNNING;
		return state;
	}
}