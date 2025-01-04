using KeyboardTool;
using KeyboardTool.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardToolTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String hookId = KeyboardFactory.RegisterKey(KeysEnum.A, (e, a) =>
            {
                KeysEvent keysEvent = e as KeysEvent;
                String str = $"{keysEvent.KeysAction}：{keysEvent.Key}";
                if (keysEvent.ModifierKey != ModifierKeysEnum.NONE)
                {
                    str = $"{keysEvent.KeysAction}：{keysEvent.ModifierKey} + {keysEvent.Key}";
                }
                richTextBox1.AppendText(str + "\n");
            });
            richTextBox1.AppendText($"注册按键：{KeysEnum.A}\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String hookId = KeyboardFactory.RegisterKey(KeysEnum.R, (e, a) =>
            {
                KeysEvent keysEvent = e as KeysEvent;
                String str = $"{keysEvent.KeysAction}：{keysEvent.Key}";
                if (keysEvent.ModifierKey != ModifierKeysEnum.NONE)
                {
                    str = $"{keysEvent.KeysAction}：{keysEvent.ModifierKey} + {keysEvent.Key}";
                }
                richTextBox1.AppendText(str + "\n");
            }, modifierKeyCode: KeysEnum.LCONTROL);
            richTextBox1.AppendText($"注册按键：{KeysEnum.LCONTROL}+{KeysEnum.R}\n");
        }
    }
}
