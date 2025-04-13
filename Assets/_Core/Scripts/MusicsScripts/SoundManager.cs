using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts
{
    public class SoundManager: MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

    [Header("Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.3f;
    [Range(0f, 1f)] public float sfxVolume = 0.6f;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSourcePrefab;

    private Queue<AudioSource> _sfxPool = new Queue<AudioSource>();
    private const int INITIAL_POOL_SIZE = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            CreateNewSfxSource();
        }
    }

    private AudioSource CreateNewSfxSource()
    {
        AudioSource source = Instantiate(_sfxSourcePrefab, transform);
        source.gameObject.SetActive(false);
        _sfxPool.Enqueue(source);
        return source;
    }

    /// <summary> Воспроизвести звук из SFX-пула </summary>
    public void PlaySfx(AudioClip clip, float pitch = 1f, float volumeModifier = 1f)
    {
        if (clip == null) return;

        AudioSource source = _sfxPool.Count > 0 ? _sfxPool.Dequeue() : CreateNewSfxSource();
        source.gameObject.SetActive(true);

        source.clip = clip;
        source.pitch = Mathf.Clamp(pitch, 0.5f, 2f);
        source.volume = sfxVolume * masterVolume * volumeModifier;
        source.Play();

        StartCoroutine(ReturnToPoolAfterPlay(source));
    }

    private System.Collections.IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);
        source.gameObject.SetActive(false);
        _sfxPool.Enqueue(source);
    }

    /// <summary> Воспроизвести музыку (с плавным переходом) </summary>
    public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
    {
        if (_musicSource.clip == clip) return;

        StartCoroutine(FadeMusic(clip, fadeDuration));
    }

    private System.Collections.IEnumerator FadeMusic(AudioClip newClip, float duration)
    {
        // Фейд-аут текущей музыки
        float startVolume = _musicSource.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }
        _musicSource.Stop();

        // Переключение на новый трек
        _musicSource.clip = newClip;
        _musicSource.Play();

        // Фейд-ин новой музыки
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _musicSource.volume = Mathf.Lerp(0f, musicVolume * masterVolume, t / duration);
            yield return null;
        }
    }

    /// <summary> Остановить музыку </summary>
    public void StopMusic(float fadeDuration = 0.5f)
    {
        StartCoroutine(FadeMusic(null, fadeDuration));
    }

    /// <summary> Установить громкость SFX </summary>
    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateAllSfxVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);

        _musicSource.volume = musicVolume * masterVolume;
    }

    private void UpdateAllSfxVolumes()
    {
        foreach (var source in GetComponentsInChildren<AudioSource>())
        {
            if (source != _musicSource)
            {
                source.volume = sfxVolume * masterVolume;
            }
        }
    }
    }
}