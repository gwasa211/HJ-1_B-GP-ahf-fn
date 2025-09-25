using UnityEngine; 

public class Door : InteractableObject
{
    [Header("문 설정")]
    public bool isOpen = false;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    private Vector3 closedPosition;

    protected override void Awake()
    {
        base.Awake(); 

       
        objectName = "문";
        interactionText = "[E] 문 열기";
        interactionType = InteractionType.Building;

        closedPosition = transform.position;
        openPosition = closedPosition + transform.right * 3f;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        Debug.Log("문과 상호작용했습니다. 현재 상태: " + (isOpen ? "열림" : "닫힘"));
    }

    void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
    }
}