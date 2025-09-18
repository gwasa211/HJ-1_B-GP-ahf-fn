using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("게임 상태")]
    public int playerScore = 0;
    public int itemsColledted = 0;

    [Header("UI 참조")]
    public Text scoreText;
    public Text itemCountText;
    public Text gameStatusText;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Collectltem()
    {
        itemsColledted++;
        Debug.Log($"아이템 수집! ( 총 : {itemsColledted}) 개");

    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "점수 : " + playerScore;
        }

        if(itemCountText != null)
        {
            itemCountText.text = "아이템 : " + itemsColledted + "개";
        }
    }
}
