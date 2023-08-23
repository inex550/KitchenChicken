using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeBlockUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private RectTransform plateIconPrefab;
    [SerializeField] private Transform plateIconsContainer;

    private float plateIconSize = 40.0f;

    private RecipeSO recipe;

    public void SetupWithRecipe(RecipeSO recipe) {
        this.recipe = recipe;

        recipeNameText.text = recipe.recipeName;

        foreach (ChickenToolSO chickenTool in recipe.chickenTools) {
            RectTransform plateIconTransform = Instantiate(plateIconPrefab, plateIconsContainer);
            plateIconTransform.sizeDelta = new Vector2(plateIconSize, plateIconSize);
            
            PlateIconUI plateIcon = plateIconTransform.GetComponent<PlateIconUI>();
            plateIcon.SetBackgroundActive(false);
            plateIcon.SetChickenTool(chickenTool);
        }
    }

    public RecipeSO GetRecipe() {
        return recipe;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}