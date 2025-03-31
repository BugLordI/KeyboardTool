using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardTool.Enums
{
    internal class ModifierKeysMap
    {
        private static Dictionary<int, ModifierKeysEnum> modifierKeysMap = new Dictionary<int, ModifierKeysEnum>()
        {
           { 0xA0, ModifierKeysEnum.LSHIFT },
           { 0xA1, ModifierKeysEnum.RSHIFT },
           { 0xA2, ModifierKeysEnum.LCONTROL },
           { 0xA3, ModifierKeysEnum.RCONTROL },
           { 0xA4, ModifierKeysEnum.LALT },
           { 0xA5, ModifierKeysEnum.RALT },
           { 0x5B, ModifierKeysEnum.LWIN },
           { 0x5C, ModifierKeysEnum.RWIN }
        };

        public static ModifierKeysEnum GetModifierKeysEnum(int key)
        {
            if (modifierKeysMap.ContainsKey(key))
            {
                return modifierKeysMap[key];
            }
            else
            {
                return ModifierKeysEnum.NONE;
            }
        }
    }
}
