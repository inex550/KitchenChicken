using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour {

    [Header(Strings.presetFields)]
    [SerializeField] private Transform recipeBlockPrefab;
    [SerializeField] private Transform containerTransform;

    private List<RecipeBlockUI> recipeBlocks;

    private void Start() {
        recipeBlocks = new List<RecipeBlockUI>();

        DeliveryManager.Instance.OnRecipeAdded += OnRecipeAdded;
        DeliveryManager.Instance.OnRecipeRemoved += OnRecipeRemoved;
    }

    private void OnRecipeAdded(RecipeSO recipe) {
        Transform recipeBlockTransform = Instantiate(recipeBlockPrefab, containerTransform);
        RecipeBlockUI recipeBlock = recipeBlockTransform.GetComponent<RecipeBlockUI>();
        recipeBlock.SetupWithRecipe(recipe);

        recipeBlocks.Add(recipeBlock);
    }

    private void OnRecipeRemoved(RecipeSO recipe) {
        for (int i = 0; i < recipeBlocks.Count; ++i) {
            if (recipeBlocks[i].GetRecipe() == recipe) {
                recipeBlocks[i].DestroySelf();
                recipeBlocks.RemoveAt(i);
                break;
            }
        }
    }
}