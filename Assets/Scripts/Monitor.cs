using UnityEngine;
using UnityEngine.Video;

public class Monitor : MonoBehaviour
{
    public VideoPlayer videoPlayerOsu;
    public SpriteRenderer codingSpriteRenderer;

    public Eyes eyes;

    public GameObject gameOverCanvas;

    bool previousSpaceHeld = false;
    bool hasGameEnded = false;

    private void Awake()
    {
        Time.timeScale = 1f;

        videoPlayerOsu.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Osu.mp4");

        videoPlayerOsu.Play();

        OnSpaceHeld(false);
    }

    private void Update()
    {
        if (hasGameEnded) return;

        bool holdingSpace = Input.GetKey(KeyCode.Space);
        if(previousSpaceHeld != holdingSpace)
        {
            previousSpaceHeld = holdingSpace;
            OnSpaceHeld(holdingSpace);
        }

        if(eyes.isStaring && holdingSpace && !hasGameEnded)
        {
            hasGameEnded = true;
            OnGameOver();
        }
    }

    void OnSpaceHeld(bool isHeld)
    {
        videoPlayerOsu.GetComponent<SpriteRenderer>().enabled = isHeld;
        codingSpriteRenderer.enabled = !isHeld;
        if (isHeld)
        {
            videoPlayerOsu.Play();
        }
        else
        {
            videoPlayerOsu.Pause();
        }
    }

    void OnGameOver()
    {
        eyes.Shake();
        gameOverCanvas.SetActive(true);
        OnSpaceHeld(true);
        Time.timeScale = 0;
    }
}
