using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Newtonsoft;
using System.Diagnostics; // Library for handling JSON files.
using System.Text.RegularExpressions; // Library for using regular expressions.
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CK3_Building_Merger
{
    public partial class Form1 : Form
    {
        private HashSet<string> castleHoldings = new HashSet<string>();
        private HashSet<string> tribalHoldings = new HashSet<string>();
        private HashSet<string> cityHoldings = new HashSet<string>();
        private HashSet<string> churchHoldings = new HashSet<string>();

        public Form1()
        {
            InitializeComponent();
        }

        public static string Protoo(string fp, HashSet<string> castleHoldings, HashSet<string> tribalHoldings, HashSet<string> cityHoldings, HashSet<string> churchHoldings)
        {
            string pc = " ";
            if (Directory.Exists(fp))
            {
                // Get all files in the directory
                string[] files = Directory.GetFiles(fp);
                foreach (string file in files)
                {
                    string content = File.ReadAllText(file);
                    ExtractBuildings(content, "castle_holding", castleHoldings);
                    ExtractBuildings(content, "tribal_holding", tribalHoldings);
                    ExtractBuildings(content, "city_holding", cityHoldings);
                    ExtractBuildings(content, "church_holding", churchHoldings);
                }
            }
            else
            {
                Debug.WriteLine("Directory does not exist.");
            }
            return pc;
        }

        private static void ExtractBuildings(string content, string holdingType, HashSet<string> holdings)
        {
            string pattern = $@"{holdingType}\s*=\s*\{{.*?buildings\s*=\s*\{{(.*?)\}}.*?\}}";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            Match match = regex.Match(content);

            if (match.Success)
            {
                string buildingsBlock = match.Groups[1].Value;
                string[] lines = buildingsBlock.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    string building = line.Trim().Split('#')[0].Trim();
                    if (!string.IsNullOrEmpty(building))
                    {
                        holdings.Add(building);
                    }
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog
            {
                Title = "Open a JSON playset file.",
                Filter = "JSON Files | *.json",
                InitialDirectory = @"C:\",
                Multiselect = false
            };

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fDialog.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog flDialog = new FolderBrowserDialog
            {
                Description = "Select your Steam Workshop folder.",
                ShowNewFolderButton = false
            };

            if (flDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = flDialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog flDialog = new FolderBrowserDialog
            {
                Description = "Select your CK3 mod folder.",
                ShowNewFolderButton = false
            };

            if (flDialog.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = flDialog.SelectedPath;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            castleHoldings.Clear();
            tribalHoldings.Clear();
            cityHoldings.Clear();
            churchHoldings.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Pull in file paths from textboxes
            string jsonPath = textBox1.Text;
            string workshopPath = textBox2.Text;
            string modPath = textBox3.Text;

            // Check if the JSON file exists
            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("JSON file does not exist. Please check the file path.");
                return;
            }

            // Check if the Steam Workshop folder exists
            if (!Directory.Exists(workshopPath))
            {
                MessageBox.Show("Steam Workshop folder does not exist. Please check the folder path.");
                return;
            }

            // Check if the CK3 mod folder exists
            if (!Directory.Exists(modPath))
            {
                MessageBox.Show("CK3 mod folder does not exist. Please check the folder path.");
                return;
            }

            var modListData = new parseJson(jsonPath);
            var mods = modListData.makeList();

            foreach (var mod in mods)
            {
                var currID = mod.steamId;
                var filepath = Path.Combine(workshopPath, currID, "common", "holdings");

                if (Directory.Exists(filepath))
                {
                    Debug.WriteLine("Buildings found for: " + mod.displayName);
                    Protoo(filepath, castleHoldings, tribalHoldings, cityHoldings, churchHoldings);
                }
                else
                {
                    Debug.WriteLine("No holdings folder/files found for: " + mod.displayName);
                }
            }

            // Output the results to a new file
            OutputHoldingsToFile(modListData.Name, modPath, castleHoldings, tribalHoldings, cityHoldings, churchHoldings);
        }

        private void OutputHoldingsToFile(string modListName, string modPath, HashSet<string> castleHoldings, HashSet<string> tribalHoldings, HashSet<string> cityHoldings, HashSet<string> churchHoldings)
        {
            string outputFolder = Path.Combine(modPath, modListName + "_buildings", "holdings");
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.Combine(outputFolder, "holdings.txt");

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("castle_holding = {");
                writer.WriteLine("\tprimary_building = castle_01");
                writer.WriteLine("\tbuildings = {");
                foreach (string holding in castleHoldings)
                {
                    writer.WriteLine($"\t\t{holding}");
                }
                writer.WriteLine("\t}");
                writer.WriteLine("}");

                writer.WriteLine("\ntribal_holding = {");
                writer.WriteLine("\tprimary_building = tribe_01");
                writer.WriteLine("\tbuildings = {");
                foreach (string holding in tribalHoldings)
                {
                    writer.WriteLine($"\t\t{holding}");
                }
                writer.WriteLine("\t}");
                writer.WriteLine("\tflag = tribal");
                writer.WriteLine("}");

                writer.WriteLine("\ncity_holding = {");
                writer.WriteLine("\tprimary_building = city_01");
                writer.WriteLine("\tbuildings = {");
                foreach (string holding in cityHoldings)
                {
                    writer.WriteLine($"\t\t{holding}");
                }
                writer.WriteLine("\t}");
                writer.WriteLine("\tcan_be_inherited = yes");
                writer.WriteLine("}");

                writer.WriteLine("\nchurch_holding = {");
                writer.WriteLine("\tprimary_building = temple_01");
                writer.WriteLine("\tbuildings = {");
                foreach (string holding in churchHoldings)
                {
                    writer.WriteLine($"\t\t{holding}");
                }
                writer.WriteLine("\t}");
                writer.WriteLine("\tcan_be_inherited = yes");
                writer.WriteLine("}");
            }

            MessageBox.Show($"Holdings data has been written to {outputPath}");
        }
    }
}

public class playset
{
    public string game { get; set; }
    public string name { get; set; }
    // List of the actual mods in the given playset
    public List<mod> mods = new List<mod>();
}

public class mod // Class for the playset JSON file structure.
{
    public string steamId { get; set; } // Mod steam id
    public string displayName { get; set; } // Mod name
    public bool enabled { get; set; } // Enabled or disabled
    public int position { get; set; } // Position in load order
}

public class parseJson
{
    private readonly string filePath;

    public string Name { get; private set; }

    public parseJson(string fp)
    {
        filePath = fp; // Mod list file path. Known issue: system might deny access to certain folders. Potential fix: run as admin.
    }

    public List<mod> makeList()
    {
        using StreamReader reader = new(filePath);
        var json = reader.ReadToEnd();
        var playset = Newtonsoft.Json.JsonConvert.DeserializeObject<playset>(json);

        if (playset != null)
        {
            Name = playset.name;
            return playset.mods;
        }
        else
        {
            throw new Exception("Could not find JSON file.");
        }
    }
}
