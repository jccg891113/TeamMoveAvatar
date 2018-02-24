using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>移动副骨骼</para>
/// <para>用于跟随主骨骼而设计，副骨骼的移动依托对应主骨骼，因而可实现相对主骨骼的移动功能（超越或滞后于主骨骼）</para>
/// </summary>
public class MoveViceBone
{
	public int baseBoneId;
	public Vector2 pos;
	public Vector2 dir;

	public void SetSpeedMax ()
	{
	}

	public void SetBaseValue (int baseBoneId, MoveBone targetBaseBone)
	{
		this.baseBoneId = baseBoneId;
		this.pos = targetBaseBone.pos;
		this.dir = targetBaseBone.dir;
	}
}