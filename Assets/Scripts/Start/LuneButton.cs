using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LuneButton : MonoBehaviour, IPointerClickHandler ,IPointerEnterHandler,IPointerExitHandler
{
    private Image image;
    [Range(0.0f, 0.5f)] public float Alpha;
    public Image fadeImage;
    public Color highlightColor;
    public  string goLevel;
    public void Start()
    {
        image = transform.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = Alpha;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OpenLevel(goLevel);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightColor;
    }
    void OpenLevel(string name)
    {
        float waitTime = 0.5f;
        fadeImage.DOFade(1f, waitTime);
        StartCoroutine(Utility.waitOpenLevel(waitTime, name));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}

