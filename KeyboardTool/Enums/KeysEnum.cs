/*
 * Copyright (c) 2024 BugZhang(BugLordl). All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 * Version: v1.0.0
 * Author:  BugZhang(BugLordl)
 * Url:     https://github.com/BugLordI/KeyboardTool
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardTool.Enums
{
    /// <summary>
    /// System VirtualKeyCode Enum
    /// </summary>
    public enum KeysEnum
    {
        #region Letter Keys
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,
        #endregion

        #region Number Keys
        DIGIT0 = 0x30,
        DIGIT1 = 0x31,
        DIGIT2 = 0x32,
        DIGIT3 = 0x33,
        DIGIT4 = 0x34,
        DIGIT5 = 0x35,
        DIGIT6 = 0x36,
        DIGIT7 = 0x37,
        DIGIT8 = 0x38,
        DIGIT9 = 0x39,
        #endregion

        #region Function Keys
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        #endregion

        #region Modifier Keys
        LSHIFT = 0xA0,
        RSHIFT = 0xA1,
        LCONTROL = 0xA2,
        RCONTROL = 0xA3,
        LMENU = 0xA4,
        RMENU = 0xA5,
        LWIN = 0x5B,
        RWIN = 0x5C,
        #endregion

        #region Navigation Keys
        UP = 0x26,
        DOWN = 0x28,
        LEFT = 0x25,
        RIGHT = 0x27,
        HOME = 0x24,
        END = 0x23,
        PAGEUP = 0x21,
        PAGEDOWN = 0x22,
        #endregion

        #region Others
        ESCAPE = 0x1B,
        RETURN = 0x0D,
        SPACE = 0x20,
        BACK = 0x08,
        TAB = 0x09,
        DELETE = 0x2E,
        INSERT = 0x2D,
        NUMLOCK = 0x90,
        SCROLL = 0x91,
        CAPITAL = 0x14,
        PRINTSCREEN = 0x2C,
        PAUSE = 0x13,
        #endregion
        NONE = 0
    }
}
