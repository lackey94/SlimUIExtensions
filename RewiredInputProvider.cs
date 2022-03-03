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

namespace SlimUI.CursorControllerPro.InputSystem
{
    public class RewiredInputProvider : MonoBehaviour, IInputProvider
    {
        #region Rewired Variables

        /// <summary>
        /// Rewired Controller Object
        /// </summary>
        [SerializeField] private Controller m_Controller;
        /// <summary>
        /// Rewired Player Object
        /// </summary>
        [SerializeField] private Player m_Player;
        /// <summary>
        /// Player ID - Assigned player in the Rewired Player List, Change to designated player
        /// </summary>
        [SerializeField] private int m_PlayerID = 0;
        /// <summary>
        /// Constant case for if mappings need to be monitored
        /// </summary>
        private bool m_SkipDisabledMaps = true;
        /// <summary>
        /// Rewired Controller Object
        /// </summary>
        public Controller GetController { get => m_Controller; }
        /// <summary>
        /// Rewired Player Object
        /// </summary>
        public Player GetPlayer { get => m_Player; }
        #endregion Rewired Variables

        #region Initializers

        public void Start()
        {
            CheckReferences();
        }

        private void CheckReferences()
        {
            if (m_Player == null) { m_Player = ReInput.players.GetPlayer(m_PlayerID); }
            if (m_Player != null && m_Controller == null) { m_Controller = m_Player.controllers.GetLastActiveController(); }
        }

        #endregion Initializers

        #region InputProvider

        private float m_XAxis = 0f, m_YAxis = 0f;

        /// <summary>
        /// Gets the last position of the mouse on the screen
        /// </summary>
        /// <returns></returns>
        public Vector2 GetAbsolutePosition()
        {
            return ReInput.controllers.Mouse.screenPosition;
        }

        /// <summary>
        /// Returns the active input type
        /// </summary>
        /// <returns></returns>
        public InputType GetActiveInputType()
        {
            CheckReferences();

            if (m_Controller != null)
            {
                switch (m_Controller.type)
                {
                    default:
                    case ControllerType.Keyboard:
                    case ControllerType.Mouse:
                        return InputType.MouseAndKeyboard;

                    case ControllerType.Joystick:
                    case ControllerType.Custom:
                        return InputType.Gamepad;
                }
            }
            else return InputType.MouseAndKeyboard;
        }

        /// <summary>
        /// Returns the relative movement of the cursor, returns Vector.Zero if controller is null
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Vector2 GetRelativeMovement(GamepadPlayerNum player = GamepadPlayerNum.One)
        {
            CheckReferences();

            m_Player = ReInput.players.GetPlayer((int)player);
            if (m_Player != null)
            {
                m_Controller = m_Player.controllers.GetLastActiveController();
                if (m_Controller != null)
                {
                    if (m_Controller.type == ControllerType.Joystick)
                    {
                        m_XAxis = m_Player.GetAxis(MasterKeyReferences.ActionUI_Horizontal);
                        m_YAxis = m_Player.GetAxis(MasterKeyReferences.ActionUI_Vertical);
                        return new Vector2(m_XAxis, m_YAxis);
                    }
                    else return Vector2.zero;
                }

                else return Vector2.zero;   //Returns 0 value if proper gamepad reference isn't found
            }
            else return Vector2.zero;   //Return 0 value if Rewired Player isn't found
        }

        /// <summary>
        /// Returns whether the "Submit" Action buton was pressed, this is a looped event
        /// </summary>
        /// <returns></returns>
        public bool GetSubmitWasPressed()
        {
            CheckReferences();

            return m_Player.GetButtonDown(MasterKeyReferences.ActionUI_Submit);
        }

        /// <summary>
        /// Returns whether the "Submit" Action button is in the "Up State", this is a looped event
        /// </summary>
        /// <returns></returns>
        public bool GetSubmitWasReleased()
        {
            CheckReferences();

            return m_Player.GetButtonUp(MasterKeyReferences.ActionUI_Submit);
        }

        #endregion InputProvider
    }
}