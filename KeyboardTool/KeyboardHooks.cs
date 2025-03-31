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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardTool
{
    internal class KeyboardHooks : IDisposable
    {
        public Action<Object, Object>? KeysEventCallback { private get; set; }

        public Action<Object>? AllKeysEventCallback { private get; set; }

        public Action<String>? CallbackError { private get; set; }

        #region
        private const int WH_KEYBOARD_LL = 13;

        private LowLevelKeyboardProc _keys_proc;
        private IntPtr _keys_hook_Id = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        private String id;
        private int keyCode;
        private int modifierKeyCode;
        private int keysAction;

        public KeyboardHooks(String hookId, KeysEnum keyCode, ModifierKeysEnum modifierKeyCode, KeysActionEnum keysAction)
        {
            this.id = hookId;
            this.keyCode = (int)keyCode;
            this.modifierKeyCode = (int)modifierKeyCode;
            this.keysAction = (int)keysAction;
            _keys_proc = KeysHookCallback;
            _keys_hook_Id = SetHook(_keys_proc);
        }

        public KeyboardHooks(String hookId)
        {
            this.id = hookId;
            _keys_proc = KeysHookCallback;
            _keys_hook_Id = SetHook(_keys_proc);
        }

        ~KeyboardHooks()
        {
            UnhookWindowsHookEx(_keys_hook_Id);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_keys_hook_Id);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr KeysHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            KeysActionEnum keysActionEnum = ((int)wParam).ParseToEnum<KeysActionEnum>();
            KeysEnum key = vkCode.ParseToEnum<KeysEnum>();
            ModifierKeysEnum mk = ModifierKeysMap.GetModifierKeysEnum(vkCode);
            if (key != KeysEnum.NONE)
            {
                if (keyCode != (int)KeysEnum.NONE)
                {
                    if (nCode >= 0 && wParam == (IntPtr)keysAction)
                    {
                        if (modifierKeyCode != (int)KeysEnum.NONE)
                        {
                            if (vkCode == keyCode && (int)ModifierKeys.Key == modifierKeyCode)
                            {
                                KeysEventCallback?.Invoke(new KeysEvent { Key = key, ModifierKey = ModifierKeys.Key, KeysAction = keysActionEnum }, "");
                            }
                        }
                        else
                        {
                            if (vkCode == keyCode)
                            {
                                KeysEventCallback?.Invoke(new KeysEvent { Key = key, ModifierKey = ModifierKeysEnum.NONE, KeysAction = keysActionEnum }, "");
                            }
                        }
                    }
                }
            }
            else if (mk != ModifierKeysEnum.NONE)
            {
                if (wParam == (IntPtr)KeysActionEnum.KEYDOWN || wParam == (IntPtr)KeysActionEnum.WM_SYSKEYDOWN)
                {
                    ModifierKeys.Key |= mk;
                }
                else
                {
                    ModifierKeys.Key &= ~mk;
                }
            }
            else
            {
                CallbackError?.Invoke(id);
            }
            AllKeysEventCallback?.Invoke(new KeysEvent { Key = key, ModifierKey = ModifierKeys.Key, KeysAction = keysActionEnum });
            return CallNextHookEx(_keys_hook_Id, nCode, wParam, lParam);
        }
    }
}
