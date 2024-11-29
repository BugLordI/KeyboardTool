/*
 * Copyright (c) 2024 BugZhang(BugLordl). All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 * Version: v1.0.0
 * Author:  BugZhang(BugLordl)
 * Url:     https://github.com/BugLordI/KeyboardTool
 */
using KeyboardTool.Enums;
using KeyboardTool.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardTool
{
    public class Keyboard
    {
        /// <summary>
        /// Register keys to listen
        /// </summary>
        /// <param name="keyCode">keys</param>
        /// <param name="modifierKeyCode">modifierKeys</param>
        /// <param name="keysAction">keyup or keydown</param>
        public void RegisterKey(KeysEnum keyCode, KeysEnum? modifierKeyCode = null, KeysActionEnum keysAction = KeysActionEnum.WM_KEYDOWN)
        {
            KeysConfigFile.SaveKey(BitConverter.GetBytes((int)keyCode), BitConverter.GetBytes((int)(modifierKeyCode ?? 0)), BitConverter.GetBytes((int)keysAction));
            KeyboardHooks hooks = new KeyboardHooks(keyCode, KeysEnum.LCONTROL, keysAction);
        }

        public static void UnRegisterKey()
        {
            
        }
    }
}
