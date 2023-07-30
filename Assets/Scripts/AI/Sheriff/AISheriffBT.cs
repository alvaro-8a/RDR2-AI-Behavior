
using System;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.AI;

public class AISheriffBT : Tree
{
	// Tree shared data
	public event EventHandler OnAIMoving;
	public event EventHandler OnAIStop;
	public event EventHandler OnAIRun;
	public event EventHandler OnAIAttack;

	private ShootProjectile shootProjectile;
	private NavMeshAgent navMeshAgent;
	private float fovRange = 10f;
	private float attackRange = 6f;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		shootProjectile = GetComponent<ShootProjectile>();
	}

	protected override Node SetupTree()
	{
		// Tree structure
		/*
											SELECTOR
												|
					SEQUENCE				SEQUENCE				ROAM	
						|						|						
			ATTACKRANGE		ATTACK		FOVRANGE	FOLLOW

		*/
		Node root = new Selector(new List<Node>
		{
			new Sequence(new List<Node>
			{
				new CheckPlayerInFOVRange(transform, attackRange),
				new TaskAttack(navMeshAgent,shootProjectile, OnAIAttack),
			}),
			new Sequence(new List<Node>
			{
				new CheckPlayerInFOVRange(transform, fovRange),
				new TaskFollow(navMeshAgent, OnAIRun),
			}),
			new TaskRoam(transform, navMeshAgent, OnAIMoving, OnAIStop),
		});

		return root;
	}
}