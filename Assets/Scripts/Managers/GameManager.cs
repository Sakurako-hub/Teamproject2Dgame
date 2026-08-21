using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Reel reel1;
    public Reel reel2;
    public Reel reel3;

    // スロット画面
    public GameObject slotGame;

    void Update()
    {
        // Spaceキー → スロット開始
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(SlotStart());
        }

        // Shiftキー → スロット画面ON/OFF
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            slotGame.SetActive(!slotGame.activeSelf);
        }
    }

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
}