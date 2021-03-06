//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Holds useful static functions used throughout the project
/// </summary>
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
	/// Converts a focal length value to a Field of View Value
	/// </summary>
	/// <param name="focalLength"></param>
	/// <param name="sensorSize"></param>
	/// <returns>returns a float value that converts a Focal Length to POV</returns>
	public static float FocalLengthToVerticalFOV(float focalLength, float sensorSize)
	{
		if (focalLength < 0.001f)
			return 180f;
		return Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorSize * 0.5f / focalLength);
	}

	/// <summary>
	/// Modifies a float value while clamping it between two values
	/// </summary>
	/// <param name="increaseAmount"></param>
	/// <param name="maxAmount"></param>
	/// <param name="minAmount"></param>
	/// <returns></returns>
	public static float ModifyFloatWithLimit(float increaseAmount, float maxAmount, float minAmount = 0)
	{
		return Mathf.Clamp(increaseAmount, minAmount, maxAmount);
	}

}
