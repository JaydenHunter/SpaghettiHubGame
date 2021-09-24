//Written by Jayden Hunter
using Cinemachine;
using UnityEngine;

/// <summary>
/// Handles the camera to always look at the cat and zoom in based on distance
/// </summary>
public class CatCameraHandlerMain : MonoBehaviour
{
	public float maxFocalLength = 60;
	public float minFocalLength = 20;

	private CinemachineVirtualCamera virtCam = null; //Reference to the virtual camera
	private Transform lookAtTransform = null; //Transform to look at

	private void Awake()
	{
		virtCam = GetComponent<CinemachineVirtualCamera>();
		lookAtTransform = virtCam.LookAt;
	}

	private void Update()
	{
		//0-28    -> 20-60
		//Get distance to object we're tracking
		float distToLookAtObject = Vector3.Distance(transform.position, lookAtTransform.position);

		//Remap distance to target
		float remappedValue = Utils.RemapValues(distToLookAtObject, 0, 28, minFocalLength, maxFocalLength);
		//Set camera's field of view based on the distance
		virtCam.m_Lens.FieldOfView = Utils.FocalLengthToVerticalFOV(remappedValue, virtCam.m_Lens.SensorSize.y);
	}

}
