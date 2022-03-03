/*MIT License

Copyright (c) [2022] [Michael Lackey]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SlimUI.CursorControllerPro
{
    [RequireComponent(typeof(Slider))]
    public class SliderReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RewiredUIActionMonitor m_ActionMonitor;
        [SerializeField] private Slider m_Slider;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Slider == null) { m_Slider = GetComponent<Slider>(); }

            if (m_ActionMonitor == null) { FindObjectOfType<RewiredUIActionMonitor>(); }

            Debug.Log("Entered Slider Object");

            if (m_ActionMonitor != null)
            {
                m_ActionMonitor.m_SliderObject = GetComponent<Slider>();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Left Slider Object");

            if (m_Slider == null) { m_Slider = GetComponent<Slider>(); }

            if (m_ActionMonitor == null) { FindObjectOfType<RewiredUIActionMonitor>(); }

            if (m_ActionMonitor != null)
            {
                m_ActionMonitor.m_SliderObject = null;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.Monitor;
            }
        }

        private void Start()
        {
            m_ActionMonitor = FindObjectOfType<RewiredUIActionMonitor>();
        }
    }
}