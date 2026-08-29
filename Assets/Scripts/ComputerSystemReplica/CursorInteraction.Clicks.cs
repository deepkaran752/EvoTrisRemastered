using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace babbarversestudios
{
    /// <summary>
    /// Responsible for the UI Interactions.
    /// </summary>
    public partial class CursorInteraction : MonoBehaviour
    {
        #region Click Input Action
        InputAction clickAction;
        #endregion
        [SerializeField] GraphicRaycaster graphicRaycaster;

        private void OnClick(InputAction.CallbackContext ctx)
        {
            Debug.Log("[Dk Log] Clicked");
            if (!ctx.performed) return;

            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(GameManager.Instance.GetCamera(), m_cursor.transform.position);

            PointerEventData pointerData = new(EventSystem.current)
            {
                position = screenPosition
            };

            List<RaycastResult> raycastResults= new();
            graphicRaycaster.Raycast(pointerData, raycastResults);

            if (raycastResults.Count == 0) return;
            bool isAnyButton = IsAnyButton(raycastResults, out GameObject result);
            if (isAnyButton)
            {
                Debug.Log($"[DK Supreme Log] {result.name}");
                ExecuteEvents.Execute(result, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }

        private bool IsAnyButton(List<RaycastResult> args, out GameObject gobj)
        {
            foreach (var t in args)
            {
                if(t.gameObject.TryGetComponent<Button>(out _))
                {
                    gobj = t.gameObject;
                    return true;
                }
            }
            gobj = null;
            return false;
        }
    }
}

