using UnityEngine;
using System.Collections;


public class NavigateToState : ActiveState {

	private Animator animator;
	private Vector3 targetLocation;
	private NavMeshAgent navAgent;

	public NavigateToState(CatAI cat, Vector3 targetLocation) : base(cat) {
		Debug.Log("Navigating to " + targetLocation + "!");

		navAgent = cat.GetComponent<NavMeshAgent>();

		navAgent.destination = targetLocation;
	}

	public override void Update() {
		base.Update();

		if (this.cat.getBrain().getPriority().eventType.Equals(CatEvent.FINISHED)) {
			Vector3 destination = new Vector3(Random.Range(-7.0f, 7.0f), 0, Random.Range(-7.0f, 7.0f));

			this.cat.getBrain().setActiveState(new NavigateToState(cat, destination));
		}

		if (!navAgent.pathPending) {
			if (navAgent.remainingDistance <= navAgent.stoppingDistance) {
				if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f) {
					cat.getBrain().addEvent(new AIEvent(CatEvent.FINISHED, 1));
				}
			}
		}
	}
}
