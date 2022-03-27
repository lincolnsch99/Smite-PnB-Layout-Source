using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smite_PnB_Layout
{
    [System.Serializable]
    public class SubmissionHistory
    {
        private List<BanDataSubmission> banDataSubmissions;
        public List<BanDataSubmission> BanDataSubmissions { get => banDataSubmissions; set => banDataSubmissions = value; }

        private int maxSubmissions, numSubmissions;
        public int MaxSubmission { get => maxSubmissions; set => maxSubmissions = value; }
        public int NumSubmissions { get => numSubmissions; set => numSubmissions = value; }

        public SubmissionHistory(int maxSubmissions)
        {
            this.maxSubmissions = maxSubmissions;
            numSubmissions = 0;
            banDataSubmissions = new List<BanDataSubmission>();
        }

        public void AddSubmission(BanDataSubmission submission)
        {
            if(numSubmissions >= maxSubmissions - 1)
            {
                banDataSubmissions.RemoveAt(0);
                banDataSubmissions.Insert(numSubmissions, submission);
            }
            else
            {
                banDataSubmissions.Insert(numSubmissions, submission);
                numSubmissions += 1;
            }
        }

        public BanDataSubmission RemoveSubmission(int removeAt)
        {
            BanDataSubmission submission = banDataSubmissions[removeAt];
            banDataSubmissions.RemoveAt(removeAt);
            return submission;
        }

        public void SaveToFile(string filePath)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(filePath, json);
        }

        public static SubmissionHistory CreateFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return JsonSerializer.Deserialize<SubmissionHistory>(json, options);
        }
    }
}
