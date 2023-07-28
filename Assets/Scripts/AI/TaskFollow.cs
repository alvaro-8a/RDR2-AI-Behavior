using System;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskFollow : Node
{
	public event EventHandler OnAIRun;

	private NavMeshAgent navMeshAgent;

	public TaskFollow(NavMeshAgent navMeshAgent, EventHandler OnAIRun)
	{
		this.navMeshAgent = navMeshAgent;
		this.OnAIRun = OnAIRun;
	}

	public override NodeState Evaluate()
	{
		navMeshAgent.SetDestination(ThirdPersonShooterController.Instance.transform.position);

		// Invoke event to start the walk animation
		OnAIRun?.Invoke(this, EventArgs.Empty);

		state = NodeState.RUNNING;
		return state;
	}
}