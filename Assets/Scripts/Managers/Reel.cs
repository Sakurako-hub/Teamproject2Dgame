using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Reel : MonoBehaviour
{
    // スロットに使う画像
    public Sprite[] symbols;

    // 画像を表示するImage
    private Image image;

    // 回転中かどうか
    private bool isSpinning = false;

    // 画像が切り替わる間隔
    public float changeInterval = 0.1f;

    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (isSpinning)
        {
            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                timer = 0f;

                // ランダムな画像に変更
                int index = Random.Range(0, symbols.Length);
                image.sprite = symbols[index];
            }
        }

        // Spaceキーで回転開始・停止
        if (Keyboard.current.spaceKey.wasPressedThisFrame)

           {
                   isSpinning = !isSpinning;
           }
    }
}