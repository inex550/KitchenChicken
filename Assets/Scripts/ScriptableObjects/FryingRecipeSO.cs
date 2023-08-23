using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {

    public ChickenToolSO origin;
    public ChickenToolSO fried;
    public float fryingTimerMax;
    public StoveCounter.FryingState state;
}
