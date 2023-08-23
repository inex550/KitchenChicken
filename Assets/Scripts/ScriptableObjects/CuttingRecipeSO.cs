using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {
    public ChickenToolSO origin;
    public ChickenToolSO sliced;
    public float cuttingProgressMax;
}
