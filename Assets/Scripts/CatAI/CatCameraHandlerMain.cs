//Written by Jayden Hunter
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCameraHandlerMain : MonoBehaviour
{
	public float maxFocalLength = 60;
	public float minFocalLength = 20;

	private CinemachineVirtualCamera virtCam = null;
	private Transform lookAtTransform = null;
	private void Awake()
	{
		virtCam = GetComponent<CinemachineVirtualCamera>();
		lookAtTransform = virtCam.LookAt;
	}

	private void Update()
	{
		//0-28    -> 20-60
		float distToLookAtObject = Vector3.Distance(transform.position, lookAtTransform.position);

		float remappedValue = Utils.RemapValues(distToLookAtObject, 0, 28, minFocalLength, maxFocalLength);
		virtCam.m_Lens.FieldOfView = Utils.FocalLengthToVerticalFOV(remappedValue, virtCam.m_Lens.SensorSize.y);
	}

}
