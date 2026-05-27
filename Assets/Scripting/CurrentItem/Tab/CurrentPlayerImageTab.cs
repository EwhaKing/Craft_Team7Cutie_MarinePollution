using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerImageTab : MonoBehaviour
{
    [Header("이미지를 표시할 UI Image")]
    [SerializeField] private Image targetImage;

    [Header("연결 스크립트")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AttachedEquippmentManager equipmentManager;

    [Header("이미지 경로")]
    [SerializeField] private string defaultImagePath;      // Ground
    [SerializeField] private string conditionImagePathA;   // I_Normal
    [SerializeField] private string conditionImagePathB;   // I_Suit

    private string currentPath = "";

    private void Start()
    {
        UpdateCurrentImage();
    }

    private void Update()
    {
        UpdateCurrentImage();
    }

    public void UpdateCurrentImage()
    {
        if (playerMovement == null)
        {
            SetImageIfChanged(defaultImagePath);
            return;
        }

        // Ground 상태일 때 기본 이미지
        if (playerMovement.Ground)
        {
            SetImageIfChanged(defaultImagePath);
        }
        // Sea 상태이고, 107번 잠수복을 장착했을 때 I_Suit 이미지
        else if (
            playerMovement.Sea &&
            equipmentManager != null &&
            equipmentManager.IsEquipped("107")
        )
        {
            SetImageIfChanged(conditionImagePathB);
        }
        // Sea 상태이지만 잠수복이 없을 때 I_Normal 이미지
        else if (playerMovement.Sea)
        {
            SetImageIfChanged(conditionImagePathA);
        }
        // 그 외에는 기본 이미지
        else
        {
            SetImageIfChanged(defaultImagePath);
        }
    }

    private void SetImageIfChanged(string imagePath)
    {
        if (currentPath == imagePath)
            return;

        currentPath = imagePath;
        LoadImageFromPath(imagePath);
    }

    public void LoadImageFromPath(string imagePath)
    {
        if (targetImage == null)
        {
            Debug.LogWarning("[CurrentPlayerImageTab] targetImage가 연결되지 않았습니다.", this);
            return;
        }

        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogWarning("[CurrentPlayerImageTab] imagePath가 비어 있습니다.", this);
            targetImage.sprite = null;
            targetImage.enabled = false;
            return;
        }

        Sprite loadedSprite = Resources.Load<Sprite>(imagePath);

        if (loadedSprite == null)
        {
            Debug.LogWarning("[CurrentPlayerImageTab] 이미지를 찾지 못했습니다. 경로: " + imagePath, this);
            targetImage.sprite = null;
            targetImage.enabled = false;
            return;
        }

        targetImage.sprite = loadedSprite;
        targetImage.enabled = true;
        targetImage.preserveAspect = true;

        Debug.Log("[CurrentPlayerImageTab] 이미지 적용 완료: " + imagePath, this);
    }

    public void SetImageByPath(string imagePath)
    {
        SetImageIfChanged(imagePath);
    }

    public void ClearImage()
    {
        currentPath = "";

        if (targetImage == null)
            return;

        targetImage.sprite = null;
        targetImage.enabled = false;
    }
}