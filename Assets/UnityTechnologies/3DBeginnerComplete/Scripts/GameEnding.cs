using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    public float maxDetection = 100.0f;
    public float detectionStep = 1.0f;
    public Slider slider;
    private float minDetection = 0.0f;
    private float currentDetection = 100.0f;
    private bool playerInVisionCone = false;
    private float detectionMultiplier = 1.0f;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    void Start () 
    {
        currentDetection = minDetection;
        slider.maxValue = maxDetection;
        slider.value = minDetection;
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    public void DetectingPlayer (float multiplier)
    {
        playerInVisionCone = true;
        detectionMultiplier = multiplier;
    }

    void FixedUpdate ()
    {
        if (playerInVisionCone)
        {
            // increase detection value at multiplier rate
            currentDetection += (detectionMultiplier * detectionStep);
            slider.value = currentDetection;
            // check for end state
            if (currentDetection >= maxDetection)
            {
                // player is caught since they are now fully detected
                m_IsPlayerCaught = true;
            }
            // reset for next pass
            playerInVisionCone = false;
            detectionMultiplier = 1.0f;
        }
        else if (!playerInVisionCone && currentDetection < maxDetection)
        {
            // drain detection bar at base rate
            currentDetection -= detectionStep;
            slider.value = currentDetection;
            // reset to min if exceeded
            if (currentDetection < minDetection)
            {
                currentDetection = minDetection;
            }
        }
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}
