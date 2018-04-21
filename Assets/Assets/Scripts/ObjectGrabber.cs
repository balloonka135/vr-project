using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class ObjectGrabber : MonoBehaviour {

	public ModifyTerrain terrainModifier;
	public bool isColliding = false; // destroy terrain only when controller grab the obj


	void OnCollisionEnter(Collision collision) {
		// if (isColliding) { 
			foreach (ContactPoint contact in collision.contacts) {
				// contact.point is a world space position where the objects collided
				terrainModifier.LowerTerrain (contact.point);
				Debug.Log ("Take Damage");
			}
		// }
	}

//	void Update () {
//		if (GetComponent<VRTK_InteractGrab>().GetGrabbedObject != null) {
//			var controllerEvents = GetComponent<VRTK_ControllerEvents>();
//			if (controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.Trigger_Press)) {
//				isColliding = true;
//			}
//		}
//	}

	//	void Update() {
	//
	//		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
	//		//transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
	//
	//	}
}
