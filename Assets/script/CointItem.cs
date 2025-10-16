using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text, Button 등을 사용하려면 추가해야 할 수 있습니다.

public class CointItem : InteractableObject
{
    [Header("동전 설정")]
    public int coinValue = 10;
    public string questTag = "Coin";                   //퀘스트에서 사용할 태그


    // Start is called before the first frame update
    protected void Start() // 👈 'override' 키워드 제거
    {
        // base.Start(); // 👈 이 줄 제거
        objectName = "동전";
        interactionText = "[E] 동전 획득";
        interactionType = InteractionType.Item;
    }

    protected override void CollectItem()
    {

        //퀘스트 매니저에 수집을 알림
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }
        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject, 0.5f);
    }
}