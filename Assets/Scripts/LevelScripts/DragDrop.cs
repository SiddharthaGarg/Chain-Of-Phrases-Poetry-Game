using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private RectTransform correctSlot;
    [SerializeField] private Transform innerScrollPanel;
    [SerializeField] private Transform outerScrollPanel;
    [SerializeField] private Sprite slotSprite;
    [SerializeField] private AudioSource OnClick = null;
    [SerializeField] private AudioSource OnPlace = null;
    [SerializeField] private AudioSource OnSwitch = null;


    private Vector3 correctPhrasePosition;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 halfScale = new Vector2(0.5f, 0.5f);
    private Vector2 fullScale = new Vector2(1f, 1f);
    private Vector3 prevPos;
    private Sprite phraseSprite;
    private float tweenTime = 0.5f;
    public bool droppedOnSlot;


    private Vector2 scaleVector = new Vector2(1.2f, 1.2f);
    private Vector2 normalVector = new Vector2(1f, 1f);


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        correctPhrasePosition = correctSlot.position;

    }
    private void Start()
    {
        prevPos = rectTransform.position;
        phraseSprite = GetComponent<Image>().sprite;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("onpointerdown");

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        droppedOnSlot = false;
        OnClick.Play();

        transform.SetParent(outerScrollPanel, true);
        LeanTween.scale(gameObject, halfScale, tweenTime).setEaseInExpo();
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;  // if this is not done, the drop event will only be captured by phrase panel
    }                                        // and not slot               

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (!droppedOnSlot)
        {
            transform.SetParent(innerScrollPanel, true);
            rectTransform.position = prevPos;
            GetComponent<Image>().sprite = phraseSprite;
        }

        LeanTween.scale(gameObject, fullScale, tweenTime).setEaseInExpo();
        canvasGroup.alpha = 1f;
        OnSwitch.Play();
        canvasGroup.blocksRaycasts = true;


    }
    public void OnDrop(PointerEventData eventData)
    {
        if (
            eventData.pointerDrag != null && !(gameObject.CompareTag("LeftPanel") &&
                                                    eventData.pointerDrag.CompareTag("RightPanel")) &&
                                                !(gameObject.CompareTag("RightPanel") &&
                                                    eventData.pointerDrag.CompareTag("LeftPanel")))
        {

            eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;

            gameObject.LeanMove(eventData.pointerDrag.GetComponent<DragDrop>().prevPos, 1.5f);
            eventData.pointerDrag.transform.SetParent(innerScrollPanel);
            eventData.pointerDrag.GetComponent<RectTransform>().position = prevPos;
            prevPos = eventData.pointerDrag.GetComponent<DragDrop>().prevPos;
            eventData.pointerDrag.GetComponent<DragDrop>().prevPos = eventData.pointerDrag.GetComponent<RectTransform>().position;

            LeanTween.scale(gameObject, scaleVector, tweenTime - 0.4f).setEaseInQuad();
            LeanTween.scale(gameObject, normalVector, tweenTime - 0.4f).setEaseOutQuad().delay = 0.1f;
            OnPlace.Play();

        }

    }
    public bool IsPositionCorrect()
    {


        if (rectTransform.position == correctPhrasePosition)
        {
            //Debug.Log(correctPhrasePosition.y);
            return true;
        }
        else
        {
            // Debug.Log(rectTransform.position.y);
            return false;
        }
    }

}
