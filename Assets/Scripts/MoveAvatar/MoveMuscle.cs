using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>移动肌肉</para>
/// <para>绑定在副骨骼之上，相对副骨骼位置实现偏移。</para>
/// <para>即根据对应副骨骼方向与位置可唯一确定肌肉位置与方向。</para>
/// <para>肌肉方向与副骨骼方向一致</para>
/// </summary>
public class MoveMuscle
{
	public int viceBoneId;
	/// <summary>
	/// -1-右，1-左，0-前
	/// </summary>
	public double sinWithBoneDir;
	public double cosWithBoneDir;
	public float deltaLength;

	public Vector2 pos;
	public Vector2 dir;

	/// <summary>
	/// Sets the base value.
	/// </summary>
	/// <param name="sinWithBoneDir">-1-右，1-左，0-前.</param>
	public void SetBaseValue (int civeBoneId, double sinWithBoneDir, double cosWithBoneDir, float deltaLength)
	{
		this.viceBoneId = civeBoneId;
		this.sinWithBoneDir = sinWithBoneDir;
		this.cosWithBoneDir = cosWithBoneDir;
		this.deltaLength = deltaLength;
	}
}