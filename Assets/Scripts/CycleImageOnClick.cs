using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CycleImageOnClick : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Sırayla gösterilecek spritelar (Inspector'da doldur).")]
    public Sprite[] sprites;

    [Tooltip("Başlangıç indeksi (0 = ilk).")]
    public int startIndex = 0;

    [Tooltip("Sona gelince başa dönsün mü?")]
    public bool loop = true;

    [Header("Son görsel geldiğinde açılacak buton")]
    [Tooltip("Başta inactive olmalı (Inspector'da kapat).")]
    public Button revealButton;

    private Image img;
    private int currentIndex;
    private bool lastImageButtonShown = false;

    void Awake()
    {
        img = GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("Bu script bir UI Image üzerinde olmalı.");
            enabled = false;
            return;
        }

        if (sprites != null && sprites.Length > 0)
        {
            currentIndex = Mathf.Clamp(startIndex, 0, sprites.Length - 1);
            img.sprite = sprites[currentIndex];

            // Eğer başlangıç zaten son görsel ise butonu hemen göster.
            TryShowRevealButton();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sprites == null || sprites.Length == 0) return;

        int next = currentIndex + 1;

        if (next >= sprites.Length)
        {
            // Son görseldeyiz.
            if (loop)
            {
                next = 0; // Başa dön
            }
            else
            {
                // Loop yoksa zaten son görseldeyiz, yeniden tıklayınca değişmiyor.
                return;
            }
        }

        currentIndex = next;
        img.sprite = sprites[currentIndex];

        TryShowRevealButton();
    }

    private void TryShowRevealButton()
    {
        if (revealButton == null) return;
        // currentIndex son elemana geldiyse ve henüz açmadıysak aç.
        if (currentIndex == sprites.Length - 1 && !lastImageButtonShown)
        {
            revealButton.gameObject.SetActive(true);
            lastImageButtonShown = true;
        }
    }
}