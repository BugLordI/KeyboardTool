/*
 * Copyright (c) 2024 BugZhang(BugLordl). All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 * Version: v1.0.0
 * Author:  BugZhang(BugLordl)
 * Url:     https://github.com/BugLordI/KeyboardTool
 */

using System;

namespace KeyboardTool.Enums
{
    [Flags]
    public enum ModifierKeysEnum
    {
        NONE = 0,        
        LSHIFT = 1 << 0,   
        RSHIFT = 1 << 1,   
        LCONTROL = 1 << 2, 
        RCONTROL = 1 << 3, 
        LALT = 1 << 4,     
        RALT = 1 << 5,    
        LWIN = 1 << 6,   
        RWIN = 1 << 7
    }
}
