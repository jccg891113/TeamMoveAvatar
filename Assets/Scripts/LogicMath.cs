using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogicMath
{
	#region Rotate

	public static Vector2 GetRotateNewPos (Vector2 oldPos, double sin, double cos)
	{
		double x = oldPos.x * cos - oldPos.y * sin;
		double y = oldPos.x * sin + oldPos.y * cos;
		return new Vector2 ((float) x, (float) y);
	}

	#endregion
}