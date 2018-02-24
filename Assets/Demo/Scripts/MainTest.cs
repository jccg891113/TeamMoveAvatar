using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTest : MonoBehaviour
{
	public float speed = 1;
	public float rotateSpeed = 10;
	public Transform dirTrans;
	public Transform dir_1;
	public Transform dir_2;

	public List<Transform> actors;

	private MoveAvatar avatar;

	private Vector2 MoveDir {
		get {
			Vector2 dir = dir_2.position - dir_1.position;
			return dir.normalized;
		}
	}

	private void Start ()
	{
		avatar = new MoveAvatar ();
		avatar.ChangeToDualColumn (actors.Count, Vector2.one, MoveDir);
		SetPos ();
	}

	private void Update ()
	{
		avatar.Update (Time.deltaTime, speed, MoveDir);
		SetPos ();
	}

	private void OnGUI ()
	{
		if (GUILayout.RepeatButton ("Left")) {
			dirTrans.localEulerAngles += new Vector3 (0, 0, rotateSpeed * Time.deltaTime);
		}
		if (GUILayout.RepeatButton ("Right")) {
			dirTrans.localEulerAngles += new Vector3 (0, 0, -rotateSpeed * Time.deltaTime);
		}
	}

	private void SetPos ()
	{
		for (int i = 0; i < actors.Count && i < avatar.muscleList.Count; i++) {
			actors[i].localPosition = avatar.muscleList[i].pos;
		}
	}
}
