using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	public Transform _target;

	public float _distance = 60.0f;

	public float _zoomStep = 0.5f;

	public float _xSpeed = 0.5f;
	public float _ySpeed = 0.5f;
	
	private float _x = 0.0f;
	private float _y = 0.0f;
 
	private Vector3 _distanceVector;
	private Vector3 _initialVector;

	private int _viewState;

	public GameObject EmpireUI;
	public GameObject EmpireBtn;
	public GameObject PlanetUI;
	public GameObject ResourcesUI;

	void Start ()
	{
		EmpireUI = GameObject.Find ("EmpireView");
		PlanetUI = GameObject.Find ("PlanetView");
		EmpireBtn = GameObject.Find ("Empire");
		EmpireUI.SetActive(false);
		PlanetUI.SetActive(false);
		this.setViewState (1);
		_initialVector = new Vector3(0.0f,0.0f,-_distance);
		_distanceVector = _initialVector;
		_viewState = 1;
		Vector2 angles = this.transform.localEulerAngles;
		_x = angles.x;
		_y = angles.y;
		
		this.Rotate(_x, _y);
		
	}

	void LateUpdate()
	{
		if (EmpireBtn.GetComponent<InstantGuiButton> ().pressed == true) {
			this.setViewState (1);
			EmpireBtn.GetComponent<InstantGuiButton> ().pressed = false;
		}
		Vector3 heading = GameObject.Find("SunLight").transform.position - Camera.main.transform.position;
		float dist = Vector3.Dot (heading, Camera.main.transform.forward);
		GameObject.Find("SunLight").GetComponent<LensFlare>().brightness = 15 / dist;
		if ( _target )
		{
			this.RotateControls();
			this.Zoom();
		}
	}

	void RotateControls()
	{
		this.Rotate(_x,_y);
		if (Input.touchCount == 1 || Input.GetButton("Fire1")) {
			RaycastHit hit = new RaycastHit ();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name != "Sun")
                {
                    _target = hit.collider.gameObject.GetComponent<Transform>();
                    _distance = 2;
                    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
					this.setViewState(2);
                    this.Rotate(_x, _y);

                }
            }
            else {
                Touch touchZero = Input.GetTouch(0);

                _x -= touchZero.deltaPosition.x;
                _y += touchZero.deltaPosition.y;

                this.Rotate(_x, _y);
            }
		}
	}

	void Rotate( float x, float y )
	{
		Quaternion rotation = Quaternion.Euler(y,x,0.0f);

		Vector3 position = rotation * _distanceVector + _target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	void Zoom()
	{
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            LensFlare sunLight = GameObject.Find("SunLight").GetComponent<LensFlare>();

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (_distance + deltaMagnitudeDiff * _zoomStep >= 10 && _distance + deltaMagnitudeDiff * _zoomStep <= 100)
				_distance += deltaMagnitudeDiff * _zoomStep;
            sunLight.brightness += 1 / _distance * (_zoomStep / 50);
            _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
        }
		
	}

	public void setViewState (int newState)
	{
		GameObject sun = GameObject.Find("Sun");
		switch (newState)
		{
			case 1:
			{
				EmpireUI.SetActive(true);
				PlanetUI.SetActive(false);
				_target = sun.GetComponent<Transform>();
				_distance = 60.0f;
				_distanceVector = _initialVector;
				_viewState = newState;
				this.Rotate(_x, _y);
				break;
			}
			case 2:
			{
				EmpireUI.SetActive(false);
				PlanetUI.SetActive(true);
				_viewState = newState;
				break;
			}
		}
	}
}
