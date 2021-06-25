using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public UnityAction<GameBoardSlot, GameBoardSlot> OnSlotChange;
	public Image containerImage;
	public Image iconImage;
	private Color normalColor = Color.white;
	public Color highlightColor = Color.yellow;
	public bool dragOnSurfaces = true;

	private Dictionary<int, GameObject> m_DraggingIcons = new Dictionary<int, GameObject>();
	private Dictionary<int, RectTransform> m_DraggingPlanes = new Dictionary<int, RectTransform>();

    public void Awake()
    {
		iconImage = GetComponent<Image>();
		containerImage = transform.parent.GetComponent<Image>();
	}

    public void OnBeginDrag(PointerEventData eventData)
	{
		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;

		if (iconImage.sprite == null)
			return;

		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
		m_DraggingIcons[eventData.pointerId] = new GameObject("icon");

		m_DraggingIcons[eventData.pointerId].transform.SetParent(canvas.transform, false);
		m_DraggingIcons[eventData.pointerId].transform.SetAsLastSibling();

		var image = m_DraggingIcons[eventData.pointerId].AddComponent<Image>();
		// The icon will be under the cursor.
		// We want it to be ignored by the event system.
		var group = m_DraggingIcons[eventData.pointerId].AddComponent<CanvasGroup>();
		group.blocksRaycasts = false;

		image.sprite = GetComponent<Image>().sprite;
		image.SetNativeSize();

		if (dragOnSurfaces)
			m_DraggingPlanes[eventData.pointerId] = transform as RectTransform;
		else
			m_DraggingPlanes[eventData.pointerId] = canvas.transform as RectTransform;

		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (iconImage.sprite == null)
			return;

		if (m_DraggingIcons[eventData.pointerId] != null)
			SetDraggedPosition(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (m_DraggingIcons[eventData.pointerId] != null)
			Destroy(m_DraggingIcons[eventData.pointerId]);

		m_DraggingIcons[eventData.pointerId] = null;
		eventData.pointerDrag = null;
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			m_DraggingPlanes[eventData.pointerId] = eventData.pointerEnter.transform as RectTransform;

		var rt = m_DraggingIcons[eventData.pointerId].GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlanes[eventData.pointerId], eventData.position, eventData.pressEventCamera, out globalMousePos))
		{
			rt.position = globalMousePos;
			rt.rotation = m_DraggingPlanes[eventData.pointerId].rotation;
		}
	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;

		var t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}


	public void OnEnable()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}

	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;

		if (containerImage == null || iconImage == null)
			return;

		Sprite dropSprite = GetOriginalSprite(data);
		if (dropSprite != null)
		{
			var oldSlot = data.pointerDrag.GetComponent<GameBoardSlot>();
			var newSlot = gameObject.GetComponent<GameBoardSlot>();

			Sprite oldSprite = iconImage.sprite;
			iconImage.sprite = dropSprite;
			Color tmp = normalColor;
			tmp.a = 1f;
			iconImage.color = tmp;
			containerImage.color = normalColor;

			// Swap sprites.
			var oldImage = data.pointerDrag.GetComponent<Image>();
			oldImage.sprite = oldSprite;
			if (oldImage.sprite == null) {
				tmp.a = 0f;
				oldImage.color = tmp;
			}

			OnSlotChange?.Invoke(oldSlot, newSlot);
		}
	}

	//Highlights Container Image
	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;

		Sprite dropSprite = GetOriginalSprite(data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;

		containerImage.color = normalColor;
	}

	private Sprite GetOriginalSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;

		var dragMe = originalObj.GetComponent<Drag>();
		if (dragMe == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;

		return srcImage.sprite;
	}
}
