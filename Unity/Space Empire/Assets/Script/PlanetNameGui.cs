using UnityEngine;
using System.Collections;

public class PlanetNameGui : MonoBehaviour {

	public GameObject target;
	public bool onTop;
	private float _dist;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		_dist = Camera.main.GetComponent<Orbit> ()._distance;
		this.transform.LookAt (2* this.transform.position - Camera.main.transform.position);
		this.transform.position = target.transform.position;
		if (onTop) {

			if (_dist/20 >= 1)
				this.transform.position += new Vector3 (0, _dist/20, 0);
			else
				this.transform.position += new Vector3 (0, 0.75f, 0);
			this.GetComponent<TextMesh> ().text = target.GetComponent<Properties>().planetName;
			//this.GetComponent<TextMesh> ().characterSize = _dist/15;
		}
		this.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
	}
}
