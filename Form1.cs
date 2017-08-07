using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualBasic.Devices;

namespace MajnLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebClient client = new WebClient();
        string domovskyAdresar = Path.GetFullPath(".minecraft");
        string spusteni = "";
        Guid identifikator = Guid.NewGuid();
        byte archos = 32;
        string jmenoHrace;
        string aToken;
        string prihlaseno;

        string prihlaseni1;
        string prihlaseni2;
        string prihlaseni3;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.Is64BitOperatingSystem) archos = 64;
            nacteniVerzi();
            // Nacteni nastaveni
            if (File.Exists(domovskyAdresar + "/MajnLauncher.txt"))
            {
                FileStream fs = new FileStream(domovskyAdresar + "/MajnLauncher.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                login.Text = sr.ReadLine();
                if (sr.ReadLine() == "true")
                {
                    origo.Checked = true;
                    origo_CheckedChanged(null, null);
                }
                else
                {
                    origo.Checked = false;
                    origo_CheckedChanged(null, null);
                }
                if (sr.ReadLine() == "true") snapshoty.Checked = true;
                cbVerze.SelectedItem = sr.ReadLine();
                domovskyAdresar = sr.ReadLine();
                sr.Close(); fs.Close();
            }

            // RAM
            ComputerInfo maxRam = new ComputerInfo();
            ulong pamet = ulong.Parse(maxRam.TotalPhysicalMemory.ToString());
            int maxpamet = Convert.ToInt16(pamet / (1024 * 1024));
            ram.Maximum = maxpamet;

            if (origo.Checked)
            {
                label1.Text = "Email:";
                heslo.Enabled = true;
            }
            else
            {
                label1.Text = "Login:";
                heslo.Enabled = false;
            }
        }
        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            prihlaseni1 = login.Text;
            prihlaseni2 = heslo.Text;
            prihlaseni3 = cbVerze.Text;
            backgroundWorker1.RunWorkerAsync(Convert.ToInt16(ram.Value));            
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                short pocitadlo = 0;
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    // Prihlaseni - nedokonceno, ale kod je plne funkcni                    
                    if (origo.Checked)
                    {
                        WebRequest paket = WebRequest.Create("https://authserver.mojang.com/authenticate");
                        paket.ContentType = "application/json";
                        paket.Method = "POST";
                        byte[] rozlozeno = Encoding.ASCII.GetBytes("{\"agent\":{\"name\":\"Minecraft\",\"version\": 1},\"username\":\"" + prihlaseni1 + "\",\"password\":\"" + prihlaseni2 + "\", \"clientToken\": \"" + identifikator.ToString() + "\"}");
                        try
                        {
                            Stream pozadavek = paket.GetRequestStream();
                            pozadavek.Write(rozlozeno, 0, rozlozeno.Length);
                            pozadavek.Close();
                            WebResponse odezva = paket.GetResponse();
                            prihlaseno = new StreamReader(odezva.GetResponseStream()).ReadToEnd();
                            Odezva udaje = JsonConvert.DeserializeObject<Odezva>(prihlaseno);
                            jmenoHrace = udaje.selectedProfile.name;
                            aToken = udaje.accessToken;
                        }
                        catch
                        { MessageBox.Show("Nemůžu se připojit, tvuj login nebo heslo jsou chybné!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    // Stazeni verze
                    Directory.CreateDirectory(domovskyAdresar + "/versions/" + prihlaseni3);
                    string cesta = domovskyAdresar + "/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".jar";
                    if (!File.Exists(cesta))
                    {
                        client.DownloadFile("https://s3.amazonaws.com/Minecraft.Download/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".jar", cesta);
                        client.DownloadFile("https://s3.amazonaws.com/Minecraft.Download/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".json", domovskyAdresar + "/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".json");
                        pocitadlo += 10;
                        backgroundWorker1.ReportProgress(pocitadlo);
                    }

                    // Stazeni libraries
                    Directory.CreateDirectory(domovskyAdresar + "/libraries");
                    Libraries knihovny = JsonConvert.DeserializeObject<Libraries>(File.ReadAllText(domovskyAdresar + "/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".json"));
                    bool povoleno = true;
                    spusteni = "-Xms512M -Xmx" + e.Argument.ToString() + "M -Djava.library.path=" + domovskyAdresar + "/libraries/natives -cp ";
                    foreach (Library polozka in knihovny.libraries)
                    {
                        bool zahrnuto = true;
                        if (polozka.rules != null)
                        {
                            foreach (Rule pravidlo in polozka.rules)
                            {
                                if (pravidlo.os != null)
                                {
                                    if (pravidlo.os.name != "windows")
                                    {
                                        zahrnuto = false;
                                    }
                                }

                                if (povoleno)
                                {
                                    try
                                    {
                                        if (pravidlo.action == "disallow")
                                        {
                                            povoleno = false;
                                            zahrnuto = true;
                                        }
                                    }
                                    catch { }
                                }
                                else
                                {
                                    povoleno = true;
                                }
                            }
                        }

                        if (zahrnuto)
                        {
                            if (polozka.natives == null)
                            {
                                string[] nazev = polozka.name.Split(':');
                                nazev[0] = nazev[0].Replace('.', '/');
                                cesta = "/libraries/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + ".jar";
                                spusteni += domovskyAdresar + cesta + ';';
                                if (!File.Exists(cesta))
                                {
                                    Directory.CreateDirectory(domovskyAdresar + "/libraries/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2]);
                                    client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + ".jar", domovskyAdresar + cesta);
                                    client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + ".jar.sha1", domovskyAdresar + cesta + ".sha1");
                                }
                            }
                            else
                            {
                                string[] nazev = polozka.name.Split(':');
                                nazev[0] = nazev[0].Replace('.', '/');
                                if (polozka.natives.windows.Contains("${arch}"))
                                {
                                    cesta = "/libraries/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows-" + archos.ToString() + ".jar";
                                    spusteni += domovskyAdresar + cesta + ';';
                                    if (!File.Exists(cesta))
                                    {
                                        Directory.CreateDirectory(domovskyAdresar + "/libraries/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2]);
                                        client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows-" + archos.ToString() + ".jar", domovskyAdresar + cesta);
                                        client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows-" + archos.ToString() + ".jar.sha1", domovskyAdresar + cesta + ".sha1");
                                    }
                                }
                                else
                                {
                                    cesta = "/libraries" + "/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows.jar";
                                    spusteni += domovskyAdresar + cesta + ';';
                                    if (!File.Exists(cesta))
                                    {
                                        Directory.CreateDirectory(domovskyAdresar + "/libraries/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2]);
                                        client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows.jar", domovskyAdresar + cesta);
                                        client.DownloadFile("https://libraries.minecraft.net/" + nazev[0] + "/" + nazev[1] + "/" + nazev[2] + "/" + nazev[1] + "-" + nazev[2] + "-natives-windows.jar.sha1", domovskyAdresar + cesta + ".sha1");
                                    }
                                }
                            }
                            if (polozka.name.Contains("org.lwjgl.lwjgl:lwjgl-platform") || polozka.name.Contains("net.java.jinput:jinput-platform"))
                            {
                                Directory.CreateDirectory(domovskyAdresar + "/libraries/natives");
                                FastZip komprimace = new FastZip();
                                komprimace.ExtractZip(domovskyAdresar + cesta, domovskyAdresar + "/libraries/natives", "");
                                if (Directory.Exists(domovskyAdresar + "/libraries/natives/META-INF"))
                                {
                                    Directory.Delete(domovskyAdresar + "/libraries/natives/META-INF", true);
                                }
                            }
                            pocitadlo += 5;
                            backgroundWorker1.ReportProgress(pocitadlo);
                        }
                    }

                    // Stazeni assets
                    Directory.CreateDirectory(domovskyAdresar + "/assets/indexes");
                    client.DownloadFile("https://s3.amazonaws.com/Minecraft.Download/indexes/" + knihovny.assets + ".json", domovskyAdresar + "/assets/indexes/" + knihovny.assets + ".json");
                    Directory.CreateDirectory(domovskyAdresar + "/assets/objects");
                    string komponenty = File.ReadAllText(domovskyAdresar + "/assets/indexes/" + knihovny.assets + ".json");
                    Match hash = Regex.Match(komponenty, "\"hash\": \".{40}");
                    string komponenta;
                    while (hash.Success)
                    {
                        komponenta = hash.Value;
                        komponenta = komponenta.Remove(0, 9);
                        Directory.CreateDirectory(domovskyAdresar + "/assets/objects/" + komponenta[0] + komponenta[1]);
                        cesta = domovskyAdresar + "/assets/objects/" + komponenta[0] + komponenta[1] + "/" + komponenta;
                        if (!File.Exists(cesta))
                        {
                            client.DownloadFile("http://resources.download.minecraft.net/" + komponenta[0] + komponenta[1] + "/" + komponenta, cesta);
                            pocitadlo += 1;
                            backgroundWorker1.ReportProgress(pocitadlo);
                        }
                        hash = hash.NextMatch();
                    }
                    backgroundWorker1.ReportProgress(pocitadlo);

                    if (Java() == "")
                    {
                        DialogResult dr = MessageBox.Show("Pro spuštěni hry Minecraft potřebujete platformu Java. Přejete si přejít na stránku ke stažení Javy?", "Java nebyla nalezena!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start("https://www.facebook.com/groups/minecrafthelpczsk/");
                        }
                    }
                    else
                    {
                        // Spusteni
                        spusteni += domovskyAdresar + "/versions/" + prihlaseni3 + "/" + prihlaseni3 + ".jar " + knihovny.mainClass + " " + knihovny.minecraftArguments;
                        // Cesta nesmi obsahovat pomlcky (-)
                        spusteni = spusteni.Replace("${auth_player_name}", "\"" + jmenoHrace + "\"");
                        spusteni = spusteni.Replace("${version_name}", "\"" + prihlaseni3 + "\"");
                        spusteni = spusteni.Replace("${game_directory}", "\"" + domovskyAdresar + "/\"");
                        spusteni = spusteni.Replace("${assets_root}", "\"" + domovskyAdresar + "/assets\"");
                        spusteni = spusteni.Replace("${assets_index_name}", knihovny.assets);
                        spusteni = spusteni.Replace("${auth_uuid}", "\"" + identifikator.ToString() + "\"");
                        spusteni = spusteni.Replace("${auth_access_token}", "\"" + aToken + "\"");
                        spusteni = spusteni.Replace("${user_properties}", "{}");
                        spusteni = spusteni.Replace("${user_type}", "\"mojang\"");
                        spusteni = spusteni.Replace("${version_type}", "\"MajnLauncher\"");
                        Process minecraft = new Process();
                        ProcessStartInfo mcstart = new ProcessStartInfo(Java() + "/bin/javaw.exe", spusteni);
                        // Pro spusteni s konzoli - ProcessStartInfo(Java() + "/bin/java.exe", spusteni)
                        mcstart.WorkingDirectory = domovskyAdresar + "/";
                        minecraft.StartInfo = mcstart;
                        minecraft.Start();

                        // Ulozeni nastaveni
                        FileStream fs = new FileStream(domovskyAdresar + "/MajnLauncher.txt", FileMode.OpenOrCreate);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(prihlaseni1);
                        if (origo.Checked) sw.WriteLine("true");
                        if (!origo.Checked) sw.WriteLine("false");
                        if (snapshoty.Checked) sw.WriteLine("true");
                        if (!snapshoty.Checked) sw.WriteLine("false");
                        sw.WriteLine(prihlaseni3);
                        sw.WriteLine(domovskyAdresar);
                        sw.Close(); fs.Close();
                    }
                }
                else
                { MessageBox.Show("Zkontrolujte připojení k internetu.", "Server je nedostupný!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception chyba)
            {
                MessageBox.Show(chyba.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
        // Autovyhledani Javy
        private string Java()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            start.Enabled = true;
            progressBar1.Value = 0;
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 1000)
            {
                progressBar1.Value = e.ProgressPercentage;
            }
        }
        private void snapshoty_CheckedChanged(object sender, EventArgs e)
        {
            nacteniVerzi();
        }
        private void odinstalace_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(domovskyAdresar))
            {
                DialogResult dr = MessageBox.Show("Určitě chcete vymazat složku s Minecraftem? Přijdete tím o všechny uložené hry, servery a snímky obrazovek.", "Jste si jistý?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Directory.Delete(domovskyAdresar, true);
                }
            }
            else
            {
                MessageBox.Show("Minecraft ještě nebyl nainstalován.", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void domov_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.ShowDialog();
            domovskyAdresar = folderBrowserDialog1.SelectedPath;
        }
        private void login_TextChanged(object sender, EventArgs e)
        {
            if (login.Text.Contains(" "))
            {
                MessageBox.Show("Login nemůže obsahovat mezery.", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                login.Text = login.Text.Remove(login.Text.Length - 1);
            }
        }
        void nacteniVerzi()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                cbVerze.Items.Clear();
                Directory.CreateDirectory(domovskyAdresar);
                client.DownloadFile("https://launchermeta.mojang.com/mc/game/version_manifest.json", domovskyAdresar + "/versions.json");
                RootObject vsechnyVerze = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(domovskyAdresar + "/versions.json"));
                foreach (Version verze in vsechnyVerze.versions)
                {
                    if (snapshoty.Checked)
                    {
                        if (verze.type == "snapshot")
                        {
                            cbVerze.Items.Add(verze.id);
                        }
                        cbVerze.SelectedItem = vsechnyVerze.latest.snapshot;
                    }
                    else
                    {
                        if (verze.type == "release")
                        {
                            cbVerze.Items.Add(verze.id);
                        }
                        cbVerze.SelectedItem = vsechnyVerze.latest.release;
                    }
                }
                
            }
            else
            { MessageBox.Show("Nemůžu načíst seznam verzí, zkontrolujte připojení k internetu a restartujte launcher.", "Server je nedostupný!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void origo_CheckedChanged(object sender, EventArgs e)
        {
            if (origo.Checked)
            {
                label1.Text = "Email:";
                heslo.Enabled = true;
            }
            else
            {
                label1.Text = "Login:";
                heslo.Enabled = false;
            }
        }
        // Version.json class
        public class Os
        {
            public string name { get; set; }
        }

        public class Rule
        {
            public string action { get; set; }
            public Os os { get; set; }
        }
        public class Artifact
        {
            public string url { get; set; }
        }
        public class Downloads
        {
            public Artifact artifact { get; set; }
        }
        public class Natives
        {
            public string windows { get; set; }
            public string linux { get; set; }
            public string osx { get; set; }
        }
        public class Library
        {
            public string name { get; set; }
            public Downloads downloads { get; set; }
            public List<Rule> rules { get; set; }
            public Natives natives { get; set; }
        }

        public class Libraries
        {
            public string mainClass { get; set; }
            public string minecraftArguments { get; set; }
            public string assets { get; set; }
            public List<Library> libraries { get; set; }
        }
        // VersionManifest class
        public class Latest
        {
            public string snapshot { get; set; }
            public string release { get; set; }
        }

        public class Version
        {
            public string id { get; set; }
            public string type { get; set; }
            public string time { get; set; }
            public string releaseTime { get; set; }
            public string url { get; set; }
        }

        public class RootObject
        {
            public Latest latest { get; set; }
            public List<Version> versions { get; set; }
        }
        // Odezva class
        public class Odezva
        {
            public string accessToken { get; set; }
            public string clientToken { get; set; }
            public selectedProfile selectedProfile { get; set; }
        }
        public class selectedProfile
        {
            public string name { get; set; }
        }
    }
}
