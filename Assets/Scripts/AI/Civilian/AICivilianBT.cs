using System;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.AI;

public class AICivilianBT : Tree
{
	// Shared data of the tree
	public event EventHandler OnAIMoving;
	public event EventHandler OnAIStop;
	public event EventHandler OnAIEscape;

	private NavMeshAgent navMeshAgent;
	private float fovRange = 10f;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Sequence(new List<Node>
			{
				new CheckPlayerInFOVRange(transform, fovRange),
				new CheckPlayerIsAiming(),
				new TaskRunAway(transform, navMeshAgent, OnAIEscape)
			}),
			new TaskRoam(transform, navMeshAgent, OnAIMoving, OnAIStop),
		});

		return root;
	}
}