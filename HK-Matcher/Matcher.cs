using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HK_Matcher
{
    public class Matcher
    {
        private readonly String path1, path2;
        private readonly MainWindow reference;
        private readonly SHA512 provider;

        public Matcher(String path1, String path2, MainWindow reference)
        {
            this.reference = reference;

            if(!(path1 == null || path1.Trim() == ""))
                this.path1 = path1;
            else
            {
                reference.Output.Text = "Invalid first file path, please check your path. It must end in \"hollow_knight_Data\".";
                return;
            }

            if(!(path2 == null || path2.Trim() == ""))
                this.path2 = path2;
            else
            {
                reference.Output.Text = "Invalid second file path, please check your path. It must end in \"hollow_knight_Data\".";
                return;
            }

            provider = SHA512.Create();
        }

        public void Run()
        {
            Boolean matchFailed = false;
            List<String> firstFiles = Directory.GetFiles(path1, "level*").ToList();
            List<String> secondFiles = Directory.GetFiles(path2, "level*").ToList();

            if(firstFiles.Count != secondFiles.Count)
            {
                reference.Output.Text = "Room files were added or removed, unequal match.";
                matchFailed = true;
                goto FAILED;
            }

            reference.Output.Text = $"{firstFiles.Count} room files detected.\n";

            for(int i = 0; i < firstFiles.Count; i++)
            {
                if(provider.ComputeHash(File.OpenRead(firstFiles[i])).SequenceEqual(provider.ComputeHash(File.OpenRead(secondFiles[i]))))
                    continue;

                reference.Output.Text += $"Match failed for room {i}\n";
                matchFailed = true;
            }

            FAILED:

            if(!matchFailed)
            {
                reference.Output.Text += $"Match succeeded, all room files are equal.";
            }
        }
    }
}
