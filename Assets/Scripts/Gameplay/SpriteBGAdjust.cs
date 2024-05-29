using UnityEngine;

public class SpriteBGAdjust : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlatformGenerate platformGenerate;
    [SerializeField] private Player player;

    private int screenWidth;
    private int screenHeight;
    private float orthoSize;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer reference not set DUDEEEEEEEEEE");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera reference not set DUDE");
            return;
        }

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        orthoSize = mainCamera.orthographicSize;

        DetectScreenSizeChange();
    }

    private void Update()
    {
        if (Screen.width != screenWidth || Screen.height != screenHeight || mainCamera.orthographicSize != orthoSize)
        {
            DetectScreenSizeChange();
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            orthoSize = mainCamera.orthographicSize;
        }
    }

    private void DetectScreenSizeChange()
    {
        Vector3 transformedScale = spriteRenderer.transform.localScale;
        if (transformedScale.x > 0.52f && transformedScale.y > 0.52f && transformedScale.x < 0.60f && transformedScale.y < 0.60f)
        {
            platformGenerate.IsMainstreamTabletRes();
            player.IsMainstreamTabletRes();

#if UNITY_EDITOR
            Debug.Log("Tablet");
#endif
        }
        else if (transformedScale.x >= 0.60f && transformedScale.y >= 0.60f)
        {
            platformGenerate.IsAppleIpadRes();
            player.IsAppleIpadRes();
#if UNITY_EDITOR
            Debug.Log("iPad");
#endif
        }
        else
        {
            platformGenerate.IsMainstreamPhoneRes();
            player.IsMainstreamPhoneRes();
#if UNITY_EDITOR
            Debug.Log("Smartphone");
#endif
        }
    }
}
