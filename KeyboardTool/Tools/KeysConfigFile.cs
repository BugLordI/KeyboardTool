/*
 * Copyright (c) 2024 BugZhang(BugLordl). All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 * Version: v1.0.0
 * Author:  BugZhang(BugLordl)
 * Url:     https://github.com/BugLordI/KeyboardTool
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KeyboardTool.Tools
{
    internal class KeysConfigFile
    {
        private const String SAVE_FILE_NAME = "Keys.save";

        public static void SaveKey(byte[] keyCode, byte[] modifierKeyCode, byte[] keysAction)
        {
            using (FileStream fs = new FileStream(SAVE_FILE_NAME, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    Span<byte> span = new byte[keyCode.Length + modifierKeyCode.Length + keysAction.Length];
                    keyCode.CopyTo(span);
                    modifierKeyCode.CopyTo(span.Slice(keyCode.Length));
                    keysAction.CopyTo(span.Slice(keyCode.Length + keysAction.Length));
                    bw.Write(span);
                }
            }
        }

        public static (int, int, int) LoadKey()
        {
            using (FileStream fs = new FileStream(SAVE_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int length = (int)fs.Length;
                    byte[] config = br.ReadBytes(length);
                    Span<byte> span = config.AsSpan();
                    byte[] keyCodeArr = span.Slice(0, 4).ToArray();
                    byte[] modifierKeyCodeArr = span.Slice(4, 4).ToArray();
                    byte[] keysActionArr = span.Slice(8, 4).ToArray();
                    return (BitConverter.ToInt32(keyCodeArr), BitConverter.ToInt32(modifierKeyCodeArr), BitConverter.ToInt32(keysActionArr));
                }
            }
        }
    }
}
