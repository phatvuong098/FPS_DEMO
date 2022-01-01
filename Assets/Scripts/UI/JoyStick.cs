using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class JoyStick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform knod;
    public RectTransform parentRect;
    public Vector2 moveDir;
    private Vector2 limitSize;
    public float distanceKnod = 128f;
    void Start()
    {
        limitSize = parentRect.sizeDelta;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 location = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, eventData.position, null, out location);

        moveDir.x = location.x / (limitSize.x * 0.5f);
        moveDir.y = location.y / (limitSize.y * 0.5f);

        moveDir.x = Mathf.Clamp(moveDir.x, -1, 1);
        moveDir.y = Mathf.Clamp(moveDir.y, -1, 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir_ = moveDir + PlayerInput.moveDir;

        if (moveDir_.magnitude >= 1)
        {
            float angle = Mathf.Atan2(moveDir_.y, moveDir_.x);// * Mathf.Rad2Deg;
            float x = distanceKnod * Mathf.Cos(angle);
            float y = distanceKnod * math.sin(angle);
            knod.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            knod.anchoredPosition = moveDir_ * distanceKnod;
        }

        PlayerInput.moveDir += moveDir;
    }
}
