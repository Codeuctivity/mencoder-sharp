using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MencoderSharp
{
    /// <summary>
    /// The <see cref="MencoderSharp"/> Namespace contains wrapperclasses for async and sync calls of mencoder. Keep the mencoderbinary (mencoder.exe) in the same directory as the instancing assembly. Documentation and Assembly - http://code.google.com/p/mencoder-sharp/ (GPLv3) Stefan Seeland
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Baseclass for async and syncmencodercalls
    /// </summary>
    public abstract class MencoderBase : IDisposable
    {
        private bool customMencoderLocation;
        private readonly object lockObject = new object();
        private bool isInitilized;

        /// <summary>
        /// The path to mencoder exe
        /// </summary>
        public string PathToExternalMencoderBin { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        public MencoderBase()
        {
            PathToExternalMencoderBin = GetPathToMencoderBin();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        /// <param name="pathToExternalMencoderBin">The path to external mencoder bin.</param>
        public MencoderBase(string pathToExternalMencoderBin)
        {
            customMencoderLocation = true;
            PathToExternalMencoderBin = pathToExternalMencoderBin;
        }

        /// <summary>
        /// Gets the path to mencoder bin.
        /// </summary>
        /// <returns></returns>
        private string GetPathToMencoderBin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (isInitilized)
                {
                    return PathToExternalMencoderBin;
                }

                lock (lockObject)
                {
                    isInitilized = true;

                    var tempDirectoryPath = Path.GetTempPath();
                    var mencoderFileName = "mencoder" + Guid.NewGuid() + ".exe";
                    var mencoderPath = Path.Combine(tempDirectoryPath, mencoderFileName);
                    if (!File.Exists(mencoderPath))
                    {
                        ExtractBinaryFromManifest("MencoderSharp.mencoder.exe", tempDirectoryPath, mencoderFileName);
                    }
                    return mencoderPath;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                customMencoderLocation = true;
                const string wellKnownPathToMencoder = "/usr/bin/mencoder";
                if (File.Exists(wellKnownPathToMencoder))
                {
                    return wellKnownPathToMencoder;
                }

                throw new FileNotFoundException("Mencoder was not found at " + wellKnownPathToMencoder + ". Install mencoder using 'apt install mencoder'.");
            }
            else
            {
                throw new NotImplementedException("Sorry, only supporting linux and windows.");
            }
        }

        private void ExtractBinaryFromManifest(string resourceName, string tempDirectoryPathpath, string mencoderFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var fileStream = File.Create(Path.Combine(tempDirectoryPathpath, mencoderFileName)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }

        private bool disposedValue = false;

        /// <summary>
        /// Disposes the mencoder bin
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                if (!customMencoderLocation && File.Exists(PathToExternalMencoderBin))
                {
                    foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(PathToExternalMencoderBin)))
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                            Task.Delay(5).Wait();
                        }
                    }
                    File.Delete(PathToExternalMencoderBin);
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        ~MencoderBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the mencoder bin
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}