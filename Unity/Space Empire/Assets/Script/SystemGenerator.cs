using UnityEngine;
using System.Collections;

public class SystemGenerator : MonoBehaviour {

	public Properties Planets;
	public float timeScale;
	private Object[] textures;
	// Use this for initialization
	void Start () {
		textures = Resources.LoadAll ("PlanetMaps");
		timeScale = 1;
		for (int i = 2; i <= 10; i++) {
			GameObject go = (GameObject)Instantiate(Resources.Load("Planet"));
			GameObject pn = (GameObject)Instantiate(Resources.Load("PlanetName"));
			GameObject pp = (GameObject)Instantiate(Resources.Load("PlanetPoint"));
			pn.transform.parent = go.transform;
			pn.GetComponent<PlanetNameGui>().target = go;
			pp.transform.parent = go.transform;
			pp.GetComponent<PlanetNameGui>().target = go;
			setPlanetAttributes(go, i);
			drawPathOf(go);
		}
	}

	void setPlanetAttributes (GameObject Planet, int planetIndex) {
		Properties go_prop = Planet.GetComponent<Properties>();
		Renderer go_render = Planet.GetComponent<Renderer>();

		go_prop.planetName = "Planet " + planetIndex;
		go_render.material.mainTexture = (Texture)Resources.Load("PlanetMaps/" + textures[planetIndex].name);
		Planet.transform.position = new Vector3(planetIndex*Random.Range(5f, 6.5f), 0, 0);
		go_prop.distanceFromSun = Planet.transform.position.x;
		go_prop.sun = GameObject.Find("Sun").GetComponent<Transform>();
		go_prop.revolutionPeriod = planetIndex* Random.Range(60, 100);
		go_prop.excentricity = Random.Range (-0.2f, 0.2f);
		go_prop.angle = Random.Range (0, 360);
	}

	void drawPathOf (GameObject Planet) {
		Properties go_prop = Planet.GetComponent<Properties>();
		LineRenderer lr = Planet.GetComponent<LineRenderer>();
		lr.SetVertexCount(361);
		for (int y = 0; y <= 360; y ++) {
			lr.SetPosition(y, new Vector3(Mathf.Cos(Mathf.Deg2Rad * y + go_prop.excentricity) * go_prop.distanceFromSun,
			                              Mathf.Cos(Mathf.Deg2Rad * y)*go_prop.excentricity/1.5f * go_prop.distanceFromSun, 
			                              Mathf.Sin(Mathf.Deg2Rad * y) * go_prop.distanceFromSun));
		}

	}
	// Update is called once per frame
	void Update () {
		if (timeScale >= 0) {
			Time.timeScale = timeScale;
		}
	}

	void createPlanet () {

	}
}
