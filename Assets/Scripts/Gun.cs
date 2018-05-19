using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour {

	public Transform muzzle;
	public GameObject projectile;
	public float timeBetweenShots = 0.1f;
	public float muzzleVelocity = 35;

	float nextShotTime;

	void Update() {
		if (hasAuthority && Input.GetButton("Fire1")) {
			CmdShoot();
		}
	}

	[Command]
	public void CmdShoot() {
		if (Time.time > nextShotTime) {
			GameObject newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
			var projectileComponent = newProjectile.GetComponent<Projectile>();
			projectileComponent.SetSpeed(muzzleVelocity);
			NetworkServer.Spawn(newProjectile);

			nextShotTime = Time.time + timeBetweenShots;
		}
	}
}
