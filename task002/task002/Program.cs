using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO.Compression;
using Org.BouncyCastle.Crypto.Digests;

namespace CalculateSHA3
{
    class Program
    {
        // Function to calculate SHA3-256 hash of a file
        static string CalculateSHA3(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var sha3 = new Sha3Digest(256);
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    sha3.BlockUpdate(buffer, 0, bytesRead);
                }
                byte[] hashBytes = new byte[sha3.GetDigestSize()];
                sha3.DoFinal(hashBytes, 0);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Function to download and extract the ZIP archive
        static void DownloadAndExtractArchive(string url, string extractFolder)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, "task2.zip");
            }
            ZipFile.ExtractToDirectory("task2.zip", extractFolder);
            File.Delete("task2.zip");
        }

        static void Main(string[] args)
        {
            string url = "https://www.dropbox.com/s/oy2668zp1lsuseh/task2.zip?dl=1";
            string extractFolder = @"C:\Users\USER\Downloads\task2"; // Replace with your directory path

            try
            {
                // Download and extract the ZIP archive
                DownloadAndExtractArchive(url, extractFolder);

                // Calculate SHA3-256 hash for each file in the extracted folder
                var hashes = new List<string>();
                foreach (var file in Directory.GetFiles(extractFolder))
                {
                    string fileHash = CalculateSHA3(file);
                    hashes.Add(fileHash);
                }

                // Sort hashes in ascending order
                hashes.Sort();

                // Concatenate sorted hashes without separator
                string concatenatedHashes = string.Concat(hashes);

                // Your email
                string email = "email.used@for.registration.here";

                // Concatenate email to the end
                string resultString = concatenatedHashes + email;

                // Calculate SHA3-256 hash of the final concatenated string
                using (var sha3 = new Sha3Digest(256))
                {
                    byte[] resultBytes = Encoding.UTF8.GetBytes(resultString);
                    sha3.BlockUpdate(resultBytes, 0, resultBytes.Length);
                    byte[] finalHashBytes = new byte[sha3.GetDigestSize()];
                    sha3.DoFinal(finalHashBytes, 0);

                    // Convert final hash to hexadecimal string
                    string finalHash = BitConverter.ToString(finalHashBytes).Replace("-", "").ToLower();

                    // Print or use finalHash as needed
                    Console.WriteLine("Final hash: " + finalHash);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
