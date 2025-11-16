using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSounds : MonoBehaviour, IPointerEnterHandler
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(() => UISoundManager.Instance.PlayClick());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UISoundManager.Instance.PlayHover();
    }
}
