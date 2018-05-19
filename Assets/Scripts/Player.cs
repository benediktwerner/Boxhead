using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof(Rigidbody))]
public class Player : LivingEntity {

	public float moveSpeed = 5;
	public Transform weaponHold;
	public GameObject playerConnection;

	Vector3 velocity;
	Rigidbody myRigidbody;
	GameObject equippedGun;

	protected override void Start () {
		base.Start();
		myRigidbody = GetComponent<Rigidbody>();
	}

	public override void OnStartAuthority() {
		CmdEquipGun(0);
	}
	
	void Update () {
		if (hasAuthority) {
			// Movement input
			velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * moveSpeed;

			// Look input
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			float rayDistance;

			if (groundPlane.Raycast(ray, out rayDistance)) {
				Vector3 point = ray.GetPoint(rayDistance);
				Vector3 lookPoint = new Vector3(point.x, transform.position.y, point.z);
				transform.LookAt(lookPoint);
			}
		}
	}

	void FixedUpdate() {
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
	}

	[Command]
	void CmdEquipGun(int gunId) {
		if (0 <= gunId && gunId < GunManager.Instance.Guns.Length) {
			if (equippedGun != null)
				Destroy(equippedGun);
			
			GameObject gun = Instantiate(GunManager.Instance.Guns[gunId].gameObject, weaponHold.position, weaponHold.rotation);
			gun.transform.parent = weaponHold;
			equippedGun = gun;
			NetworkServer.SpawnWithClientAuthority(gun, playerConnection);
			RpcSyncGun(gun);
		}
	}

	[ClientRpc]
	void RpcSyncGun(GameObject gun) {
		gun.transform.parent = weaponHold;
		gun.transform.position = weaponHold.position;
		gun.transform.rotation = weaponHold.rotation;
	}
}
