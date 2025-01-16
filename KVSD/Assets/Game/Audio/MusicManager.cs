using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] tracks; // Массив музыкальных треков
    private AudioSource audioSource;
    private int lastTrackIndex = -1; // Индекс последнего проигранного трека
    public float fadeDuration = 1f; // Длительность плавного перехода между треками

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Устанавливаем начальную громкость
        audioSource.volume = 0.04f;
        PlayFirstTrackImmediately();
    }

    private void PlayFirstTrackImmediately()
    {
        if (tracks.Length == 0) return;

        // Выбор нового трека
        int newTrackIndex = Random.Range(0, tracks.Length);
        lastTrackIndex = newTrackIndex;

        // Меняем трек и сразу проигрываем
        audioSource.clip = tracks[newTrackIndex];
        audioSource.Play();

        // Ждём окончания трека и запускаем следующий с плавным переходом
        Invoke(nameof(PlayRandomTrackWithFade), audioSource.clip.length - fadeDuration);
    }

    private void PlayRandomTrackWithFade()
    {
        if (tracks.Length == 0) return;

        // Выбор нового трека, исключая последний проигранный
        int newTrackIndex;
        do
        {
            newTrackIndex = Random.Range(0, tracks.Length);
        } while (newTrackIndex == lastTrackIndex);

        lastTrackIndex = newTrackIndex;
        StartCoroutine(FadeToNewTrack(tracks[newTrackIndex]));
    }

    private IEnumerator FadeToNewTrack(AudioClip newTrack)
    {
        // Фаза затухания
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0;

        // Меняем трек
        audioSource.clip = newTrack;
        audioSource.Play();

        // Фаза увеличения громкости
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = startVolume;

        // Ждём окончания трека и запускаем следующий
        Invoke(nameof(PlayRandomTrackWithFade), newTrack.length - fadeDuration);
    }
}
