using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Reel reel1;
    public Reel reel2;
    public Reel reel3;

    // スロット画面
    public GameObject slotGame;

    // 親
    public GameObject parentImage;

    // 怒っている親の画像
    public Image parentAngryImage;
    public Sprite mother1;
    public Sprite mother2;
    public Sprite mother3;

    // 親が来る回数
    private int parentCount = 0;

    // 親が来る最大回数
    private int maxParentCount = 6;

    // 親が来るまでの待ち時間
    public float parentInterval = 20f;

    // スロットのレベル
    private int slotLevel = 1;
    
    // 親の怒りレベル
    private int angerLevel = 0;
    
    // 怒りレベルの最大値
    private int maxAngerLevel = 3;

    // UI
    public TMPro.TextMeshProUGUI angerText;
    public TMPro.TextMeshProUGUI slotLvText;
    

    void Start()
    {
        // ゲーム開始時は親を非表示
        parentImage.SetActive(false);
        parentAngryImage.gameObject.SetActive(false);
    
        // UIを初期表示
        UpdateLevelUI();
    
        // 親が来る処理を開始
        StartCoroutine(ParentRoutine());
    }
    
    void Update()
    {
        // Spaceキー → スロット開始
        // Slot画面が表示されている時だけ動く
        if (slotGame.activeSelf &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(SlotStart());
        }

        // Enterキー → スロット画面ON/OFF
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            slotGame.SetActive(!slotGame.activeSelf);
        }

        // Pキー → 親の表示/非表示（テスト用）
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            parentImage.SetActive(!parentImage.activeSelf);
        }
    }


    // ==============================
    // スロット
    // ==============================

    IEnumerator SlotStart()
    {
        // 3つ同時に回す
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        // 1秒後に1つ目停止
        yield return new WaitForSeconds(1.0f);
        reel1.StopSpin();

        // 0.5秒後に2つ目停止
        yield return new WaitForSeconds(0.5f);
        reel2.StopSpin();

        // 0.5秒後に3つ目停止
        yield return new WaitForSeconds(0.5f);
        reel3.StopSpin();

        // 結果判定
        CheckResult();
    }


    void CheckResult()
    {
        Sprite symbol1 = reel1.GetCurrentSymbol();
        Sprite symbol2 = reel2.GetCurrentSymbol();
        Sprite symbol3 = reel3.GetCurrentSymbol();

        if (symbol1 == symbol2 && symbol2 == symbol3)
        {
            Debug.Log("🎉 当たり！");
        }
        else
        {
            Debug.Log("❌ ハズレ！");
        }
    }


    // ==============================
    // 親システム
    // ==============================

    IEnumerator ParentRoutine()
    {
        while (parentCount < maxParentCount)
        {
            // 次の親が来るまで待つ
            yield return new WaitForSeconds(parentInterval);
    
            parentCount++;
    
            Debug.Log("親が来る5秒前！");
    
            // ガラス越しに親を表示
            parentImage.SetActive(true);
    
            // ここに後で足音SEを入れる！
    
            // 5秒待つ
            yield return new WaitForSeconds(5f);
    
            // ガラス越しの親を消す
            parentImage.SetActive(false);
    
            // 親が部屋に来る
            ParentComing();
    
            // 3秒間、親がいる
            yield return new WaitForSeconds(3f);
    
            // 怒っている親を消す
            parentAngryImage.gameObject.SetActive(false);
        }
    
        Debug.Log("親の登場回数6回終了！");
    }


    void ParentComing()
    {
        Debug.Log("👩 親が部屋に来た！");
    
        // ゲーム画面が開いているか判定
        if (slotGame.activeSelf)
        {
            // アウト
            angerLevel++;
    
            Debug.Log("❌ アウト！親に見つかった！");
            Debug.Log("親の怒りLv：" + angerLevel);
    
            // 怒りレベルに応じて親の画像を変更
            if (angerLevel <= 1)
            {
                parentAngryImage.sprite = mother1;
            }
            else if (angerLevel == 2)
            {
                parentAngryImage.sprite = mother2;
            }
            else
            {
                parentAngryImage.sprite = mother3;
            }
    
            // 怒りLvが最大になったらゲームオーバー
            if (angerLevel >= maxAngerLevel)
            {
                Debug.Log("💥 GAME OVER！親の怒りがMAX！");
            }
        }
        else
        {
            // セーフ
            slotLevel++;
    
            Debug.Log("⭕ セーフ！親に見つからなかった！");
            Debug.Log("🎰 スロットLv：" + slotLevel);
        }
    
        // 怒っている親を表示
        parentAngryImage.gameObject.SetActive(true);
    
        // UIを更新
        UpdateLevelUI();    
    }

    void UpdateLevelUI()
    {
        // 怒りゲージ
        string angerGauge = "";
    
        for (int i = 0; i < 6; i++)
        {
            if (i < angerLevel)
            {
                angerGauge += "■";
            }
            else
            {
                angerGauge += "□";
            }
        }
    
        angerText.text = " 怒り：" + angerGauge;
    
    
        // スロット当たり確率
        int slotProbability = 20 + (slotLevel - 1) * 10;
    
        slotLvText.text = "SLOT 当たる確率 " + slotProbability + "%";
    }
}