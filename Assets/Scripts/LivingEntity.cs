using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class LivingEntity : NetworkBehaviour, IDamagable {

	public float startingHealth = 5;

	[SyncVar]
	protected float health;
	protected bool dead;

	protected virtual void Start() {
		health = startingHealth;
	}

	public void TakeHit(float damage, RaycastHit hit) {
		health -= damage;

		if (health <= 0 && !dead) {
			Die();
		}
	}

	void Die() {
		dead = true;
		GameObject.Destroy(gameObject);
	}
}
