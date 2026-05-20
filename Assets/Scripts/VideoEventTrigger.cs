using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoEventTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    public UnityEvent onVideoFinished;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += VideoEnded;
        videoPlayer.Prepare();
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= VideoEnded;
        }
    }

    private void VideoEnded(VideoPlayer source)
    {
        onVideoFinished?.Invoke();
    }
}