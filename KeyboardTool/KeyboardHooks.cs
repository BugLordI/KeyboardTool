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
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace KeyboardTool
{
    internal class KeyboardHooks : IDisposable
    {
        public Action<Object, Object>? KeysEventCallback { private get; set; }
        public Action<String>? CallbackError { private get; set; }

        #region
        private const int WH_KEYBOARD_LL = 13;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

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

        private int keyCode;
        private int modifierKeyCode;
        private int keysAction;
        private String hookId;

        public KeyboardHooks(String hookId, KeysEnum keyCode, KeysEnum modifierKeyCode, KeysActionEnum keysAction)
        {
            this.hookId = hookId;
            this.keyCode = (int)keyCode;
            this.modifierKeyCode = (int)modifierKeyCode;
            this.keysAction = (int)keysAction;
            new ModifierKeysHook();
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        ~KeyboardHooks()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (keyCode != (int)KeysEnum.NONE)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (nCode >= 0 && wParam == (IntPtr)keysAction)
                {
                    Enum.TryParse<KeysActionEnum>(keysAction.ToString(), out KeysActionEnum keysActionEnum);
                    if (modifierKeyCode != (int)KeysEnum.NONE)
                    {
                        if (vkCode == keyCode && ModifierKeys.Key == modifierKeyCode)
                        {
                            Enum.TryParse<KeysEnum>(vkCode.ToString(), out KeysEnum k);
                            Enum.TryParse<ModifierKeysEnum>(ModifierKeys.Key.ToString(), out ModifierKeysEnum mk);
                            KeysEventCallback?.Invoke(new KeysEvent { Key = k, ModifierKey = mk, KeysAction = keysActionEnum }, "");
                        }
                    }
                    else
                    {
                        if (vkCode == keyCode)
                        {
                            Enum.TryParse<KeysEnum>(keyCode.ToString(), out KeysEnum k);
                            KeysEventCallback?.Invoke(new KeysEvent { Key = k, ModifierKey = ModifierKeysEnum.NONE, KeysAction = keysActionEnum }, "");
                        }
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
