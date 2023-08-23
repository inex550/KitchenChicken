using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public static DeliveryManager Instance { get; private set; }

    public event Action<RecipeSO> OnRecipeAdded;
    public event Action<RecipeSO> OnRecipeRemoved;
    public event Action OnRecipeSuccess;
    public event Action OnRecipeFailed;

    [SerializeField] private List<RecipeSO> recipes;

    [SerializeField] private float startSpawnRecipeTimerMax = 4.0f;
    [SerializeField] private float nextSpawnRecipeTimerMax = 6.0f;
    [SerializeField] private int waitingRecipesMax = 4;

    private float spawnRecipeTimerMax;

    private float spawnRecipeTimer;

    private List<RecipeSO> waitingRecipes;

    private int deliveredRecipiesCount = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        spawnRecipeTimerMax = startSpawnRecipeTimerMax;

        waitingRecipes = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;

        if (spawnRecipeTimer >= spawnRecipeTimerMax && GameManager.Instance.IsGamePlaying()) {
            spawnRecipeTimerMax = nextSpawnRecipeTimerMax;
            spawnRecipeTimer = 0.0f;

            if (waitingRecipes.Count < waitingRecipesMax) {
                RecipeSO recipe = recipes[UnityEngine.Random.Range(0, recipes.Count)];
                waitingRecipes.Add(recipe);
                OnRecipeAdded?.Invoke(recipe);
            }
        }
    }

    public void Deliver(List<ChickenToolSO> ingredients) {
        bool delivered = false;

        for (int i = 0; i < waitingRecipes.Count; ++i) {
            RecipeSO recipe = waitingRecipes[i];
            delivered = CheckCorrectRecipe(recipe, ingredients);

            if (delivered) {
                // Recipe was delivered
                deliveredRecipiesCount += 1;

                waitingRecipes.RemoveAt(i);
                OnRecipeRemoved?.Invoke(recipe);

                OnRecipeSuccess?.Invoke();
                break;
            }
        }

        if (!delivered) {
            // Recipe was not delivered
            OnRecipeFailed?.Invoke();
        }
    }

    private bool CheckCorrectRecipe(RecipeSO recipe, List<ChickenToolSO> chickenTools) {
        bool correct = recipe.chickenTools.Count == chickenTools.Count;

        if (correct) {
            foreach (ChickenToolSO chickenTool in chickenTools) {
                if (!recipe.chickenTools.Contains(chickenTool)) {
                    correct = false;
                    break;
                }
            }
        }

        return correct;
    }

    public int GetDeliveredRecipesCount() {
        return deliveredRecipiesCount;
    }
}