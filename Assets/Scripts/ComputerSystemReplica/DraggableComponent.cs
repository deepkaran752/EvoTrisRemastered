using UnityEngine;
using UnityEngine.UI;

namespace babbarversestudios
{
    /// <summary>
    /// Attach it to the Draggabel component u wanna control, ONly works for Slider rn, it can be expanded.
    /// </summary>
    public class DraggableComponent : MonoBehaviour, IDraggable
    {
        [SerializeField] Slider m_Slider;
        [SerializeField] float sliderFillSpeed = 0.25f;

        public void OnDragBegin(float value)
        {
            if (m_Slider)
                m_Slider.value += value * sliderFillSpeed * Time.deltaTime;
        }

        public void OnDragReset()
        {
            //sets the value to the last modified stuff.
            Debug.Log("[DK LOG] Drag reset!");
        }

        private void SliderValueChanged(float value)
        {

        }
    }

    //Responsible for allowing dragging.
    interface IDraggable
    {
        void OnDragBegin(float value);
        void OnDragReset();
    }

}
