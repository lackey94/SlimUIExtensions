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
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Scrollbar m_Scrollbar;
        [SerializeField] private RewiredUIActionMonitor m_ActionMonitor;
        [SerializeField] private bool m_IsScrollbarInstanced;
        [SerializeField] private ScrollBlock m_Scrollblock;
        public bool IsScrollbarInstanced { get => m_IsScrollbarInstanced; }
        public void CheckReferences()
        {
            if (m_Scrollbar == null) { m_Scrollbar = GetComponent<Scrollbar>(); }
            if (m_ActionMonitor == null) { m_ActionMonitor = FindObjectOfType<RewiredUIActionMonitor>(); }
        }

        public void OnDisable()
        {
            CheckReferences();
            if (m_ActionMonitor == null) { return; }

            if (m_Scrollblock != null)
            {
                if (m_Scrollblock.gameObject.activeInHierarchy)
                {
                    m_Scrollblock.Block();
                }
            }
        }

        public void OnEnable()
        {
            CheckReferences();
            if (m_ActionMonitor == null) { return; }

            if (m_IsScrollbarInstanced)
            {
                m_ActionMonitor.InteractableScrollTarget = m_Scrollbar;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.InteractableScrollTarget;
            }
            else
            {
                m_ActionMonitor.m_SceneScrollTarget = m_Scrollbar;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.StaticScrollbar;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CheckReferences();
            if (m_ActionMonitor == null) { return; }

            if (m_IsScrollbarInstanced)
            {
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.InteractableScrollTarget;
                m_ActionMonitor.InteractableScrollTarget = m_Scrollbar;
            }
            else
            {
                m_ActionMonitor.m_SceneScrollTarget = m_Scrollbar;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.StaticScrollbar;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CheckReferences();
            if (m_ActionMonitor == null) { return; }

            if (m_IsScrollbarInstanced)
            {
                m_ActionMonitor.InteractableScrollTarget = null;
                if (m_ActionMonitor.m_SceneScrollTarget != null)
                {
                    m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.StaticScrollbar;
                }
                else
                {
                    m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.Monitor;
                }
            }
        }

        public void Start()
        {
            CheckReferences();

            if (m_ActionMonitor == null) { return; }

            if (m_IsScrollbarInstanced)
            {
                m_ActionMonitor.InteractableScrollTarget = m_Scrollbar;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.InteractableScrollTarget;
            }
            else
            {
                m_ActionMonitor.m_SceneScrollTarget = m_Scrollbar;
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.StaticScrollbar;
            }
        }
    }
}