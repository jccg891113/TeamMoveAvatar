using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>移动核心</para>
/// 
/// <para></para>
/// <para>内部集成移动的主骨骼、副骨骼、肌肉。</para>
/// <para>主骨骼成一字长蛇阵移动</para>
/// <para>副骨骼依托主骨骼移动。当前副骨骼移动模式为跟随，可根据需求定制副骨骼移动策略。</para>
/// <para>肌肉完全依托副骨骼，根据副骨骼方向与位置进行偏移求得具体位置与方向信息</para>
/// 
/// </summary>
public partial class MoveAvatar
{
	/// <summary>
	/// 阵型支持最大人数
	/// </summary>
	const int c_nMaxActorNum = 6;

	/// <summary>
	/// 主骨骼间距
	/// </summary>
	private float interval;

	public int firstBone;
	public int boneNum;
	public List<MoveBone> boneList;

	public int viceNum;
	public List<MoveViceBone> viceBoneList;

	public int muscleNum;
	public List<MoveMuscle> muscleList;

	public int centerPosBone1;
	public int centerPosBone2;

	public MoveAvatar ()
	{
		interval = 20;

		firstBone = 0;

		boneNum = c_nMaxActorNum;
		viceNum = c_nMaxActorNum;
		muscleNum = c_nMaxActorNum;

		boneList = new List<MoveBone> ();
		viceBoneList = new List<MoveViceBone> ();
		muscleList = new List<MoveMuscle> ();

		for (int i = 0; i < c_nMaxActorNum; i++) {
			boneList.Add (new MoveBone ());
			viceBoneList.Add (new MoveViceBone ());
			muscleList.Add (new MoveMuscle ());
		}
	}

	#region 移动更新骨骼、副骨骼、肌肉数据

	public void Update (float delta_s, float speed, Vector2 dir)
	{
		UpdateBone (delta_s, speed, dir);
		UpdateViceBone ();
		UpdateMuscle ();
	}

	/// <summary>
	/// 更新主骨骼层数据
	/// </summary>
	/// <param name="delta_s">时间，单位秒.</param>
	/// <param name="speed">速度.</param>
	/// <param name="dir">方向.</param>
	private void UpdateBone (float delta_s, float speed, Vector2 dir)
	{
		MoveBone bonePtr = GetBaseBone (firstBone);
		MoveBone beforeBonePtr = null;
		bonePtr.pos = bonePtr.pos + dir * (speed * delta_s);
		bonePtr.dir = dir;
		for (int i = firstBone + 1, imax = firstBone + boneNum; i < imax; i++) {
			bonePtr = GetBaseBone (i);
			beforeBonePtr = GetBaseBone (i - 1);
			Vector2 fNDir = (beforeBonePtr.pos - bonePtr.pos).normalized;
			bonePtr.pos = beforeBonePtr.pos - fNDir * bonePtr.lengthWithBefore;
			bonePtr.dir = fNDir;
		}
	}

	private void UpdateViceBone ()
	{
		MoveViceBone vicePtr = null;
		for (int i = 0; i < viceNum; i++) {
			vicePtr = GetViceBone (i);
			SetViceBoneMoveDataWhenFollow (vicePtr);
		}
	}

	private void SetViceBoneMoveDataWhenFollow (MoveViceBone vicePtr)
	{
		MoveBone bonePtr = GetBaseBone (vicePtr.baseBoneId);
		vicePtr.pos = bonePtr.pos;
		vicePtr.dir = bonePtr.dir;
	}

	private void UpdateMuscle ()
	{
		MoveViceBone viceBonePtr = null;
		MoveMuscle musclePtr = null;
		for (int i = 0; i < muscleNum; i++) {
			musclePtr = GetMuscle (i);
			viceBonePtr = GetViceBone (musclePtr.viceBoneId);
			MoveBone baseBone = GetBaseBone (viceBonePtr.baseBoneId);
			musclePtr.dir = baseBone.dir;
			Vector2 offsetDir =
				LogicMath.GetRotateNewPos (baseBone.dir, musclePtr.sinWithBoneDir, musclePtr.cosWithBoneDir);
			musclePtr.pos = baseBone.pos + offsetDir * musclePtr.deltaLength;
		}
	}

	#endregion

	/// <summary>
	/// 获取队伍中心点位置
	/// 通过设置代表中心点的两根骨骼计算得到
	/// 使用两根骨骼是为了在队伍有偶数排的情况下准确计算
	/// </summary>
	/// <returns>The center position.</returns>
	public Vector2 GetCenterPos ()
	{
		if (centerPosBone1 != centerPosBone2) {
			return (GetBaseBone (centerPosBone1).pos + GetBaseBone (centerPosBone2).pos) * 0.5f;
		} else {
			return GetBaseBone (centerPosBone1).pos;
		}
	}

	private MoveBone GetBaseBone (int baseBoneId)
	{
		return boneList[baseBoneId];
	}

	private MoveViceBone GetViceBone (int viceBoneId)
	{
		return viceBoneList[viceBoneId];
	}

	private MoveMuscle GetMuscle (int muscleId)
	{
		return muscleList[muscleId];
	}
}