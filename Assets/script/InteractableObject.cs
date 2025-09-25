using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("상호 작용 정보")]
    public string objectName = "아이템";
    public string interactionText = "[E] 상호 작용";
    public InteractionType interactionType = InteractionType.Item;

    [Header("하이라이트 설정")]
    public Color highlightColor = Color.yellow;

    public Renderer objectRenderer;
    private Material objectMaterial;
    private Color originalColor;
    private bool isHighlighted = false;

    public enum InteractionType
    {
        Item,
        Machine,
        Building,
        NPC,
        Collectible
    }

    protected virtual void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material;
            originalColor = objectMaterial.color;
        }
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public virtual void OnPlayerEnter()
    {
        Debug.Log($"[{objectName}] 감지됨");
        HighlightObject();
    }

    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objectName}] 범위에서 벗어남");
        RemoveHighlight();
    }

    protected virtual void HighlightObject()
    {
        if (objectMaterial != null && !isHighlighted)
        {
            objectMaterial.color = highlightColor;
            isHighlighted = true;
        }
    }

    protected virtual void RemoveHighlight()
    {
        if (objectMaterial != null && isHighlighted)
        {
            objectMaterial.color = originalColor;
            isHighlighted = false;
        }
    }

    protected virtual void CollectItem()
    {
        Destroy(gameObject);
    }

    protected virtual void OperateMachine()
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = Color.green;
        }
    }

    protected virtual void AccessBuilding()
    {
        transform.Rotate(Vector3.up * 90f);
    }

    protected virtual void TalkToNPC()
    {
        Debug.Log($"{objectName}와 대화를 시작합니다");
    }

    public virtual void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Item:
            case InteractionType.Collectible:
                CollectItem();
                break;

            case InteractionType.Machine:
                OperateMachine();
                break;

            case InteractionType.Building:
                AccessBuilding();
                break;

            case InteractionType.NPC:
                TalkToNPC();
                break;
        }
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}