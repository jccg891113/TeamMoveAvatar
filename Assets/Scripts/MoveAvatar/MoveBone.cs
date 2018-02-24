using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>移动主骨骼</para>
/// <para>主骨骼仅保存位置与方向信息，以及与前一主骨骼的间距。</para>
/// </summary>
public class MoveBone
{
	public Vector2 pos;
	public Vector2 dir;
	public float lengthWithBefore;
}