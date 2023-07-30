using System;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskAttack : Node
{
	public event EventHandler OnAIAttack;

	// ShootProjectile script
	private ShootProjectile shootProjectile;
	private NavMeshAgent navMeshAgent;


	public TaskAttack(NavMeshAgent navMeshAgent, ShootProjectile shootProjectile, EventHandler OnAIAttack)
	{
		this.navMeshAgent = navMeshAgent;
		this.shootProjectile = shootProjectile;
		this.OnAIAttack = OnAIAttack;
	}

	public override NodeState Evaluate()
	{
		navMeshAgent.ResetPath();

		// Invoke event to set aim animation
		OnAIAttack?.Invoke(this, EventArgs.Empty);

		state = NodeState.RUNNING;
		return state;
	}
}