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
    /// Keydown or Keyup
    /// </summary>
    public enum KeysActionEnum
    {
        KEYDOWN = 0x0100,
        KEYUP = 0x0101,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105
    }
}
