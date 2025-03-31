using KeyboardTool.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using KeyboardTool.Tools;

namespace KeyboardToolTest
{
    public class TestEnumTool
    {
        public static void testEnumTool()
        {
            IntPtr intPtr = new IntPtr(0x106);
            KeysActionEnum actionEnum = ((int)intPtr).ParseToEnum<KeysActionEnum>();
            Console.WriteLine("TestEnumTool");
        }
    }
}
