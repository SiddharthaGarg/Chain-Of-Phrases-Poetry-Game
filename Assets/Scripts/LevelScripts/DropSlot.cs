using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public Sprite slotSprite;
    // private LevelElementsManager levelElementsManager;
    private Vector2 scaleVector = new Vector2(1.2f, 1.2f);
    private Vector2 normalVector = new Vector2(1f, 1f);
    private float tweenTime = 0.1f;


    public void OnDrop(PointerEventData eventData)
    {
        if (//gameObject.transform.childCount == 0 && 
            eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
            eventData.pointerDrag.transform.SetParent(gameObject.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<Image>().sprite = slotSprite;

            //eventData.pointerDrag.GetComponentInChildren<Text>().color = new Vector4(165,56,96,255);
            LeanTween.scale(gameObject, scaleVector, tweenTime).setEaseInQuad();
            LeanTween.scale(gameObject, normalVector, tweenTime).setEaseOutQuad().delay = 0.1f;

        }

    }

}
