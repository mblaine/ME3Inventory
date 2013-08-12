using System;
using System.Windows.Forms;

namespace ME3Inventory
{
    public partial class Find : Form
    {
        internal static String LastSearch = "";
        internal static bool LastSearchForward = true;

        public Find()
        {
            InitializeComponent();
        }

        private void Find_Load(object sender, EventArgs e)
        {
            txtFind.Text = LastSearch;
            radDown.Checked = LastSearchForward;
            radUp.Checked = !LastSearchForward;
        }

        private void Find_FormClosing(object sender, FormClosingEventArgs e)
        {
            LastSearch = txtFind.Text;
            LastSearchForward = radDown.Checked;
        }

        private void Find_Activated(object sender, EventArgs e)
        {
            txtFind.Focus();
            txtFind.SelectAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnFindNext.Focus();
                btnFindNext_Click(this, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal static void FindNext(String search, bool forward, Form1 target, Form msgBoxParent)
        {
            if (Search(search, target.TextBox, forward))
                target.TextBox.ScrollToCaret();
            else
                MessageBox.Show(msgBoxParent, String.Format("Unable to find \"{0}\".", search), "Find", MessageBoxButtons.OK);
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            LastSearch = txtFind.Text;
            LastSearchForward = radDown.Checked;
            FindNext(LastSearch, LastSearchForward, (Form1)this.Owner, this);

            this.Focus();
            txtFind.Focus();
            txtFind.SelectAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static bool Search(String search, TextBox target, bool forward)
        {
            if (forward)
            {
                int start = target.SelectionStart + target.SelectionLength;
                int i = target.Text.IndexOf(search, start, StringComparison.InvariantCultureIgnoreCase);
                if (i >= 0)
                {
                    target.Select(i, search.Length);
                    return true;
                }
                else
                {
                    i = target.Text.IndexOf(search, 0, StringComparison.InvariantCultureIgnoreCase);
                    if (i >= 0)
                    {
                        target.Select(i, search.Length);
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
            {
                int start = target.SelectionStart;
                int i = target.Text.LastIndexOf(search, start, start + 1, StringComparison.InvariantCultureIgnoreCase);
                if (i >= 0)
                {
                    target.Select(i, search.Length);
                    return true;
                }
                else
                {
                    i = target.Text.LastIndexOf(search, target.Text.Length - 1, target.Text.Length - start, StringComparison.InvariantCultureIgnoreCase);
                    if (i >= 0)
                    {
                        target.Select(i, search.Length);
                        return true;
                    }
                    else
                        return false;
                }
            }

        }
    }
}