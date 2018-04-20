using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallingCube : MonoBehaviour {

	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;
	public ModifyTerrain terrainModifier;

//	void OnCollisionEnter(Collision hit)
//	{
//		if(hit.gameObject.tag == ("Plane")) {
//			Debug.Log("Take Damage");
//		}
//	}

	// suggestion
	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			// contact.point is a world space position where the objects collided
			terrainModifier.LowerTerrain(contact.point);
			Debug.Log("Take Damage");
		}
	}

//	void Update() {
//
//		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
//		//transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
//
//	}

//	void OnMouseDown() {
//
//		renderer.enabled = false;
//
//	}
}

