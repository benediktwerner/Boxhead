using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {
	const float refreshRate = 0.25f;

	NavMeshAgent pathfinder;
	Transform target;

	protected override void Start () {
		base.Start();
		pathfinder = GetComponent<NavMeshAgent>();
		StartCoroutine(UpdatePath());
	}
	
	IEnumerator UpdatePath() {
		while (true) {
			if (target == null)
				FindTarget();
			if (target != null) {
				Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
				if (dead)
					yield break;
				pathfinder.SetDestination(targetPosition);
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}

	void FindTarget() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		float minDistance = float.PositiveInfinity;
		foreach (GameObject p in players) {
			float dist = Vector3.Distance(p.transform.position, transform.position);
			if (target == null || dist < minDistance) {
				target = p.transform;
				minDistance = dist;
			}
		}
	}
}
