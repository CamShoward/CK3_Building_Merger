using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Newtonsoft; // Library for handling JSON files.
using System.Diagnostics; 
using System.Text.RegularExpressions; // Library for using regular expressions.
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CK3_Building_Merger
{
    public partial class CK3_Building_Merger : Form
    {
        private HashSet<string> castleHoldings = new HashSet<string>();
        private HashSet<string> tribalHoldings = new HashSet<string>();
        private HashSet<string> cityHoldings = new HashSet<string>();
        private HashSet<string> churchHoldings = new HashSet<string>();

        public CK3_Building_Merger()
        {
            InitializeComponent();
        }

        public static void Protoo(string fp, HashSet<string> castleHoldings, HashSet<string> tribalHoldings, HashSet<string> cityHoldings, HashSet<string> churchHoldings)
        {
            
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
            createDescripFile(modPath, modListData.Name);
            createModFile(modPath, "buildingsList");
        }


        private void createModFile(string modPath, string modName)
        {

            // Add the additional line with the path
            string modFolder = textBox3.Text;
            //Directory.CreateDirectory(modFolder);

            string modFilePath = Path.Combine(modFolder, modName + ".mod");

            using (StreamWriter sw = new StreamWriter(modFilePath, true)) // 'true' to append to the file
            {   
                string buildingsFolderPath = modPath;
                sw.WriteLine("version = \"1\"");
                sw.WriteLine("tags ={");
                sw.WriteLine("\t Utilities");
                sw.WriteLine("}");
                sw.WriteLine($"name= \"{modName}\"");
                sw.WriteLine("supported_version= \"*\"");
                sw.WriteLine($"path=\"mod\\buildingsList\"");
            }
        }

        private void createDescripFile(string modPath, string modName)
        {
            string outputFolder = Path.Combine(modPath, "buildingsList");
            Directory.CreateDirectory(outputFolder);

            string outputPath = Path.Combine(outputFolder,"descriptor.mod");

            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine("version = \"1\"");
                sw.WriteLine("tags ={");
                sw.WriteLine("\t Utilities");
                sw.WriteLine("}");
                sw.WriteLine($"name= \"{modName}\"");
                sw.WriteLine("supported_version= \"*\"");
            }
        }

        private void OutputHoldingsToFile(string modListName, string modPath, HashSet<string> castleHoldings, HashSet<string> tribalHoldings, HashSet<string> cityHoldings, HashSet<string> churchHoldings)
        {
            string outputFolder = Path.Combine(modPath,"buildingsList\\common\\holdings");
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.Combine(outputFolder, "~holdings.txt");

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                if (castleHoldings.Count > 0)
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
                }

                if (tribalHoldings.Count > 0)
                {
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
                }

                if (cityHoldings.Count > 0)
                {
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
                }

                if (churchHoldings.Count > 0)
                {
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
        filePath = fp;
    }

    public List<mod> makeList()
    {
        try
        {
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();

            // Log the JSON content
            Debug.WriteLine("JSON Content: " + json);

            var playset = Newtonsoft.Json.JsonConvert.DeserializeObject<playset>(json);

            if (playset != null)
            {
                Name = playset.name;
                return playset.mods;
            }
            else
            {
                throw new Exception("Could not deserialize JSON file.");
            }
        }
        catch (Newtonsoft.Json.JsonReaderException jrex)
        {
            Debug.WriteLine("JSON Reader Exception: " + jrex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("General Exception: " + ex.Message);
            throw;
        }
    }
}

