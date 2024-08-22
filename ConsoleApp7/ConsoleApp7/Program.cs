using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

class Program
{
    // Function to calculate SHA3-256 hash of a file using BouncyCastle
    static async Task<string> CalculateSHA3Async(string filePath)
    {
        var sha3 = new Sha3Digest(256); // 256 for SHA3-256
        byte[] buffer = new byte[8192];
        int bytesRead;

        using (var stream = File.OpenRead(filePath))
        {
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                sha3.BlockUpdate(buffer, 0, bytesRead);
            }
        }

        var hash = new byte[sha3.GetDigestSize()];
        sha3.DoFinal(hash, 0);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    static async Task Main(string[] args)
    {
        string extractFolder = @"C:\Users\USER\Downloads\task2"; // Target folder where files are extracted
        string userEmail = "arifulislam546950@gmail.com"; // Replace with your email in lowercase

        try
        {
            // Get list of files in the extracted folder
            var files = Directory.GetFiles(extractFolder);

            // Ensure we're processing exactly 256 files (assuming they are all required)
            if (files.Length != 256)
            {
                throw new Exception($"Expected exactly 256 files, but found {files.Length}");
            }

            // Calculate SHA3-256 hash for each file
            var hashTasks = files.Select(file => CalculateSHA3Async(file)).ToList();
            var hashes = await Task.WhenAll(hashTasks);

            // Sort hashes in ascending order
            Array.Sort(hashes);

            // Concatenate sorted hashes manually without separator
            var concatenatedHashes = new StringBuilder();
            foreach (var hash in hashes)
            {
                concatenatedHashes.Append(hash);
            }

            // Append email in lowercase
            concatenatedHashes.Append(userEmail.ToLower());

            // Calculate SHA3-256 hash of the concatenated string
            var finalHashBytes = new Sha3Digest(256).DoFinal(Encoding.UTF8.GetBytes(concatenatedHashes.ToString()));
            var finalHash = BitConverter.ToString(finalHashBytes).Replace("-", "").ToLower();

            // Print or use finalHash as needed
            Console.WriteLine("Final hash: " + finalHash);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}

public static class Sha3DigestExtensions
{
    public static byte[] DoFinal(this Sha3Digest digest, byte[] input)
    {
        digest.BlockUpdate(input, 0, input.Length);
        var result = new byte[digest.GetDigestSize()];
        digest.DoFinal(result, 0);
        return result;
    }
}
