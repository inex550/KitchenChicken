using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }
    
    private const float DEFAULT_VOLUME = 1.0f;

    [SerializeField] private AudioClipRefsSO audioClipRefs;

    private float volume = 1.0f;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        volume = PlayerPrefs.GetFloat(Strings.pref_soundEffectsVolume, DEFAULT_VOLUME);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnChickenToolDropedHere += BaseCounter_OnChickenToolPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1.0f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1.0f) {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position) {
        PlaySound(audioClipRefs.footstep, position);
    }

    public void PlayCountdownSound() {
        PlaySound(audioClipRefs.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position) {
        PlaySound(audioClipRefs.warning, position);
    }

    private void DeliveryManager_OnRecipeSuccess() {
        PlaySound(audioClipRefs.deliverySuccessClip, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed() {
        PlaySound(audioClipRefs.deliveryFailedClip, DeliveryCounter.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs args) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefs.chop, cuttingCounter.transform.position);
    }

    private void Player_OnPickedSomething() {
        PlaySound(audioClipRefs.pickup, Player.Instance.transform.position);
    }

    private void BaseCounter_OnChickenToolPlacedHere(object sender, System.EventArgs args) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefs.drop, baseCounter.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs args) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefs.trash, trashCounter.transform.position);
    }

    public void ChangeVolume() {
        volume += 0.1f;
        if (volume > 1.0f) {
            volume = 0.0f;
        }

        PlayerPrefs.SetFloat(Strings.pref_soundEffectsVolume, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}