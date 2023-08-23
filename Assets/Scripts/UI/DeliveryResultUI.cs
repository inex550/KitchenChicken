using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultUI : MonoBehaviour {

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();

        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeSuccess() {
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = Strings.deliveryMessage_success;

        animator.SetTrigger(Strings.trigger_popup);
    }

    private void DeliveryManager_OnRecipeFailed() {
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = Strings.deliveryMessage_faield;

        animator.SetTrigger(Strings.trigger_popup);
    }
}