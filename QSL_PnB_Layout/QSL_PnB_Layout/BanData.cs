using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Smite_PnB_Layout
{
    [System.Serializable]
    public class BanData
    {
        [JsonPropertyName("bancounts")]
        public Dictionary<string, int> BanCounts { get; set; }

        [JsonPropertyName("totalgames")]
        public int TotalGames { get; set; }

        [JsonPropertyName("teamname")]
        public string TeamName { get; set; }

        [JsonConstructor]
        public BanData() { }

        public BanData(string teamName, string resourcesPath)
        {
            BanCounts = new Dictionary<string, int>();
            this.TeamName = teamName;
            try
            {
                TotalGames = 0;
                StreamReader reader = new StreamReader(resourcesPath + "/CharactersList.txt");
                string line = "";
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        BanCounts.Add(line, 0);
                    }
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.StackTrace);
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                File.CreateText(resourcesPath + "/CharactersList.txt").Close();
                Console.WriteLine(dnfe.StackTrace);
            }
        }

        public void SaveToFile(string filePath)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize<BanData>(this, options);
            File.WriteAllText(filePath, json);
        }

        public void CheckForAllGods(string resourcesPath)
        {
            StreamReader reader = new StreamReader(resourcesPath + "/CharactersList.txt");
            string line = "";
            try
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if(!BanCounts.Keys.Contains(line))
                        BanCounts.Add(line, 0);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.StackTrace);
            }
            finally
            {
                reader.Close();
            }
        }

        public static BanData CreateFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<BanData>(json); 
        }

        public void AddBan(string godName)
        {
            if (BanCounts.ContainsKey(godName))
                BanCounts[godName] += 1;
            else
                BanCounts[godName] = 1;
        }

        public Dictionary<string, float> GetTopNBans(int n)
        {
            Dictionary<string, float> topN = new Dictionary<string, float>();
            Dictionary<string, int> duplicate = new Dictionary<string, int>(BanCounts);
            while(topN.Keys.Count < n)
            {
                string highestKey = GetKeyWithHighestValue(duplicate);
                if (highestKey != "")
                {
                    topN.Add(highestKey, (float)Math.Round((double)duplicate[highestKey] / (double)TotalGames, 2));
                    duplicate.Remove(highestKey);
                }
                else
                    break;
            }
            return topN;
        }

        private string GetKeyWithHighestValue(Dictionary<string, int> dict)
        {
            int highestValue = 0;
            string highestKey = "";

            foreach(string key in dict.Keys)
            {
                if(dict[key] >= highestValue)
                {
                    highestValue = dict[key];
                    highestKey = key;
                }
            }

            return highestKey;
        }
    }
}
