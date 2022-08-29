using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgCmp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void RTB1_VScroll(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(RTB1.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            int wParam = (int)ScrollBarCommands.SB_THUMBPOSITION | (int)nPos;
            SendMessage(RTB2.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0)); //Error occurs here.
        }

        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }

        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RTB1.Text = Value.rtbstr1;
            RTB2.Text = Value.rtbstr2;
            diffs = DIFF.diff_main(RTB1.Text, RTB2.Text);
            DIFF.diff_cleanupSemanticLossless(diffs);      // <--- see note !

            chunklist1 = collectChunks(RTB1);
            chunklist2 = collectChunks(RTB2);

            paintChunks(RTB1, chunklist1);
            paintChunks(RTB2, chunklist2);

            RTB1.SelectionLength = 0;
            RTB2.SelectionLength = 0;
        }

        // this is the diff object;
        diff_match_patch DIFF = new diff_match_patch();

        // these are the diffs
        List<Diff> diffs;

        // chunks for formatting the two RTBs:
        List<Chunk> chunklist1;
        List<Chunk> chunklist2;

        // two color lists:
        Color[] colors1 = new Color[3] { Color.LightGreen, Color.LightSalmon, Color.White };
        Color[] colors2 = new Color[3] { Color.LightSalmon, Color.LightGreen, Color.White };


        public struct Chunk
        {
            public int startpos;
            public int length;
            public Color BackColor;
        }

        List<Chunk> collectChunks(RichTextBox RTB)
        {
            RTB.Text = "";
            List<Chunk> chunkList = new List<Chunk>();
            foreach (Diff d in diffs)
            {
                if (RTB == RTB2 && d.operation == Operation.DELETE) continue;  // **
                if (RTB == RTB1 && d.operation == Operation.INSERT) continue;  // **

                Chunk ch = new Chunk();
                int length = RTB.TextLength;
                RTB.AppendText(d.text);
                ch.startpos = length;
                ch.length = d.text.Length;
                ch.BackColor = RTB == RTB1 ? colors1[(int)d.operation]
                                           : colors2[(int)d.operation];
                chunkList.Add(ch);
            }
            return chunkList;

        }

        void paintChunks(RichTextBox RTB, List<Chunk> theChunks)
        {
            foreach (Chunk ch in theChunks)
            {
                RTB.Select(ch.startpos, ch.length);
                RTB.SelectionBackColor = ch.BackColor;
            }

        }
    }
}
