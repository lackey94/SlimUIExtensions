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
using UnityEngine.UI;

namespace SlimUI.CursorControllerPro
{
    public class ScrollBlock : MonoBehaviour
    {
        [SerializeField] private RewiredUIActionMonitor m_ActionMonitor;
        [SerializeField] private Scrollbar m_Scrollbar;
        [SerializeField] private ScrollbarReceiver m_ScrollbarReceiver;

        public void Block()
        {
            Debug.Log($"Active in Hierarchy {m_Scrollbar.gameObject.activeInHierarchy}");
            if (!m_Scrollbar.gameObject.activeInHierarchy && m_ActionMonitor != null)
            {
                Debug.Log("Freezing Scroll");
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.FreezeScrolling;
            }
        }

        public void OnDisable()
        {
            if (m_ActionMonitor == null) { m_ActionMonitor = FindObjectOfType<RewiredUIActionMonitor>(); }
            if (!m_Scrollbar.IsActive() && m_ActionMonitor != null)
                m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.Monitor;

            if (m_ScrollbarReceiver.IsScrollbarInstanced)
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
            else { m_ActionMonitor.ActionType = RewiredUIActionMonitor.CurrentActionType.Monitor; }
        }

        public void Start()
        {
            if (m_ActionMonitor == null) { m_ActionMonitor = FindObjectOfType<RewiredUIActionMonitor>(); }
        }
    }
}