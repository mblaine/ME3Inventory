using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ME3Inventory
{
    public partial class Form1 : Form
    {
        private const String URL = @"http://social.bioware.com/n7hq/home/inventory/?name={0}&platform={1}";
        private readonly String ARCHIVE = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ME3Inventory");

        private List<Item> items;
        private Settings settings = new Settings();
        private Find findForm = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            Directory.CreateDirectory(ARCHIVE);

            if (!String.IsNullOrWhiteSpace(settings.LastPlatform))
            {
                switch (settings.LastPlatform)
                {
                    case "pc":
                        cmbPlatform.SelectedIndex = 0;
                        txtName.Text = settings.PCUser;
                        break;
                    case "xbox":
                        cmbPlatform.SelectedIndex = 1;
                        txtName.Text = settings.XboxUser;
                        break;
                    case "ps3":
                        cmbPlatform.SelectedIndex = 2;
                        txtName.Text = settings.PS3User;
                        break;
                    case "wiiu":
                        cmbPlatform.SelectedIndex = 3;
                        txtName.Text = settings.WiiUUser;
                        break;
                }
                cmbPlatform_SelectionChangeCommitted(this, null);
            }
            else
                cmbPlatform.SelectedIndex = 0;

            LoadList();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            GetData();
            if(lstArchive.Items.Count > 0)
                lstArchive.SelectedIndex = 0;
            btnGo.Enabled = true;
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            Process.Start(ARCHIVE);
        }

        private void lstArchive_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseAndDisplay();
        }

        private void radShow_CheckedChanged(object sender, EventArgs e)
        {
            ParseAndDisplay();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            switch (cmbPlatform.SelectedIndex)
            {
                case 0:
                    settings.PCUser = txtName.Text;
                    break;
                case 1:
                    settings.XboxUser = txtName.Text;
                    break;
                case 2:
                    settings.PS3User = txtName.Text;
                    break;
                case 3:
                    settings.WiiUUser = txtName.Text;
                    break;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                btnGo.Enabled = false;
                lstArchive.Items.Clear();
            }
            else
            {
                btnGo.Enabled = true;
                LoadList();
            }
        }

        private void cmbPlatform_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (cmbPlatform.SelectedIndex)
            {
                case 0:
                    settings.LastPlatform = "pc";
                    if (!String.IsNullOrWhiteSpace(settings.PCUser))
                        txtName.Text = settings.PCUser;
                    else
                        settings.PCUser = txtName.Text;
                    break;
                case 1:
                    settings.LastPlatform = "xbox";
                    if (!String.IsNullOrWhiteSpace(settings.XboxUser))
                        txtName.Text = settings.XboxUser;
                    else
                        settings.XboxUser = txtName.Text;
                    break;
                case 2:
                    settings.LastPlatform = "ps3";
                    if (!String.IsNullOrWhiteSpace(settings.PS3User))
                        txtName.Text = settings.PS3User;
                    else
                        settings.PS3User = txtName.Text;
                    break;
                case 3:
                    settings.LastPlatform = "wiiu";
                    if (!String.IsNullOrWhiteSpace(settings.WiiUUser))
                        txtName.Text = settings.WiiUUser;
                    else
                        settings.WiiUUser = txtName.Text;
                    break;
            }
            LoadList();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.Write();
        }

        private void txtOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
            {
                txtOutput.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                if (Find.LastSearch == null || Find.LastSearch.Length == 0)
                    OpenFindForm();
                else
                    Find.FindNext(Find.LastSearch, true, this, this);
                return true;
            }
            else if (keyData == (Keys.F3 | Keys.Shift))
            {
                if (Find.LastSearch == null || Find.LastSearch.Length == 0)
                    OpenFindForm();
                else
                    Find.FindNext(Find.LastSearch, false, this, this);
                return true;
            }
            else if (keyData == (Keys.F | Keys.Control))
            {
                OpenFindForm();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadList()
        {
            lstArchive.Items.Clear();

            if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace((String)cmbPlatform.SelectedItem))
            {
                btnGo.Enabled = false;
                return;
            }
            else
                btnGo.Enabled = true;
            
            foreach (String file in Directory.GetFiles(ARCHIVE, "*.xml").Where(s => s.ToLower().Contains(String.Format("{0}-{1}", txtName.Text.ToLower(), ((String)cmbPlatform.SelectedItem).ToLower()))).OrderByDescending(s => s))
            {
                lstArchive.Items.Add(new ArchiveFile(file));
            }
        }

        private void GetData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(URL, txtName.Text, cmbPlatform.SelectedItem.ToString().ToLower()));
            request.Method = "GET";
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = "ME3Inventory/0.1pre (http://github.com/mblaine/ME3Inventory)";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("N7HQ_agegate", "1970-1-1", "/", "social.bioware.com"));
            request.CookieContainer.Add(new Cookie("se_language_autodetected", "1", "/", "social.bioware.com"));
            request.CookieContainer.Add(new Cookie("se_language_anonymous", "1", "/", "social.bioware.com"));
            request.Proxy = null;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader responseStream = new StreamReader(response.GetResponseStream());
            String html = responseStream.ReadToEnd();
            response.Close();
            responseStream.Close();

            try
            {
                XDocument doc = XDocument.Parse(Regex.Match(html, "(<div id=\"inventory\">[\\s\\S]*?)<div class=\"clear\"></div>").Groups[1].Value);
                Save(doc);
            }
            catch (XmlException)
            {
                MessageBox.Show("Trouble retrieving data, double check user name and platform.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private List<Item> ParseData(ArchiveFile file)
        {
            List<Item> i = new List<Item>();
            XDocument doc = XDocument.Parse(File.ReadAllText(file.FilePath));
            foreach (XElement category in doc.XPathSelectElements("/div[@id='inventory']/div[@id='inventory_content']/div"))
            {
                String typeId = Regex.Replace(category.Attribute("id").Value, "_|content|inventory", "");
                ItemType type = (ItemType)Enum.Parse(typeof(ItemType), typeId == "gear" ? typeId : typeId.Substring(0, typeId.Length - 1), true);

                foreach (XElement node in category.XPathSelectElements("./div"))
                {
                    Item item;
                    if (type == ItemType.Consumable)
                        item = new Consumable(node, type);
                    else
                        item = new Durable(node, type);
                    i.Add(item);
                }
            }
            return i;
        }

        private void Save(XDocument doc)
        {
            using (StringWriter sw = new StringWriter())
            {
                doc.Save(sw);
                String output = sw.ToString();

                String latest = Directory.GetFiles(ARCHIVE, "*.xml").Where(s => s.ToLower().Contains(String.Format("{0}-{1}", txtName.Text.ToLower(), ((String)cmbPlatform.SelectedItem).ToLower()))).OrderByDescending(s => s).FirstOrDefault();
                
                if (String.IsNullOrEmpty(latest) || output != File.ReadAllText(latest))
                {
                    String path = Path.Combine(ARCHIVE, String.Format("{0}-{1}-{2:yyyy-MM-dd-HH-mm-ss}.xml", txtName.Text, cmbPlatform.SelectedItem.ToString().ToLower(), DateTime.Now));
                    File.WriteAllText(path, output);
                    lstArchive.Items.Insert(0, new ArchiveFile(path));
                }
            }
        }

        private void DisplayAll()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Item i in items)
            {
                sb.AppendLine(i.ToString());
            }
            txtOutput.Text = sb.ToString();
        }

        private void DisplayDifferences(List<Item> previous)
        {
            Dictionary<String, Item> prev = previous.ToDictionary(i => i.Name, i => i);

            ItemType lastType = items[0].Type;
            StringBuilder sb = new StringBuilder();
            foreach (Item item in items)
            {
                if (!item.Equals(prev[item.Name]))
                {
                    if (lastType != item.Type)
                        sb.AppendLine();
                    lastType = item.Type;
                    sb.AppendLine(item.Difference(prev[item.Name]));
                }

            }
            sb.AppendLine();
            txtOutput.Text = sb.ToString();
        }

        private void DisplayTotals()
        {
            var typeAndRarity = from item in items
                                where item is Durable
                                group item by new { item.Type, item.Rarity } into grouped
                                orderby grouped.Key.Type, grouped.Key.Rarity
                                select new
                                {
                                    Type = grouped.Key.Type,
                                    Rarity = grouped.Key.Rarity,
                                    Current = grouped.Sum(i => ((Durable)i).Level),
                                    Max = grouped.Sum(i => ((Durable)i).MaxLevel)
                                };

            var byType = from entry in typeAndRarity
                         group entry by entry.Type into grouped
                         orderby grouped.Key
                         select new
                         {
                             Type = grouped.Key,
                             Current = grouped.Sum(i => i.Current),
                             Max = grouped.Sum(i => i.Max)
                         };

            var byRarity = from entry in typeAndRarity
                           group entry by entry.Rarity into grouped
                           orderby grouped.Key
                           select new
                           {
                               Rarity = grouped.Key,
                               Current = grouped.Sum(i => i.Current),
                               Max = grouped.Sum(i => i.Max)
                           };

            StringBuilder sb = new StringBuilder();

            ItemType last = typeAndRarity.First().Type;
            foreach (var entry in typeAndRarity)
            {
                if (last != entry.Type)
                    sb.AppendLine();
                last = entry.Type;
                sb.AppendFormat("{0} - {1}: {2:#.00}% ({3}/{4}){5}", entry.Type, entry.Rarity, ((decimal)entry.Current) / ((decimal)entry.Max) * 100m, entry.Current, entry.Max, Environment.NewLine);
            }

            sb.AppendLine();

            foreach(var entry in byType)
                sb.AppendFormat("{0}: {1:#.00}% ({2}/{3}){4}", entry.Type, ((decimal)entry.Current) / ((decimal)entry.Max) * 100m, entry.Current, entry.Max, Environment.NewLine);

            sb.AppendLine();

            foreach (var entry in byRarity)
                sb.AppendFormat("{0}: {1:#.00}% ({2}/{3}){4}", entry.Rarity, ((decimal)entry.Current) / ((decimal)entry.Max) * 100m, entry.Current, entry.Max, Environment.NewLine);

            sb.AppendLine();

            int totalCurrent = byType.Sum(i => i.Current);
            int totalMax = byType.Sum(i => i.Max);
            decimal total = ((decimal)totalCurrent) / ((decimal)totalMax) * 100m;

            sb.AppendFormat("Total progress: {0:#.00}% ({1}/{2})", total, totalCurrent, totalMax);

            txtOutput.Text = sb.ToString();
        }

        private void ParseAndDisplay()
        {
            if (lstArchive.SelectedIndex < 0 || lstArchive.SelectedItem == null)
                return; ;
            
            items = ParseData((ArchiveFile)lstArchive.SelectedItem);
            if (radShowChanges.Checked && lstArchive.SelectedIndex < lstArchive.Items.Count - 1)
                DisplayDifferences(ParseData((ArchiveFile)lstArchive.Items[lstArchive.SelectedIndex + 1]));
            else if (radShowTotals.Checked)
                DisplayTotals();
            else
                DisplayAll();
        }

        internal TextBox TextBox
        {
            get
            {
                return this.txtOutput;
            }
        }

        private void OpenFindForm()
        {
            if (findForm == null || findForm.IsDisposed)
                findForm = new Find();
            if (findForm.Visible)
                findForm.Focus();
            else
                findForm.Show(this);
        }
    }
}
