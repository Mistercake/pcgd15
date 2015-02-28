using UnityEngine;
using System.Collections;

public class VentLaser : MonoBehaviour {

    public float speed;
    public Vector3[] offsets;
    int i = 1;
    Transform laser;
    float pos = 0f;

    Vector3 startPos;
    Vector3 from;
    Vector3 to;

	// Use this for initialization
	void Start () {
        laser = transform.Find("Laser");
        startPos = transform.position;
        from = startPos;
        to = startPos + offsets[0];


        Vector3[] corners = new Vector3[offsets.Length+1];
        corners[0] = Vector3.zero;
        offsets.CopyTo(corners, 1);
        offsets = corners;
	}
	
	// Update is called once per frame
	void Update () {
        if (pos >= 1f)
        {
            from = to;
            i++;
            if (i >= offsets.Length)
            {
                i = 0;
            }
            to = startPos + offsets[i];
            pos = 0f;
        }
        transform.position = Vector3.Lerp(from, to, pos);
        pos += Time.deltaTime*speed;
        pos = Mathf.Clamp(pos,0,1);
	}
}
