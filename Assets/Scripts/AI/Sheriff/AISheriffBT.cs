
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

	private NavMeshAgent navMeshAgent;
	private float fovRange = 5f;

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
				new TaskFollow(navMeshAgent, OnAIRun),
			}),
			new TaskRoam(transform, navMeshAgent, OnAIMoving, OnAIStop),
		});

		return root;
	}
}