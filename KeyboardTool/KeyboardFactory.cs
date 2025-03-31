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
using System.Diagnostics;
using System.Reflection;

namespace KeyboardTool
{
    public class KeyboardFactory
    {
        private static Dictionary<String, KeyboardHooks> hooksMap = new Dictionary<string, KeyboardHooks>();

        /// <summary>
        /// Register keys to listen
        /// </summary>
        /// <param name="keyCode">keys</param>
        /// <param name="modifierKeyCode">modifierKeys</param>
        /// <param name="keysAction">keyup or keydown</param>
        /// <param name="callback">Key event callback</param>
        /// <returns>The hookId id specified if null return function full name</returns>
        public static String RegisterKey(KeysEnum keyCode, Action<Object, Object> callback, String? hookId = null,
            ModifierKeysEnum modifierKeyCode = ModifierKeysEnum.NONE,
            KeysActionEnum keysAction = KeysActionEnum.KEYDOWN)
        {
            if (hookId == null)
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);
                MethodBase method = stackFrame.GetMethod();
                string className = method.DeclaringType.FullName;
                string methodName = method.Name;
                hookId = $"{className}.{methodName}";
            }
            String key = hookId;
            KeysConfigFile.SaveKey(BitConverter.GetBytes((int)keyCode), BitConverter.GetBytes((int)(modifierKeyCode)), BitConverter.GetBytes((int)keysAction));
            KeyboardHooks hooks = new KeyboardHooks(key, keyCode, modifierKeyCode, keysAction);
            hooks.KeysEventCallback = callback;
            hooks.CallbackError = callbackError;
            hooksMap.Add(key, hooks);
            return key;
        }

        /// <summary>
        /// UnRegister keys listener use specified hookId
        /// </summary>
        /// <param name="hookId"></param>
        public static void UnRegisterKey(String hookId)
        {
            if (hooksMap.ContainsKey(hookId))
            {
                KeyboardHooks hooks = hooksMap[hookId];
                hooks.Dispose();
                hooksMap.Remove(hookId);
            }
        }

        public static String OnKeyPressed(Action<Object> callback)
        {
            String key = "OnKeyPressedListener";
            KeyboardHooks hooks = new KeyboardHooks(key);
            hooks.AllKeysEventCallback = callback;
            hooks.CallbackError = callbackError;
            hooksMap.Add(key, hooks);
            return key;
        }

        private static void callbackError(String hookId)
        {
            if (hooksMap.ContainsKey(hookId))
            {
                hooksMap.Remove(hookId);
            }
        }
    }
}
