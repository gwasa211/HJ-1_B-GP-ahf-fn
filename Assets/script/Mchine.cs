using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [수정됨] 클래스 이름 오타 수정 (Mchine -> Machine)
public class Machine : InteractableObject
{
    // [수정됨] Start()를 부모와 동일한 Awake()로 변경하고 override
    protected override void Awake()
    {
        base.Awake(); // 부모의 Awake()를 먼저 실행
        objectName = "기계";
        interactionText = "[E] 기계 동작";
        interactionType = InteractionType.Machine;
    }

    // [참고] Interact 함수를 override하여 기계만의 동작을 구현합니다.
    public override void Interact()
    {
        Debug.Log("기계를 작동시킵니다.");
        // 부모의 기본 동작을 실행하거나, 이 클래스만의 특별한 동작을 실행
        // base.OperateMachine(); 

        // 코루틴을 사용한 특별한 동작 실행
        StartCoroutine(OperateMachineCoroutine());
    }

    // [개선됨] 코루틴 함수의 이름을 더 명확하게 변경
    private IEnumerator OperateMachineCoroutine()
    {
        // 5초 동안 30도씩 회전하는 예시
        for (int i = 0; i < 50; i++)
        {
            transform.Rotate(new Vector3(0, 1, 0), 30 * Time.deltaTime * 5); // 더 부드러운 회전
            yield return null; // 한 프레임 대기
        }

        Debug.Log("기계 작동 완료.");
    }
}