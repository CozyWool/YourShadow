using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoEventTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    // Твое событие, сюда в инспекторе можно накинуть функции
    public UnityEvent onVideoFinished;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Подписка на событие конца видео
        videoPlayer.loopPointReached += VideoEnded;

        // Если видео весит много, лучше запустить подготовку
        videoPlayer.Prepare();
    }

    void OnDestroy()
    {
        // Отписка, чтобы не было утечек памяти
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= VideoEnded;
        }
    }

    private void VideoEnded(VideoPlayer source)
    {

        // Запуск твоего события
        onVideoFinished?.Invoke();
    }
}