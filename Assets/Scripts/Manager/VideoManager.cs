using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public float waitSeconds;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(waitSeconds);

        videoPlayer.Play();
    }
}
