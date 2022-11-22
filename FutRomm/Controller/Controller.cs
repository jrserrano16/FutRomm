using FutRomm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FutRomm.Controller
{
        public class Controller
        {
            public static string xmlFile = "Assets//FutRomm.xml";
            public static string csvfile = @"C://Users//USUARIO//Desktop//FIFA23_LaLiga2_data.csv";


            public static List<Player> loadPlayers()
            {

                XmlDocument docSol = new XmlDocument();
                docSol.Load(xmlFile);
                XmlNode root = docSol.SelectSingleNode("FutRom");
                XmlNode playersRoot = root.SelectSingleNode("Players");
                List<Player> listPlayer = new List<Player>();
                foreach (XmlNode node in playersRoot.ChildNodes)
                {
                    Player player = new Player();
                    player.id = Convert.ToInt32(node.Attributes["ID"].Value);
                    player.name = node.Attributes["Name"].Value;
                    player.age = Convert.ToInt32(node.Attributes["Age"].Value);
                    player.photo = node.Attributes["Photo"].Value;
                    player.nationality = node.Attributes["Nationality"].Value;
                    player.flag = node.Attributes["Flag"].Value;
                    player.club = node.Attributes["Club"].Value;
                    player.club_logo = node.Attributes["Club_Logo"].Value;
                    player.foot = node.Attributes["Foot"].Value;
                    player.position = node.Attributes["Position"].Value;
                    player.kit_number = Convert.ToInt32(node.Attributes["Kit_Number"].Value);
                    listPlayer.Add(player);

                    //player.url_nombre = "ms-appx:///Assets/no-pictures.png";
                }
                return listPlayer;
            }



            public static void addPlayer()
            {
                StreamReader archivo = new StreamReader(csvfile);
                string linea;

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);
                XmlNode futRom = doc.SelectSingleNode("FutRom");
                XmlNode playersRoot = futRom.SelectSingleNode("Players");


                while ((linea = archivo.ReadLine()) != null)
                {

                    string[] fila = linea.Split(',');
                    XmlElement playerNode = doc.CreateElement("Player");

                    XmlAttribute id = doc.CreateAttribute("ID");
                    id.Value = fila[0].ToString();
                    playerNode.Attributes.Append(id);

                    XmlAttribute name = doc.CreateAttribute("Name");
                    name.Value = fila[1];
                    playerNode.Attributes.Append(name);

                    XmlAttribute age = doc.CreateAttribute("Age");
                    age.Value = fila[2];
                    playerNode.Attributes.Append(age);

                    XmlAttribute photo = doc.CreateAttribute("Photo");
                    photo.Value = fila[3];
                    playerNode.Attributes.Append(photo);

                    XmlAttribute nationality = doc.CreateAttribute("Nationality");
                    nationality.Value = fila[4];
                    playerNode.Attributes.Append(nationality);

                    XmlAttribute flag = doc.CreateAttribute("Flag");
                    flag.Value = fila[5];
                    playerNode.Attributes.Append(flag);

                    XmlAttribute club = doc.CreateAttribute("Club");
                    club.Value = fila[8];
                    playerNode.Attributes.Append(club);

                    XmlAttribute club_logo = doc.CreateAttribute("Club_Logo");
                    club_logo.Value = fila[9];
                    playerNode.Attributes.Append(club_logo);

                    XmlAttribute foot = doc.CreateAttribute("Foot");
                    foot.Value = fila[13];
                    playerNode.Attributes.Append(foot);

                    XmlAttribute position = doc.CreateAttribute("Position");
                    string[] csvPosString = fila[20].Split('>');
                    position.Value = csvPosString[1];
                    playerNode.Attributes.Append(position);

                    XmlAttribute kit_number = doc.CreateAttribute("Kit_Number");
                    kit_number.Value = fila[28];
                    playerNode.Attributes.Append(kit_number);

                    playersRoot.AppendChild(playerNode);

                }
                doc.Save(xmlFile);
            }


            public static void loadData()
            {
                string file = @"C://Users//USUARIO//Desktop//FIFA23_official_data.csv";
                string file2 = @"C://Users//USUARIO//Desktop//FIFA23_LaLiga2_data.csv";

                var lines = File.ReadAllLines(file);
                var linesToWrite = new List<string>();

                foreach (var s in lines)
                {
                    var split = s.Split(',');
                    if (!split[8].Equals("Unión Deportiva Almería"))
                    {
                        linesToWrite.Remove(s);
                    }
                    else
                        linesToWrite.Add(s);

                }

                File.WriteAllLines(file2, linesToWrite);
            }
        }
    
}