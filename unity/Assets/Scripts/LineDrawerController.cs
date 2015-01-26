using UnityEngine;
using System.Collections;

public class LineDrawerController : MonoBehaviour {

	public Material linerendererMat;
	public GameObject targetToFollow;

	private LineRenderer linerenderer;
	private int lineRendererVertexCount; // keeps rising as the player object is moving
	private int latestIndex; //tracks the last added index in the LineRenderer
	private Vector3 lastPosition;

	void Start () {
		linerenderer = gameObject.AddComponent<LineRenderer>();
		lineRendererVertexCount = 0;
		linerenderer.SetVertexCount (lineRendererVertexCount);
		//linerenderer.material.SetColor("_TintColor", new Color(1, 1, 1, 1f));
		linerenderer.material = linerendererMat;//new Material(Shader.Find("Sprites/Diffuse"));
		linerenderer.SetWidth (0.03f, 0.03f);
		linerenderer.sortingOrder = 10;
		linerenderer.sortingLayerName = "Objects";
	}
	

	void Update () {
		transform.position = targetToFollow.transform.position;
		if (transform.position != lastPosition) { // we don't want the LineRenderer to add new vertices while still
			ManageLineRenderer ();
		}
		lastPosition = transform.position;
	}

	void ManageLineRenderer() {
		lineRendererVertexCount++;
		linerenderer.SetVertexCount (lineRendererVertexCount);
		linerenderer.SetPosition (latestIndex, transform.position);
		latestIndex++;
	}

	public void reset() {
		lineRendererVertexCount = 0;
		latestIndex = 0;
		targetToFollow.SetActive (true);
	}
}
