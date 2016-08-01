/*
 * 
 * HEADER WRITER
 * Miguel Ramos Carretero
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeaderWriter
{
    /// <summary>
    /// Writes a text header to a set of files from a directory given a certain filter.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main program. Holds the whole functionality.
        /// </summary>
        /// <param name="args">
        /// Use with 3 arguments: HeaderWriter.exe (path to file with header text) (path to folder that contains the files where the header should be written) (filter)\n
        /// Example: HeaderWriter.exe c:/headertext.txt c:/destfolder *.cs");
        /// </param>
        /// <remarks>
        /// For safety reasons, it is recommended to make a copy of the files before running the program. 
        /// A backup of the original files will also be provided by the program.
        /// </remarks>
        static void Main(string[] args)
        {
            byte[] headerBytes;
            string[] filesFound;

            //Control the arguments
            if (args.Length != 3)
            {
                var origColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine();
                Console.WriteLine("Use: HeaderWriter.exe <path to file with header text> <path to folder that contains the files where the header should be written> <filter>");
                Console.WriteLine();

                Console.WriteLine("Example: HeaderWriter.exe c:/headertext.txt c:/destfolder *.cs");

                Console.ForegroundColor = origColor;

                return;
            }

            //Read the header file
            if (File.Exists(args[0]))
            {
                var headerFile = File.Open(args[0], FileMode.Open);
                int numberOfBytes = (int)headerFile.Length;
                headerBytes = new byte[numberOfBytes];
                headerFile.Read(headerBytes, 0, numberOfBytes);

                headerFile.Dispose();
                headerFile.Close();

                Console.WriteLine("The header file has " + numberOfBytes + " bytes");
            }
            else
            {
                Console.WriteLine("The header file does not exists");
                Console.ReadLine();
                return;
            }

            //Get the files that match the filter from the given folder
            if (Directory.Exists(args[1]))
            {
                filesFound = Directory.GetFiles(args[1], args[2], SearchOption.AllDirectories);
                Console.WriteLine(filesFound.Length + " files found with filter " + args[2]);
            }
            else
            {
                Console.WriteLine("The destination folder does not exists");
                Console.ReadLine();
                return;
            }

            //Create a backup folder
            if (!Directory.Exists(args[1] + "/Backup"))
                Directory.CreateDirectory(args[1] + "/Backup");

            //Write the header for each file
            foreach(string S in filesFound)
            {
                Console.WriteLine("Adding header to " + S);

                var tempFile = File.Open(S+"~", FileMode.Create);
                tempFile.Write(headerBytes, 0, headerBytes.Length);

                var originalFile = File.Open(S, FileMode.Open);
                originalFile.CopyTo(tempFile);
                
                originalFile.Dispose();
                originalFile.Close();

                tempFile.Dispose();
                tempFile.Close();

                try
                {
                    File.Replace(S + "~", S, S + ".bk");
                    File.Move(S + ".bk", args[1] + "/Backup/" + System.IO.Path.GetFileName(S));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception catched: " + e.Message);
                }
            }

            Console.WriteLine("Process completed");
            Console.ReadLine();
        }
    }
}
