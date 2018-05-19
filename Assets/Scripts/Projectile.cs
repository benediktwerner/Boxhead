using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	float speed = 10;
	float damage = 1;

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}

	void Update () {
		float distToMove = speed * Time.deltaTime;
		CheckCollisions(distToMove);

		transform.Translate(Vector3.forward * distToMove);
	}

	void CheckCollisions(float distToMove) {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, distToMove, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHit(hit);
		}
	}

	void OnHit(RaycastHit hit) {
		IDamagable damagableObject = hit.collider.GetComponent<IDamagable>();
		if (damagableObject != null)
			damagableObject.TakeHit(damage, hit);
		GameObject.Destroy(gameObject);
	}
}
