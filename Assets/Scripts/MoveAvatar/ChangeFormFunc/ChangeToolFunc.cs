using UnityEngine;
using System.Collections;

public partial class MoveAvatar
{
	private void _InitBonePosInfo ()
	{
		Vector2 dir = GetBaseBone (0).dir;
		for (int i = 1; i < boneList.Count; i++) {
			boneList[i].pos = boneList[i - 1].pos - dir * boneList[i].lengthWithBefore;
			boneList[i].dir = dir;
		}
	}

	private void _InitMusclePosInfo ()
	{
		MoveMuscle musclePtr = null;
		MoveViceBone bonePtr = null;
		for (int i = 0; i < c_nMaxActorNum; i++) {
			musclePtr = GetMuscle (i);
			bonePtr = GetViceBone (musclePtr.viceBoneId);
			musclePtr.dir = bonePtr.dir;
			Vector2 offsetDir =
				LogicMath.GetRotateNewPos (bonePtr.dir, musclePtr.sinWithBoneDir, musclePtr.cosWithBoneDir);
			musclePtr.pos = bonePtr.pos + offsetDir * musclePtr.deltaLength;
		}
	}
}