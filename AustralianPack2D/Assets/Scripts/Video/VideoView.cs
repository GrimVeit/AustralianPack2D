using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoView : View
{
    [SerializeField] private VideoPlayers videoPlayers;

    public void Initialize()
    {
        videoPlayers.Initialize();
    }

    public void Prepare(string id)
    {
        var videoPlay = videoPlayers.GetVideoPlayById(id);

        if (videoPlay.VideoPlayer == null)
        {
            Debug.LogWarning($"VideoPlayer with id: {id} not found!");
            return;
        }

        var vp = videoPlay.VideoPlayer;

        videoPlay.Image.texture = videoPlay.Texture;
        videoPlay.Image.enabled = false;

        vp.Stop();
        vp.frame = 0;
        vp.time = 0;

        vp.Prepare();
    }


    public void Play(string id, Action onComplete = null)
    {
        var videoPlay = videoPlayers.GetVideoPlayById(id);

        if (videoPlay.VideoPlayer == null)
        {
            Debug.LogWarning($"VideoPlayer with id: {id} not found!");
            return;
        }

        var vp = videoPlay.VideoPlayer;

        videoPlay.Image.texture = videoPlay.Texture;
        videoPlay.Image.enabled = false;

        vp.Stop();
        vp.frame = 0;
        vp.time = 0;

        vp.loopPointReached -= OnVideoEnd;
        vp.loopPointReached += OnVideoEnd;


        void OnVideoEnd(VideoPlayer player)
        {
            player.loopPointReached -= OnVideoEnd;
            onComplete?.Invoke();
        }


        if (vp.isPrepared)
        {
            StartVideo();
        }
        else
        {
            vp.prepareCompleted += OnPrepared;
            vp.Prepare();
        }


        void OnPrepared(VideoPlayer player)
        {
            player.prepareCompleted -= OnPrepared;
            StartVideo();
        }


        void StartVideo()
        {
            StartCoroutine(StartRoutine());
        }


        IEnumerator StartRoutine()
        {
            vp.frame = 0;

            // Рендерим первый кадр
            vp.Play();

            yield return null;

            vp.Pause();

            yield return null;

            // Теперь texture содержит первый кадр
            videoPlay.Image.enabled = true;

            vp.Play();
        }
    }


    public void Stop(string id)
    {
        var videoPlay = videoPlayers.GetVideoPlayById(id);

        if (videoPlay.VideoPlayer == null)
            return;

        videoPlay.VideoPlayer.Stop();
        videoPlay.VideoPlayer.frame = 0;

        videoPlay.Image.enabled = false;
    }
}

[Serializable]
public class VideoPlayers
{
    [SerializeField] private List<VideoPlay> videoPlays = new();

    public void Initialize()
    {
        for (int i = 0; i < videoPlays.Count; i++)
        {
            if (videoPlays[i].IsAwakePrepare)
            {
                videoPlays[i].Image.texture = videoPlays[i].Texture;

                videoPlays[i].VideoPlayer.time = 0;
                videoPlays[i].VideoPlayer.Prepare();
            }
        }
    }

    public VideoPlay GetVideoPlayById(string id) => videoPlays.FirstOrDefault(data => data.Id == id);
}

[System.Serializable]
public class VideoPlay
{
    [SerializeField] private string id;
    [SerializeField] private RawImage image;
    [SerializeField] private Texture texture;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private bool isAwakePrepare;

    public string Id => id;
    public VideoPlayer VideoPlayer => videoPlayer;
    public RawImage Image => image;
    public Texture Texture => texture;
    public bool IsAwakePrepare => isAwakePrepare;
}
