using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject {

    public AudioClip[] deliverySuccessClip;
    public AudioClip[] deliveryFailedClip;

    public AudioClip[] chop;
    public AudioClip[] footstep;
    public AudioClip[] drop;
    public AudioClip[] pickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}