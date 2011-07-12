using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        struct MeshSource
        {
            public string Name;
            public string floatCount;
            public string floatArrayContent;
            public string accessorCount;

            public MeshSource(string _Name, string _floatCount, string _floatArrayContent, string _accessorCount) 
           {
                this.Name = _Name;             
                this.floatCount = _floatCount;
                this.floatArrayContent = _floatArrayContent;
                this.accessorCount = _accessorCount;
           }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string outputDAE = tOutputFolder.Text + tMeshName.Text + "_cloud0.dae";
            string outputDAEname = tMeshName.Text + "_cloud0";

            List<string> cloudFiles = new List<string>();
            cloudFiles.Add(outputDAEname);


            if (!convertDAE(tInputDAE.Text, tMeshName.Text + "_cloud0", tCrysisResourceFolder.Text, outputDAE))
                return;
 
            if (!runThroughRC(outputDAE))
                return;

            if (!buildPrefab(tMeshName.Text, cloudFiles, tOutputFolder.Text, tCrysisResourceFolder.Text))
                return;



            
        }



        private bool convertDAE(string inputDAE, string cloudname, string imagelocation, string outputDAE)
        {
            // open input DAE
            string s = System.IO.File.ReadAllText(inputDAE);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(s);
            XmlNode xnRoot = xml.ChildNodes[1];


            // open template
            string template = System.IO.File.ReadAllText(Application.StartupPath + "\\template\\crysis_dae.xml");


            // now fill out data in each mesh source
            Hashtable sources = new Hashtable();
            foreach (XmlNode meshsource in xnRoot["library_geometries"]["geometry"]["mesh"].ChildNodes)
            {
                if (meshsource.Name == "source")
                {
                    sources.Add(meshsource.Attributes["id"].InnerText.ToString(), new MeshSource(meshsource.Attributes["id"].InnerText.ToString(), meshsource["float_array"].Attributes["count"].InnerText.ToString(), meshsource["float_array"].InnerText.ToString(), meshsource["technique_common"]["accessor"].Attributes["count"].InnerText.ToString()));
                }
            }

            // look up TEXCOORD, NORMAL, and VERTEX/POSITION elements properly (not assuming fixed order)
            Hashtable lookup = new Hashtable();


            // is the node called polygons or triangles?
            string polygonName;
            if (xnRoot["library_geometries"]["geometry"]["mesh"].GetElementsByTagName("triangles").Count == 1)
            {
                polygonName = "triangles";
            }
            else if (xnRoot["library_geometries"]["geometry"]["mesh"].GetElementsByTagName("polygons").Count == 1)
            {
                polygonName = "polygons";
            } else {
                MessageBox.Show("No Triangle/Polygon node found!");
                return false;
            }


            foreach (XmlNode source in xnRoot["library_geometries"]["geometry"]["mesh"][polygonName].ChildNodes)
            {
                if (source.Name == "input") 
                    lookup.Add(source.Attributes["semantic"].InnerText.ToString(), source.Attributes["source"].InnerText.ToString().Replace("#", ""));              
            }
            // lookup position source from vertext
            string SourcePosName = xnRoot["library_geometries"]["geometry"]["mesh"]["vertices"].Attributes["id"].InnerText.ToString();

            if (SourcePosName == lookup["VERTEX"].ToString())
            {
                lookup.Add("POSITION", xnRoot["library_geometries"]["geometry"]["mesh"]["vertices"]["input"].Attributes["source"].InnerText.ToString().Replace("#", ""));
            }
            else
            {
                MessageBox.Show("Error: Could not find POSITION mesh in vertices node");
                return false;
            }

            string triangleCount = xnRoot["library_geometries"]["geometry"]["mesh"][polygonName].Attributes["count"].InnerText.ToString();
            // combined all <p> nodes together
            string pContent = "";
            foreach (XmlNode node in xnRoot["library_geometries"]["geometry"]["mesh"][polygonName].GetElementsByTagName("p")) {
                pContent = pContent + node.InnerText + " ";
            }



            // Now populate the template
            template = template.Replace("(created_date)", DateTime.UtcNow.ToString("s") + "Z");
            template = template.Replace("(modified_date)", DateTime.UtcNow.ToString("s") + "Z");
            template = template.Replace("(imagefolder)", imagelocation);
            
            template = template.Replace("(imagename)", (Path.GetFileName(xnRoot["library_images"]["image"].InnerText)));
            template = template.Replace("(cloudname)", cloudname);

            template = template.Replace("(pos_count)", ((MeshSource)sources[lookup["POSITION"].ToString()]).floatCount);
            template = template.Replace("(pos_content)", ConvertWhitespaceToSpacesRegex(((MeshSource)sources[lookup["POSITION"].ToString()]).floatArrayContent));
            template = template.Replace("(pos_assessor_count)",  ((MeshSource)sources[lookup["POSITION"].ToString()]).accessorCount);
            template = template.Replace("(normal_count)", ((MeshSource)sources[lookup["NORMAL"].ToString()]).floatCount);
            template = template.Replace("(normal_content)", ConvertWhitespaceToSpacesRegex(((MeshSource)sources[lookup["NORMAL"].ToString()]).floatArrayContent));
            template = template.Replace("(normal_assessor_count)", ((MeshSource)sources[lookup["NORMAL"].ToString()]).accessorCount);
            template = template.Replace("(texcoord_count)", ((MeshSource)sources[lookup["TEXCOORD"].ToString()]).floatCount);
            template = template.Replace("(texcoord_content)", ConvertWhitespaceToSpacesRegex(((MeshSource)sources[lookup["TEXCOORD"].ToString()]).floatArrayContent ));
            template = template.Replace("(texcoord_assessor_count)", ((MeshSource)sources[lookup["TEXCOORD"].ToString()]).accessorCount);
            template = template.Replace("(triangle_count)", triangleCount);
            template = template.Replace("(triangle_content)", ConvertWhitespaceToSpacesRegex(pContent));




          
            // write out template file
            TextWriter tw = new StreamWriter(outputDAE);
            tw.Write(template);
            tw.Close();


           // MessageBox.Show("Done converting DAE.  Now running RC");
            return true;
   
        }

        private bool runThroughRC(string filename) { 
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents=false;
            proc.StartInfo.FileName=tRCfile.Text;
            proc.StartInfo.Arguments = "\"" + filename+"\"";
            proc.Start();
            proc.WaitForExit();
            //MessageBox.Show("RC processed");
            return true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tMeshName_TextChanged(object sender, EventArgs e)
        {
            tCrysisResourceFolder.Text = "Objects/"+ tMeshName.Text + "/";
        }

        private void tCrysisResourceFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private bool buildPrefab(string MeshName, List<string> cloudFiles, string outputFolder, string CrysisResourceFolder)
        {

            // write out prefab file
            TextWriter tw = new StreamWriter(outputFolder + "/" + MeshName + "_prefab.xml");




            Guid uuid = Guid.NewGuid();

            tw.Write("<PrefabsLibrary Name=\""+MeshName+"_prefab\">\n");

            tw.Write(" <Prefab Name=\""+MeshName+"_prefab01\" Id=\"{"+uuid.ToString()+"}\">\n");
            tw.Write("  <Objects>\n");

            foreach (string cloudname in cloudFiles)
            {
                tw.Write("   <Object ");
                tw.Write("Type=\"Brush\" ");
                tw.Write("Layer=\"Main\" ");
                uuid = Guid.NewGuid();
                tw.Write("Id=\"{"+uuid.ToString()+"}\" ");
                tw.Write("Name=\""+cloudname+"\" ");
                tw.Write("Pos=\"0,0,0\" ");
                tw.Write("Rotate=\"0,0,0,0\" ");
                tw.Write("ColorRGB=\"16777215\" ");
                tw.Write("MatLayersMask=\"0\" ");
                tw.Write("Prefab=\""+CrysisResourceFolder+cloudname+".cfg\" ");
                tw.Write("OutdoorOnly=\"0\" ");
                tw.Write("CastShadowMaps=\"1\" ");
                tw.Write("SupportSecondVisarea=\"0\" ");
                tw.Write("CastRAMmap=\"0\" ");
                tw.Write("ReceiveRAMmap=\"0\" ");
                tw.Write("Hideable=\"0\" ");
                tw.Write("LodRatio=\"100\" ");
                tw.Write("ViewDistRatio=\"100\" ");
                tw.Write("NotTriangulate=\"0\" ");
                tw.Write("AIRadius=\"-1\" ");
                tw.Write("NoStaticDecals=\"0\" ");
                tw.Write("RAMmapQuality=\"1\" ");
                tw.Write("NoAmnbShadowCaster=\"0\" ");
                tw.Write("RecvWind=\"0\" ");
                tw.Write("RndFlags=\"8\"");
                tw.Write("/>\n");
            }

            tw.Write("  </Objects>\n");
            tw.Write(" </Prefab>\n");
            tw.Write("</PrefabsLibrary>\n");



            tw.WriteLine();
            tw.Close();

            return true;
        }

        /// <summary>
        /// Converts all whitespace in the string to spaces using Regex.
        /// </summary>
        public static string ConvertWhitespaceToSpacesRegex(string value)
        {
            value = Regex.Replace(value, "[\n\r\t]", " ");
            return value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\settings.txt"))
            {
                loadSettings();
            }
            else
            {
                saveSettings();
            }
        }

        private void loadSettings()
        {
            List<string> settings = (System.IO.File.ReadAllLines(Application.StartupPath + "\\settings.txt")).ToList();
            tRCfile.Text = settings[0];
            tInputDAE.Text = settings[1];
            tOutputFolder.Text = settings[2];
            tMeshName.Text = settings[3];
        }

        private void saveSettings()
        {
            // compile settings
            string settings = tRCfile.Text + "\n" + tInputDAE.Text + "\n" + tOutputFolder.Text + "\n" + tMeshName.Text;


            // write out settings
            TextWriter tw = new StreamWriter(Application.StartupPath + "\\settings.txt");
            tw.Write(settings);
            tw.Close();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

    }
}
