using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<NoteInfo> noteInfos;

    [SerializeField] private string filename;
    [SerializeField] Transform tfNoteAppear;
    [SerializeField] GameObject goNote;

    public int bpm = 0;
    double currentTime = 0d;
    JudgeManager judgeManager;
    JudgeViewer judgeViewer;

    void Start()
    {
        InitializeNotesFromTextFile(filename);
        judgeManager = GetComponent<JudgeManager>();
        judgeViewer = GetComponent<JudgeViewer>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (noteInfos.Count > 0 && currentTime >= noteInfos[0].GetAppearTime()) {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            Image t_noteImage = t_note.GetComponent<Image>();
            string spritePath = noteInfos[0].GetImagePath();
            Sprite newSprite = Resources.Load<Sprite>(spritePath);
            if (newSprite != null)
                t_noteImage.sprite = newSprite;
            t_note.GetComponent<Note>().SetVectors(noteInfos[0].GetVectors());
            t_note.tag = "note";
            judgeManager.boxNoteList.Add(t_note);
            // currentTime -= noteInfos[0].GetAppearTime();
            noteInfos.RemoveAt(0);
        }
    }

    void InitializeNotesFromTextFile(string filename)
    {
        string filePath = Path.Combine(Application.dataPath, "Resources", filename);
        string[] lines = File.ReadAllLines(filePath);

        string firstLine = lines[0];
        string[] elements = firstLine.Split(' ');
        List<Vector3> frameJoints = new List<Vector3>();
        foreach (string element in elements)
            frameJoints.Add(VectorUtils.ParseVector3(element));
        List<Vector3> bodyFrame = VectorUtils.ParseBodyFrame(frameJoints);

        lines = lines.Skip(1).ToArray();
        noteInfos = new List<NoteInfo>();
        foreach (string line in lines)
        {
            elements = line.Split(' ');

            float appearTime = float.Parse(elements[0]);
            string spriteName = elements[1];
            List<Vector3> jointList = new List<Vector3>();
            for (int i = 2; i < elements.Length; i++)
            	jointList.Add(VectorUtils.ParseVector3(elements[i]));

            noteInfos.Add(new NoteInfo(appearTime, spriteName, bodyFrame, jointList));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("note")) {
            judgeManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            judgeViewer.DisplayImage();
        }
    }
}