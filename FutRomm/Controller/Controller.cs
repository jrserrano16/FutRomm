using FutRomm.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml;
using static FutRomm.Model.LeagueSearch;
using static FutRomm.Model.NationsSearch;
using static FutRomm.Model.PlayersSearch;
using static FutRomm.Model.TeamApi;

namespace FutRomm.Controller
{
    public static class Controller
    {
        public static string xmlFile = "Assets//FutRomm.xml";
        public static string xmlFile2 = "Assets//FutRomm_All.xml";
        public static string csvfile = @"C://Users//USUARIO//Desktop//FIFA23_LaLiga2_data.csv";



        public static void deleteNodes()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Players");
            XmlNode teamsRoot = futRom.SelectSingleNode("Teams");

            foreach (XmlNode team in teamsRoot.ChildNodes)
            {
                int idTeam = Convert.ToInt32(team.Attributes["League"].Value.ToString());
                if (idTeam!=10 && idTeam!=13 && idTeam!=16 && idTeam!=19 &&idTeam!=31 && idTeam!=53 && idTeam!=308 && idTeam!=1000)
                {
                    teamsRoot.RemoveChild(team);
                }
            }
            foreach (XmlNode player in playersRoot.ChildNodes)
            {
                int idPlayer = Convert.ToInt32(player.Attributes["Liga"].Value.ToString());
                if (idPlayer!=10 && idPlayer!=13 && idPlayer!=16 && idPlayer!=19 &&idPlayer!=31 && idPlayer!=53 && idPlayer!=308 && idPlayer!=1000)
                {
                    playersRoot.RemoveChild(player);
                }
            }
            doc.Save(xmlFile2);
        }

        public static List<Question> loadQuestions()
        {
            XmlDocument docSol = new XmlDocument();
            docSol.Load(xmlFile2);
            XmlNode root = docSol.SelectSingleNode("FutRomm");
            XmlNode playersRoot = root.SelectSingleNode("Questions");
            List<Question> listQuestions = new List<Question>();
            foreach (XmlNode node in playersRoot.ChildNodes)
            {
                Question question = new Question();
                question.question = node.Attributes["Question"].Value;
                question.answers = node.Attributes["Answer"].Value.Split(',');
                question.solution = node.Attributes["Solution"].Value;

                listQuestions.Add(question);
            }
            return listQuestions;
        }


        public static List<Player> loadPlayers()
        {
            XmlDocument docSol = new XmlDocument();
            docSol.Load(xmlFile2);
            XmlNode root = docSol.SelectSingleNode("FutRomm");
            XmlNode playersRoot = root.SelectSingleNode("Players");
            List<Player> listPlayer = new List<Player>();
            foreach (XmlNode node in playersRoot.ChildNodes)
            {
                Player player = new Player();
                player.id = Convert.ToInt32(node.Attributes["ID"].Value);
                player.name = node.Attributes["Name"].Value;
                player.age = Convert.ToInt32(node.Attributes["Age"].Value);
                player.photo = node.Attributes["Photo"].Value;
                player.nation= node.Attributes["Nation"].Value;
                player.nation_id= Convert.ToInt32(node.Attributes["Nation_ID"].Value);
                player.nation_logo = "ms-appx:///Assets/nations/"+player.nation_id+".png";

                player.club = node.Attributes["Club"].Value;
                player.club_id = Convert.ToInt32(node.Attributes["Club_ID"].Value);
                player.club_logo = "ms-appx:///Assets/teams/"+player.club_id+".png";

                player.league = node.Attributes["League"].Value;
                player.league_id = Convert.ToInt32(node.Attributes["League_ID"].Value);
                player.league_logo = "ms-appx:///Assets/leagues/"+player.league_id+".png";

                player.foot = node.Attributes["Foot"].Value;
                player.position = node.Attributes["Position"].Value;
                listPlayer.Add(player);
            }
            return listPlayer;
        }

        public static void updatePlayer()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Players");
            XmlNode teamsRoot = futRom.SelectSingleNode("Teams");
            XmlNode nationsRoot = futRom.SelectSingleNode("Nations");
            XmlNode leaguesRoot = futRom.SelectSingleNode("Leagues");

            foreach (XmlNode player in playersRoot)
            {
                foreach (XmlNode team in teamsRoot)
                {
                    if (Convert.ToInt32(player.Attributes["Club_ID"].Value.ToString()) == Convert.ToInt32(team.Attributes["ID"].Value.ToString()))
                    {
                        XmlAttribute club = doc.CreateAttribute("Club");
                        club.Value = team.Attributes["Name"].Value.ToString();
                        player.Attributes.Append(club);
                        break;
                    }
                }
                foreach (XmlNode nation in nationsRoot)
                {
                    if (Convert.ToInt32(player.Attributes["Nation_ID"].Value.ToString()) == Convert.ToInt32(nation.Attributes["ID"].Value.ToString()))
                    {
                        XmlAttribute nation1 = doc.CreateAttribute("Nation");
                        nation1.Value = nation.Attributes["Name"].Value.ToString();
                        player.Attributes.Append(nation1);
                        break;
                    }
                }
                foreach (XmlNode league in leaguesRoot)
                {

                    if (Convert.ToInt32(player.Attributes["League_ID"].Value.ToString()) == Convert.ToInt32(league.Attributes["ID"].Value.ToString()))
                    {
                        XmlAttribute league1 = doc.CreateAttribute("League");
                        league1.Value = league.Attributes["Name"].Value.ToString();
                        player.Attributes.Append(league1);
                        break;
                    }
                }
            }

            doc.Save(xmlFile2);
        }

        public static PlayersResult GetPlayers(int id)
        {
            var client = new RestClient($"https://futdb.app/api/players?page={id}");
            client.AddDefaultHeader("accept", "application/json");
            client.AddDefaultHeader("X-AUTH-TOKEN", "7bcdb557-3e27-4627-a4a5-40e6ef01cc16");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                PlayersResult result = JsonConvert.DeserializeObject<PlayersResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }



        public static void updateNation()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode nodeRoot = futRom.SelectSingleNode("Nations");
            foreach (XmlNode child in nodeRoot)
            {
                XmlAttribute image = doc.CreateAttribute("Image");
                image.Value = "Assets/nations/"+child.Attributes["ID"].Value.ToString()+".png";
                child.Attributes.Append(image);
            }

            doc.Save(xmlFile2);
        }

        public static NationsResult GetNations(int id)
        {
            var client = new RestClient($"https://futdb.app/api/nations?page={id}");
            client.AddDefaultHeader("accept", "application/json");
            client.AddDefaultHeader("X-AUTH-TOKEN", "7bcdb557-3e27-4627-a4a5-40e6ef01cc16");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                NationsResult result = JsonConvert.DeserializeObject<NationsResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
        public static LeagueResult GetLeagues(int id)
        {
            var client = new RestClient($"https://futdb.app/api/leagues?page={id}");
            client.AddDefaultHeader("accept", "application/json");
            client.AddDefaultHeader("X-AUTH-TOKEN", "7bcdb557-3e27-4627-a4a5-40e6ef01cc16");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                LeagueResult result = JsonConvert.DeserializeObject<LeagueResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
        public static TeamsResult GetTeams(int id)
        {
            var client = new RestClient($"https://futdb.app/api/clubs?page={id}");
            client.AddDefaultHeader("accept", "application/json");
            client.AddDefaultHeader("X-AUTH-TOKEN", "7bcdb557-3e27-4627-a4a5-40e6ef01cc16");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                TeamsResult result = JsonConvert.DeserializeObject<TeamsResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void addPlayer(PlayerR p)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Players");
            XmlElement playerNode = doc.CreateElement("Player");

            XmlAttribute id = doc.CreateAttribute("ID");
            id.Value = p.id.ToString();
            playerNode.Attributes.Append(id);

            XmlAttribute name = doc.CreateAttribute("Name");
            name.Value = p.commonName.ToString();
            playerNode.Attributes.Append(name);

            XmlAttribute age = doc.CreateAttribute("Age");
            age.Value = p.age.ToString();
            playerNode.Attributes.Append(age);

            XmlAttribute photo = doc.CreateAttribute("Photo");
            photo.Value = "https://fifastatic.fifaindex.com/FIFA23/players/"+p.resourceId+".png";
            playerNode.Attributes.Append(photo);

            XmlAttribute nationality = doc.CreateAttribute("Nationality");
            nationality.Value = p.nation.ToString();
            playerNode.Attributes.Append(nationality);

            XmlAttribute club = doc.CreateAttribute("Club");
            club.Value = p.club.ToString();
            playerNode.Attributes.Append(club);

            XmlAttribute league = doc.CreateAttribute("Liga");
            league.Value = p.league.ToString();
            playerNode.Attributes.Append(league);

            XmlAttribute foot = doc.CreateAttribute("Foot");
            foot.Value = p.foot;
            playerNode.Attributes.Append(foot);

            XmlAttribute position = doc.CreateAttribute("Position");
            position.Value = p.position.ToString();
            playerNode.Attributes.Append(position);

            playersRoot.AppendChild(playerNode);

            doc.Save(xmlFile2);
        }

        public static void addNation(NationR p)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Nations");
            XmlElement playerNode = doc.CreateElement("Nation");

            XmlAttribute id = doc.CreateAttribute("ID");
            id.Value = p.id.ToString();
            playerNode.Attributes.Append(id);

            XmlAttribute name = doc.CreateAttribute("Name");
            name.Value = p.name.ToString();
            playerNode.Attributes.Append(name);

            XmlAttribute photo = doc.CreateAttribute("Photo");
            photo.Value = "no foto";
            playerNode.Attributes.Append(photo);

            playersRoot.AppendChild(playerNode);

            doc.Save(xmlFile2);
        }

        public static void addleague(LeagueR p)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Leagues");
            XmlElement playerNode = doc.CreateElement("League");

            XmlAttribute id = doc.CreateAttribute("ID");
            id.Value = p.id.ToString();
            playerNode.Attributes.Append(id);

            XmlAttribute name = doc.CreateAttribute("Name");
            name.Value = p.name.ToString();
            playerNode.Attributes.Append(name);

            XmlAttribute photo = doc.CreateAttribute("Photo");
            photo.Value = "no foto";
            playerNode.Attributes.Append(photo);

            playersRoot.AppendChild(playerNode);

            doc.Save(xmlFile2);
        }

        public static void addTeams(TeamR p)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile2);
            XmlNode futRom = doc.SelectSingleNode("FutRomm");
            XmlNode playersRoot = futRom.SelectSingleNode("Teams");
            XmlElement playerNode = doc.CreateElement("Team");

            XmlAttribute id = doc.CreateAttribute("ID");
            id.Value = p.id.ToString();
            playerNode.Attributes.Append(id);

            XmlAttribute name = doc.CreateAttribute("Name");
            name.Value = p.name.ToString();
            playerNode.Attributes.Append(name);

            XmlAttribute photo = doc.CreateAttribute("Photo");
            photo.Value = "no foto";
            playerNode.Attributes.Append(photo);

            XmlAttribute league = doc.CreateAttribute("League");
            if (p.league != null)
            {
                league.Value = p.league.ToString();
            }
            else
            {
                league.Value = "1000";
            }

            playerNode.Attributes.Append(league);

            playersRoot.AppendChild(playerNode);

            doc.Save(xmlFile2);
        }
    }
}


