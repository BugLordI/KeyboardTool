/*
 * Copyright (c) 2024 BugZhang(BugLordl). All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 * Version: v1.0.0
 * Author:  BugZhang(BugLordl)
 * Url:     https://github.com/BugLordI/KeyboardTool
 */
using KeyboardTool.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardTool
{
    public class KeysEvent
    {
        public KeysEnum Key { get; set; }
        public ModifierKeysEnum ModifierKey { get; set; }
        public KeysActionEnum KeysAction { get; set; }
    }
}
