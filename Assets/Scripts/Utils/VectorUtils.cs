using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorUtils
{
    public static List<Vector3> ParseBodyFrame(List<Vector3> input) {
        List<Vector3> result = new List<Vector3>();
        result.Add(input[(int)BodyJoint.LEFT_SHOULDER] - input[(int)BodyJoint.SPINE_SHOULDER]);
        result.Add(input[(int)BodyJoint.LEFT_ELBOW] - input[(int)BodyJoint.LEFT_SHOULDER]);
        result.Add(input[(int)BodyJoint.LEFT_WRIST] - input[(int)BodyJoint.LEFT_ELBOW]);
        result.Add(input[(int)BodyJoint.RIGHT_SHOULDER] - input[(int)BodyJoint.SPINE_SHOULDER]);
        result.Add(input[(int)BodyJoint.RIGHT_ELBOW] - input[(int)BodyJoint.RIGHT_SHOULDER]);
        result.Add(input[(int)BodyJoint.RIGHT_WRIST] - input[(int)BodyJoint.RIGHT_ELBOW]);
        result.Add(input[(int)BodyJoint.SPINE_BASE] - input[(int)BodyJoint.SPINE_SHOULDER]);
        result.Add(input[(int)BodyJoint.LEFT_KNEE] - input[(int)BodyJoint.SPINE_BASE]);
        result.Add(input[(int)BodyJoint.LEFT_ANKLE] - input[(int)BodyJoint.LEFT_KNEE]);
        result.Add(input[(int)BodyJoint.RIGHT_KNEE] - input[(int)BodyJoint.SPINE_BASE]);
        result.Add(input[(int)BodyJoint.RIGHT_ANKLE] - input[(int)BodyJoint.RIGHT_KNEE]);

        return result;
    }

    public static Vector3 ParseVector3(string input)
    {
        string[] values = input.Split(',');

        if (values.Length != 2)
        {
            Debug.LogError("Invalid input format. Expected three comma-separated values.");
            return Vector3.zero;
        }

        float x, y;
        if (float.TryParse(values[0], out x) &&
            float.TryParse(values[1], out y))
        {
            return new Vector3(x, -y, 0);
        }
        else
        {
            Debug.LogError("Failed to parse input values to Vector3.");
            return Vector3.zero;
        }
    }

    public static List<Vector3> Calc3D(List<Vector3> frame, List<Vector3> input) {
        List<Vector3> result = new List<Vector3>();

        for (int i = 0; i < frame.Count; i++) {
            float magnitude = frame[i].magnitude;
            float zValue = magnitude * magnitude - input[i].x * input[i].x - input[i].y * input[i].y;
            if (zValue < 0)
                zValue = 0;
            result.Add(new Vector3(input[i].x, input[i].y, Mathf.Sqrt(zValue)));
        }

        return result;
    }

    public static bool CheckSimilar(List<Vector3> v1, List<Vector3> v2, float maxAngle) {
        for (int i = 0; i < v1.Count; i++) {
            if (Vector3.Angle(v1[i], v2[i]) > maxAngle)
                return false;
        }
        return true;
    }
}