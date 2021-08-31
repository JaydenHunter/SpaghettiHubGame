//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	/// <summary>
	/// Remaps a value from one range to another
	/// </summary>
	/// <param name="value"></param>
	/// <param name="from1"></param>
	/// <param name="to1"></param>
	/// <param name="from2"></param>
	/// <param name="to2"></param>
	/// <returns></returns>
    public static float RemapValues(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	/// <summary>
	/// Convers a focal length value to a Field of View Value
	/// </summary>
	/// <param name="focalLength"></param>
	/// <param name="sensorSize"></param>
	/// <returns></returns>
	public static float FocalLengthToVerticalFOV(float focalLength, float sensorSize)
	{
		if (focalLength < 0.001f)
			return 180f;
		return Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorSize * 0.5f / focalLength);
	}

	public static float ModifyFloatWithLimit(float increaseAmount,float maxAmount, float minAmount = 0)
	{
		return Mathf.Clamp(increaseAmount, minAmount, maxAmount);
	}

}
