using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInfo
{
	private float appearTime;
	private string imagePath;
	private List<Vector3> vectorList;

	public NoteInfo(float time, string path, List<Vector3> bodyFrame, List<Vector3> jointList) {
		appearTime = time;
        imagePath = "Notes/" + path;
        vectorList = VectorUtils.ParseBodyFrame(jointList);
        vectorList = VectorUtils.Calc3D(bodyFrame, vectorList);
	}

	public List<Vector3> GetVectors() {
		return vectorList;
	}

	public string GetImagePath() {
		return imagePath;
	}

	public float GetAppearTime() {
		return appearTime;
	}
}
