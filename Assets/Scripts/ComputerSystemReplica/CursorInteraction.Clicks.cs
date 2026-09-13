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
        #region Click Input Action + Dpad Slider Input
        InputAction clickAction;
        InputAction sliderValueAction;
        #endregion
        [SerializeField] GraphicRaycaster graphicRaycaster;
        private GameObject m_Draggable;
        private PointerEventData m_PointerData; //exclusively for drag

        #region Click Related Input Action Hooked Methods
        /// <summary>
        /// Hooked to Perform of ClickAction, check <href="CursorInteraction.cs"/>
        /// </summary>
        /// <param name="ctx"></param>
        private void OnClick(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            if (!InputManager.CanAccessCursor) return;
            if (!InputManager.IsUsingGamepad) return;

            Debug.Log("[Dk Log] Clicked");

            var (raycastResults, pointerData, result) = GetValuesTuple(out bool isAnyButton, out _);
            if (isAnyButton)
            {
                Debug.Log($"[DK Supreme Log] {result.name}");
                ExecuteEvents.Execute(result, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }

        private void OnClickStarted(InputAction.CallbackContext ctx)
        {
            if (!ctx.started) return;
            if (!InputManager.CanAccessCursor) return; //shouldn't start if we can't access cursor
            if (!InputManager.IsUsingGamepad) return;
            Debug.Log("[Dk Log] Clicked Started");

            var (_, pointerData, result) = GetValuesTuple(out bool isAnyButton, out bool isSlider);

            if (isSlider)
            {
                //responsible for dragging.
                m_PointerData = pointerData;
                m_Draggable = result;
                ExecuteEvents.Execute(m_Draggable, m_PointerData, ExecuteEvents.pointerDownHandler);
                
                return;
            }
            //if (isAnyButton)
            //    ExecuteEvents.Execute(result,
            //        pointerData,
            //        ExecuteEvents.pointerDownHandler
            //    );
        }

        private void OnClickCanceled(InputAction.CallbackContext ctx)
        {
            if (!ctx.canceled) return;
            if (!InputManager.CanAccessCursor) return;
            if (!InputManager.IsUsingGamepad) return;
            Debug.Log("[DK LOG] Click Canceled");

            var (_, pointerData, result) = GetValuesTuple(out bool isAnyButton, out bool isSlider);

            if (m_Draggable != null)
            {
                ExecuteEvents.Execute(m_Draggable, m_PointerData, ExecuteEvents.pointerUpHandler);
                m_Draggable = null;
                m_PointerData = null;
                return;
            }
            //if (isAnyButton)
            //    ExecuteEvents.Execute(result, pointerData, ExecuteEvents.pointerUpHandler);
        }
        #endregion
        #region Drag Related Stuff
        /// <summary>
        /// When the Drag begins, Required Draggable Component.
        /// </summary>
        /// <param name="ctx"></param>
        private void OnDragStarted(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            if(m_Draggable != null)
            {
                Vector2 value = ctx.ReadValue<Vector2>();
                if (value.y > 0 || value.y < 0)
                    return;

                m_Draggable.GetComponent<IDraggable>().OnDragBegin(value.x);
            }
        }

        private void OnDragReset(InputAction.CallbackContext ctx)
        {
            if(!ctx.canceled) return;

            m_Draggable?.GetComponent<IDraggable>().OnDragReset();
        }
        #endregion

        #region Commented OUT RN
        //private void UpdateDrag()
        //{
        //    if (m_Draggable == null || m_PointerData == null)
        //        return;

        //    m_PointerData.position =
        //        RectTransformUtility.WorldToScreenPoint(
        //            m_Camera,
        //            m_cursor.transform.position
        //        );

        //    ExecuteEvents.Execute(
        //        m_Draggable,
        //        m_PointerData,
        //        ExecuteEvents.dragHandler
        //    );
        //}
        #endregion
        #region Helpers
        /// <summary>
        /// the screen position via the cursor + camera, and a raycast is made to hit at that position by first converting 
        /// it to pointerData, and then raycasting the pointerData which gives a list of raycast hit objects, then determing 
        /// if it is a button or slider via the helpers.
        /// </summary>
        /// <param name="isAnyButton"> outs if the component hit is a button</param>
        /// <param name="isSlider"> outs if the component hit is a slider</param>
        /// <returns>a tuple of RaycastResults, PointerEvent Data and GameObject</returns>
        private (List<RaycastResult>, PointerEventData, GameObject) GetValuesTuple(out bool isAnyButton, out bool isSlider)
        {
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(m_Camera, m_cursor.transform.position);

            PointerEventData pointerData = new(EventSystem.current)
            {
                position = screenPosition
            };

            List<RaycastResult> raycastResults = new();
            graphicRaycaster.Raycast(pointerData, raycastResults);
            if (raycastResults.Count == 0)
            {
                isAnyButton = false;
                isSlider = false;
                return new();
            }
            isSlider = IsDraggable(raycastResults, out GameObject draggable);
            isAnyButton = IsAnyButton(raycastResults, out GameObject result);
            GameObject Gobj = isSlider ? draggable : result;
            return (raycastResults, pointerData, Gobj);
        }

        /// <summary>
        /// Checks if the GraphicRaycaster hit List contains the component which has draggable component.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="draggable">outs the gameobject which is draggable</param>
        /// <returns> boolean value </returns>
        private bool IsDraggable(List<RaycastResult> args, out GameObject draggable)
        {
            foreach(var t in args)
            {
                if(t.gameObject.TryGetComponent<IDraggable>(out IDraggable drag))
                {
                    draggable = t.gameObject;
                    return true;
                }
            }
            draggable = null;
            return false;
        }

        /// <summary>
        /// Checks if the Graphic Raycaster hit List contains button.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="gobj"> outs the gameobject which has button component</param>
        /// <returns>boolean value</returns>
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
        #endregion
    }
}

