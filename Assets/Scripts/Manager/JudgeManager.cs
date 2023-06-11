using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    private static float minSimilarity = 15.0f;
	public List<GameObject> boxNoteList = new List<GameObject>();

	[SerializeField] Transform Center = null;
	[SerializeField] RectTransform judgeRect = null;
	Vector2 judgeBox;

    void Start()
    {
        judgeBox = new Vector2();
        judgeBox.Set(Center.localPosition.x - judgeRect.rect.width / 2,
        	Center.localPosition.x + judgeRect.rect.width / 2);
    }

    public void Judge(List<Vector3> userPos)
    {
        List<Vector3> perfectPos = boxNoteList[0].GetComponent<Note>().GetVectors();
        if (VectorUtils.CheckSimilar(perfectPos, userPos, minSimilarity))
            Debug.Log("Good");
        else
            Debug.Log("Bad");
        boxNoteList[0].GetComponent<Note>().HideNote();
        boxNoteList.RemoveAt(0);
    }
}
