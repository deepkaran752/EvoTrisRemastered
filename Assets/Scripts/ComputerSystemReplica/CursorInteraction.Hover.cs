using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace babbarversestudios{
    //manages the hover state
    public partial class CursorInteraction : MonoBehaviour
    {
        private GameObject m_hoverObject;
        
        private void UpdatePointerHover()
        {
            if (!InputManager.CanAccessCursor)
            {
                //clearing the pointer hover here
                ClearPointerHover();
                return;
            }

            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(m_Camera, m_cursor.transform.position);
            PointerEventData pointerData = new(EventSystem.current)
            {
                position = screenPosition
            };
            List<RaycastResult> raycastResults = new();
            graphicRaycaster.Raycast(pointerData, raycastResults);

            GameObject newHoverObject = null;

            if (raycastResults.Count > 0)
            {
                IsAnyButton(raycastResults, out newHoverObject);
            }

            // Nothing changed.
            if (m_hoverObject == newHoverObject)
                return;

            // Exit the previous button.
            if (m_hoverObject != null)
            {
                ExecuteEvents.Execute(
                    m_hoverObject,
                    pointerData,
                    ExecuteEvents.pointerExitHandler
                );
            }
            
            m_hoverObject = newHoverObject;

            // Enter the new button.
            if (m_hoverObject != null)
            {
                ExecuteEvents.Execute(
                    m_hoverObject,
                    pointerData,
                    ExecuteEvents.pointerEnterHandler
                );
            }

        }

        private void ClearPointerHover()
        {
            if (m_hoverObject == null)
                return;

            PointerEventData pointerData = new(EventSystem.current);

            ExecuteEvents.Execute(
                m_hoverObject,
                pointerData,
                ExecuteEvents.pointerExitHandler
            );

            m_hoverObject = null;
        }
    }
}
