using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace lukeIam.nuget.tasks.FilesExists
{
    public class FilesExistsTask: Task
    {
        [Required]
        public string Files { get; set; }

        [Required]
        public string Folder { get; set; }

        [Output]
        public bool Result { get; set; }

        public override bool Execute()
        {
            if (String.IsNullOrEmpty(Files) || String.IsNullOrEmpty(Folder))
            {
                Log.LogError("Parameter must be not emty");
                return false;
            }

            Result = true;
            foreach (string file in Files.Split(';'))
            {
                try
                {
                    if (!File.Exists(Path.Combine(Folder, file)))
                    {
                        Result = false;
                        break;
                    }
                }
                catch (ArgumentException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
            }

            return true;
        }
    }
}
