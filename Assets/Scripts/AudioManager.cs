using System.Collections;
using System.Linq;
using BNG;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource SFXSource;

    [Header("Audio Mixer")]
    [SerializeField]
    private AudioMixer audioMixer;

    [Header("SFX")]
    public AudioClip backgroundMusic;
    public AudioClip calmMusic;

    public AudioClip liftingEntering;
    public AudioClip suddenMovement;
    public AudioClip switcher1;
    public AudioClip switcher2;
    public AudioClip carabiner;
    public AudioClip footstepFullGear;
    public AudioClip liftWorking1;
    public AudioClip liftWorking2;
    public AudioClip climbing;
    public AudioClip death;
    public AudioClip metalCling1;
    public AudioClip metalCling2;
    public AudioClip metalCling3;
    public AudioClip windSound;

    private static AudioManager instance;
    private float targetVolume = 1.0f;
    private Coroutine volumeCoroutine;

    private WindTrigger windTrigger;
    private bool isWindSoundPlaying = false;

    private Climbable[] climbables;
    
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
        StartCoroutine(CheckWindTrigger());
        EventManager.OnPlayerTeleportBeginning += PlayFootStepSound;
        
        climbables = FindObjectsOfType<Climbable>();
    }

    void Update()
    {
        CheckLadderGrabbed();
    }
    
    private void CheckLadderGrabbed()
    {
        foreach (var climbable in climbables)
        {
            if (climbable.HeldByGrabbers == null)
            {
                continue;
            }

            if (climbable.HeldByGrabbers.Count != 0)
            {
                IsLadderGrabbed();
            }
        }
    }
    
    private void PlayFootStepSound()
    {
        SFXSource.clip = footstepFullGear;
        SFXSource.Play();
        StartCoroutine(StopSoundAfterDuration(1.5f));
    }
    
    public void PlayLiftingEnteringSound()
    {
        SFXSource.clip = liftingEntering;
        SFXSource.Play();
    }

    public void IsLadderGrabbed()
    {
        SFXSource.clip = climbing;
        SFXSource.Play();
    }
    
    private IEnumerator CheckWindTrigger()
    {
        while (windTrigger == null)
        {
            windTrigger = FindObjectOfType<WindTrigger>();
            if (windTrigger != null)
            {
                windTrigger.OnWindStart.AddListener(HandleWindStart);
                windTrigger.OnWindStop.AddListener(HandleWindStop);
                windTrigger.OnWindStay.AddListener(HandleWindStay);

                Debug.Log("WindTrigger found!");

                yield break; // Exit the coroutine
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        /*if (StressManager.Instance == null)
        {
            Debug.LogError("StressManager not found in scene");
            gameObject.SetActive(false);
            return;
        }*/
        EventManager.OnStressDataReceived += ReactToStressData;
    }

    public void OnDisable()
    {
        EventManager.OnStressDataReceived -= ReactToStressData;
    }
    
    public void IsScissorLiftGrabbed()
    {
        SFXSource.clip = liftWorking1;
        SFXSource.Play();
        StartCoroutine(StopSoundAfterDuration(2.0f));
    }
    
    private IEnumerator StopSoundAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        SFXSource.Stop();
    }
    
    public void ReactToStressData(StressData data)
    {
        float stressLevel;
        
        if (StressManager.Instance == null)
        {
            stressLevel = 0.0f;
        }
        else
        {
            stressLevel = data.StressLevel;
        }

        if (data.StressLevel > 0.8f)
        {
            if (musicSource.clip != calmMusic)
            {
                StartCoroutine(ChangeMusic(calmMusic, 0.0f, 1.0f, 3.0f)); // Longer fade time
            }
            else
            {
                ChangeVolume(1.0f, 3.0f); // Longer fade time
            }
        }
        else if (data.StressLevel > 0.5f)
        {
            if (musicSource.clip == calmMusic)
            {
                StartCoroutine(ChangeMusic(backgroundMusic, 0.0f, 0.5f, 3.0f)); // Longer fade time
            }
            else
            {
                ChangeVolume(0.5f, 3.0f); // Longer fade time
            }
        }
        else
        {
            if (musicSource.clip != backgroundMusic)
            {
                StartCoroutine(ChangeMusic(backgroundMusic, 0.0f, 1.0f, 3.0f)); // Longer fade time
            }
            else
            {
                ChangeVolume(1.0f, 3.0f); // Longer fade time
            }
        }
    }

    private void ChangeVolume(float newVolume, float duration)
    {
        if (volumeCoroutine != null)
        {
            StopCoroutine(volumeCoroutine);
        }
        volumeCoroutine = StartCoroutine(ChangeVolumeCoroutine(newVolume, duration));
    }

    private IEnumerator ChangeVolumeCoroutine(float newVolume, float duration)
    {
        float currentVolume;
        audioMixer.GetFloat("music", out currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20); // Convert from dB to linear

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float volume = Mathf.Lerp(currentVolume, newVolume, elapsedTime / duration);
            audioMixer.SetFloat("music", Mathf.Log10(volume) * 20); // Convert from linear to dB
            yield return null;
        }

        audioMixer.SetFloat("music", Mathf.Log10(newVolume) * 20); // Ensure final volume is set
    }

    private IEnumerator ChangeMusic(AudioClip newClip, float fadeOutVolume, float fadeInVolume, float duration)
    {
        yield return ChangeVolumeCoroutine(fadeOutVolume, duration);
        musicSource.clip = newClip;
        musicSource.Play();
        yield return ChangeVolumeCoroutine(fadeInVolume, duration);
    }

    private void HandleWindStart()
    {
        // Handle wind start event
    }

    private void HandleWindStop()
    {
        if (isWindSoundPlaying)
        {
            StartCoroutine(FadeOutWindSound(1.0f)); // Adjust fade-out duration as needed
        }
    }

    private void HandleWindStay()
    {
        if (!isWindSoundPlaying)
        {
            StartCoroutine(FadeInWindSound(1.0f)); // Adjust fade-in duration as needed
        }
    }

    private IEnumerator FadeInWindSound(float duration)
    {
        isWindSoundPlaying = true;
        SFXSource.clip = windSound;
        SFXSource.Play();
        yield return ChangeVolumeCoroutine(1.0f, duration); // Fade in to full volume
    }

    private IEnumerator FadeOutWindSound(float duration)
    {
        yield return ChangeVolumeCoroutine(0.0f, duration); // Fade out to zero volume
        SFXSource.Stop();
        isWindSoundPlaying = false;
    }
}