using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour {

	public Gun[] Guns;

	public static GunManager Instance { get; private set; }

	void Start() {
		Instance = this;
	}
}
