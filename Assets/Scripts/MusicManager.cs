using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Müzik Ayarları")]
    public AudioClip musicClip;
    [Range(0f, 1f)] public float volume = 0.5f;
    public bool loop = true;
    public bool playOnStart = true;

    private AudioSource source;

    private void Awake()
    {
        // Singleton: tek kopya kalsın
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource hazırla
        source = GetComponent<AudioSource>();
        if (source == null) source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;   // kontrol bizde
        source.loop = loop;
        source.volume = volume;

        if (musicClip != null)
            source.clip = musicClip;

        if (playOnStart && source.clip != null && !source.isPlaying)
            source.Play();
    }

    // İsteğe bağlı yardımcılar
    public void SetVolume(float v) { volume = Mathf.Clamp01(v); if (source) source.volume = volume; }
    public void PauseMusic() { if (source) source.Pause(); }
    public void ResumeMusic() { if (source) source.UnPause(); }
    public void StopMusic() { if (source) source.Stop(); }
}