using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[SerializeField] GameObject PrefabToInstantiate;
    [SerializeField] RectTransform UIDragElement;

    [SerializeField] RectTransform Canvas;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private Vector2 originalPosition;
    public Material instanceMaterial;


    private void Start()
    {
        UIDragElement = this.GetComponent<RectTransform>();
        Canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        originalPosition = UIDragElement.localPosition;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("begin");
        originalPanelLocalPosition = UIDragElement.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, data.position, data.pressEventCamera,
            out originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("drag");
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, data.position, data.pressEventCamera,
                out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            UIDragElement.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }
        //ClampToArea();
    }

    public IEnumerator MoveItem(RectTransform rectTransform, Vector2 targetPosition, float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = rectTransform.localPosition;
        while (elapsedTime < duration)
        {
            rectTransform.localPosition = Vector2.Lerp(startingPos, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rectTransform.localPosition = targetPosition;
    }

    // drag area clamp
    private void ClampToArea()
    {
        Vector3 pos = UIDragElement.localPosition;

        Vector3 minPosition = Canvas.rect.min - UIDragElement.rect.min;
        Vector3 maxPosition = Canvas.rect.max - UIDragElement.rect.max;

        pos.x = Mathf.Clamp(UIDragElement.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(UIDragElement.localPosition.y, minPosition.y, maxPosition.y);

        UIDragElement.localPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end");
        StartCoroutine(MoveItem(UIDragElement, originalPosition, 0.5f));
        DressupCharacter();
    }

    private void DressupCharacter()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform topTransform = hit.collider.transform.Find("Tops");
            if (topTransform != null)
            {
                var avatarMesh = topTransform.GetComponent<SkinnedMeshRenderer>();
                if (avatarMesh != null)
                {
                    avatarMesh.material = instanceMaterial;
                    Debug.Log("başarılı");
                }
                else
                {
                    Debug.Log("Top objesinde SkinnedMeshRenderer bulunamadı");
                }
            }
            else
            {
                Debug.Log("Top adında bir obje bulunamadı");
            }
        }
        else
        {
            Debug.Log("karakterin üstüne sürüklenmedi");
            return;
        }
    }
}