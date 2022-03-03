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

using Rewired;
using YOURNAME.Client.REFERENCE; // Static Key Reference Class
using UnityEngine;
using UnityEngine.UI;

namespace SlimUI.CursorControllerPro
{
    public class RewiredUIActionMonitor : MonoBehaviour
    {
        /// <summary>
        /// Current action taking place on the user interface
        /// </summary>
        public CurrentActionType ActionType = CurrentActionType.Monitor;
        /// <summary>
        /// Currently active scrollbar, use this for instanced scrollbars
        /// </summary>
        public Scrollbar InteractableScrollTarget;
        /// <summary>
        /// Currently attached scrollbar in the scene, use this for non-instanced scroll bars or for top-layer bodied scrollbars
        /// </summary>
        public Scrollbar m_SceneScrollTarget;
        /// <summary>
        /// Target slider object in scene the cursor is currently hovering
        /// </summary>
        public Slider m_SliderObject;
        /// <summary>
        /// Intensity of vertical scroll multiplier
        /// </summary>
        [SerializeField] private Controller m_Controller;
        /// <summary>
        /// Currently active player
        /// </summary>
        [SerializeField] private Player m_Player;
        /// <summary>
        /// Active player ID for Player object
        /// </summary>
        [SerializeField] private int m_PlayerID = 0;
        /// <summary>
        /// Action Types - Determins how the UI is handled
        /// </summary>
        public enum CurrentActionType
        {
            Monitor,
            SliderInteract,
            StaticScrollbar,
            InteractableScrollTarget,
            FreezeScrolling
        }
        /// <summary>
        /// Intensity of vertical scroll multiplier
        /// </summary>
        public Controller GetController { get => m_Controller; }
        /// <summary>
        /// Currently active player
        /// </summary>
        public Player GetPlayer { get => m_Player; }

        /// <summary>
        /// Performs a repeated action, core method that handles monitoring all UI activity
        /// </summary>
        public void DoAction()
        {
            m_Controller = m_Player.controllers.GetLastActiveController();

            if (m_Controller.type == ControllerType.Joystick)
            {
                Debug.Log($"Current Action Type {ActionType}");

                switch (ActionType)
                {
                    case CurrentActionType.Monitor:
                        if (m_SceneScrollTarget != null)
                        {
                            if (m_SceneScrollTarget.IsActive())
                                ActionType = CurrentActionType.StaticScrollbar;
                            else
                            {
                                Debug.Log("Scene Scroll Target isn't Active, clearing");
                                m_SceneScrollTarget = null;
                            }
                        }
                        else if (m_SliderObject != null)
                        {
                            ActionType = CurrentActionType.SliderInteract;
                        }
                        break;

                    case CurrentActionType.SliderInteract:
                    {
                        if (m_SliderObject != null)
                        {
                            if (m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollHorizontal) > 0)
                                Debug.Log("Increasing Slider");
                            if (m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollHorizontal) < 0)
                                Debug.Log("Decreasing Slider");

                            if (m_SceneScrollTarget != null)
                            {
                                if (Mathf.Abs(m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollVertical)) >= .7f && Mathf.Abs(m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollHorizontal)) < .2)
                                {
                                    ActionType = CurrentActionType.StaticScrollbar;
                                }
                            }

                            float sliderMin = m_SliderObject.minValue;
                            float sliderMax = m_SliderObject.maxValue;
                            float sliderDif = Mathf.Abs(sliderMin - sliderMax);
                            float currentAxisValue = m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollHorizontal);
                            float addValue = 0;
                            if (m_SliderObject.wholeNumbers)
                                addValue = Mathf.RoundToInt(currentAxisValue * (sliderDif * .03f));
                            else { addValue = (currentAxisValue * (sliderDif * .03f)); }

                            Debug.Log($"Value: {addValue}");

                            float newValue = m_SliderObject.value + addValue;
                            if (newValue >= m_SliderObject.minValue || newValue <= m_SliderObject.maxValue) { m_SliderObject.value = newValue; }
                            else if (newValue <= m_SliderObject.minValue) { m_SliderObject.value = m_SliderObject.minValue; }
                            else if (newValue >= m_SliderObject.maxValue) { m_SliderObject.value = m_SliderObject.maxValue; }
                        }

                        break;
                    }

                    case CurrentActionType.StaticScrollbar:
                    {
                        if (m_SceneScrollTarget != null)
                        {
                            if (m_SliderObject != null)
                            {
                                if (Mathf.Abs(m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollHorizontal)) >= 0.3 && Mathf.Abs(m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollVertical)) < .6f)
                                {
                                    ActionType = CurrentActionType.SliderInteract;
                                }
                            }
                            m_SceneScrollTarget.value += m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollVertical) / 20;
                        }
                        break;
                    }

                    case CurrentActionType.InteractableScrollTarget:
                    {
                        if (InteractableScrollTarget != null)
                        {
                            InteractableScrollTarget.value += m_Player.GetAxis(MasterKeyReferences.ActionUI_ScrollVertical) / 20;
                        }
                        break;
                    }
                }
            }
        }

        public void OnEnable()
        {
            ActionType = CurrentActionType.Monitor;
        }

        public void Start()
        {
            if (m_Player == null) { m_Player = ReInput.players.GetPlayer(m_PlayerID); }
            if (m_Player != null && m_Controller == null) { m_Controller = m_Player.controllers.GetLastActiveController(); }
        }

        public void Update()
        {
            if (m_Player != null && m_Controller != null)
                DoAction();
            else
            {
                if (m_Player == null) { m_Player = ReInput.players.GetPlayer(m_PlayerID); }
                if (m_Player != null && m_Controller == null) { m_Controller = m_Player.controllers.GetLastActiveController(); }
            }
        }
    }
}