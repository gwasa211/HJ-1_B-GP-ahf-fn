using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("상호 작용 설정")]
    public float interactionRange = 2.0f;
    public LayerMask interactionLayerMaxk = 1;
    private KeyCode interactionKey = KeyCode.E;

    [Header("UI 설정")]
    public Text interactionText;
    public GameObject interactionUI;

    private Transform playerTransform;
    private InteractableObject currentInteractable;


    private void Start()
    {
    
      
            playerTransform = transform;
            HideInteractionUI();
        
    }
    private void Update()
    {
        CheckForInteractables();
        HandleInteractionInput();

    }


    private void HandleInteractionInput()
    {
        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    void ShowInteractionUI(string text)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);

        }

        if (interactionText != null)
        {
            interactionText.text = text;
        }
    }

    void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }

    }
    public void UpdateInteractionUI(string promptText)
    {
        bool shouldShow = !string.IsNullOrEmpty(promptText);
        if (interactionUI != null)
        {
            interactionUI.SetActive(shouldShow);
        }
        if (shouldShow && interactionText != null)
        { 
            interactionText.text = promptText;
        }
    }
    void CheckForInteractables()
    {
        Vector3 checkPosition = playerTransform.position + playerTransform.forward * (interactionRange * 0.5f);

        Collider[] hitColliders = Physics.OverlapSphere(checkPosition, interactionRange, interactionLayerMaxk);

        InteractableObject closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in hitColliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(playerTransform.position, collider.transform.position);

                Vector3 directionToObject = (collider.transform.position - playerTransform.position).normalized;
                float angle = Vector3.Angle(playerTransform.forward, directionToObject);

                if (angle < 90f && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
            if (closestInteractable != currentInteractable)
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnPlayerExit();
                }

                currentInteractable = closestInteractable;

                if (currentInteractable != null)
                {
                    currentInteractable.OnPlayerEnter();
                    ShowInteractionUI(currentInteractable.GetInteractionText());
                }
                else
                {
                    HideInteractionUI();
                }
            }
        }

    }
}
