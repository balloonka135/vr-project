using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrain : MonoBehaviour {

	public Terrain terrain;
	public float strength = 0.005f;

	private int heightmapWidth;
	private int heightmapHeight;
	private float[,] heights;
	private TerrainData terrainData; 
	private float[,] heightMapBackup;
	public ParticleSystem dirt;

	// Use this for initialization
	void Start () {
		terrainData = terrain.terrainData;
		heightmapWidth = terrainData.heightmapWidth;
		heightmapHeight = terrainData.heightmapHeight;
		heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);


		// reset to flat
		if (Debug.isDebugBuild)
		{
			heightMapBackup = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
		} 
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // change to controller position

		// raise terrain
		if (Input.GetMouseButton(0)) {  // Controller.GetHairTriggerDown()
			if (Physics.Raycast(ray, out hit))
			{
				RaiseTerrain(hit.point);
			}
		}

		// lower terrain
		if (Input.GetMouseButton(1)) {
			if (Physics.Raycast(ray, out hit)) 
			{
				LowerTerrain(hit.point);

			}
		}
	}

	private void RaiseTerrain(Vector3 point)
	{
		int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
		int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

		float[,] modifiedHeights = new float[1, 1];
		float y = heights[mouseX, mouseZ];
		y += strength * Time.deltaTime;

		if (y > terrainData.size.y)
		{
			y = terrainData.size.y;
		}

		modifiedHeights[0,0] = y;
		heights[mouseX, mouseZ] = y;
		terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);
	}

	public void LowerTerrain(Vector3 point)
	{
		int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
		int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

		float[,] modifiedHeights = new float[1, 1];
		float y = heights[mouseX, mouseZ];
		y -= strength * Time.deltaTime;

		if (y < 0)
		{
			y = 0;
		}

		modifiedHeights[0,0] = y;
		heights[mouseX, mouseZ] = y;
		terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);

		EmitParticles (point);
	}

	// flatten
	void OnApplicationQuit()
	{
		if (Debug.isDebugBuild)
		{
			terrainData.SetHeights(0, 0, heightMapBackup);
		}
	}

	void EmitParticles(Vector3 point)
	{
		dirt.transform.localPosition = point;
		dirt.Play ();
	}

//	void Awake()
//	{
//		dirt.Stop ();
//	}
}

/* 
    using VRTK;

	public class Backpack : MonoBehaviour {

	[Header("Backpack Options", order = 1)]
	public GameObject spawnObject;

	private void OnTriggerStay(Collider collider) {
		VRTK_InteractGrab grabbingObject = (collider.gameObject.GetComponent<VRTK_InteractGrab>() ? collider.gameObject.GetComponent<VRTK_InteractGrab>() : collider.gameObject.GetComponentInParent<VRTK_InteractGrab>());
		if (CanGrab(grabbingObject)) {
			Debug.Log ("Spawning object");

			GameObject spawned = Instantiate(spawnObject);
			grabbingObject.GetComponent<VRTK_InteractTouch>().ForceTouch(spawned);
			grabbingObject.AttemptGrab();
		}
	}

	private bool CanGrab(VRTK_InteractGrab grabbingObject) {
		return (grabbingObject && grabbingObject.GetGrabbedObject() == null && grabbingObject.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
	}
}
*/

// from VRTK
/*
private SteamVR_TrackedObject trackedObj;

private SteamVR_Controller.Device Controller
{
	get { return SteamVR_Controller.Input((int)trackedObj.index); }
}

void Awake()
{
	trackedObj = GetComponent<SteamVR_TrackedObject>();
}
*/

// to grab object
// public GameObject originalObj;
// VRTK_InteractGrab myGrab = originalObj.GetComponent<VRTK_InteractGrab>();

/*
 * VRTK_ControllerEvents Controller;
 * Controller.OnTriggerPressed()
 * or Controller.OnTriggerPressed('a')
 * Controller.OnTriggerReleased()
 */
