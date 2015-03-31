using UnityEngine;
using System.Collections;

public class Properties : MonoBehaviour {

	public string planetName;
	public string planetOwner;

	public float velocity;
	public float revolutionPeriod;
	public float distanceFromSun;
	public Transform sun;
	public float excentricity;
	public float angle;

	private float silicium_factor;
	private float metal_factor;
	private float water_factor;
	private float energy_factor;

	// Use this for initialization
	void Start () {
		distanceFromSun = transform.position.x;
	}

	void setResourcesFactor (float distance) {
		silicium_factor = distance / 100;
		metal_factor = distance / 100;
		water_factor = distance / 100;
		energy_factor = distance / 100;
	}

	// Update is called once per frame
	void Update () {
		velocity = (2 * Mathf.PI) / (60 * (revolutionPeriod / 365));
		angle += velocity * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Cos(angle + excentricity) * distanceFromSun + sun.position.x,
		                                  Mathf.Cos(angle)* excentricity/1.5f * distanceFromSun, 
		                                  Mathf.Sin(angle) * distanceFromSun + sun.position.z);
	}
}
