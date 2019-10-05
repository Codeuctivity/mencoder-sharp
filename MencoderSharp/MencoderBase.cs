using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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
    /// Mencoderparamters
    /// </summary>
    internal struct MencoderParameters
    {
        public string source;
        public string destination;
        public string videoParameter;
        public string audioParameter;
    }

    /// <summary>
    /// Baseclass for async and syncmencodercalls
    /// </summary>
    public abstract class MencoderBase
    {
        private bool customMencoderLocation;

        /// <summary>
        /// The path to mencoder exe
        /// </summary>
        public string PathToExternalMencoderBin { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        public MencoderBase()
        {
            PathToExternalMencoderBin = getPathToMencoderBin();
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
        /// Finalizes an instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        ~MencoderBase()
        {
            //TODO dont use destructor
            if (!customMencoderLocation)
                File.Delete(PathToExternalMencoderBin);
        }

        /// <summary>
        /// Gets the path to mencoder bin.
        /// </summary>
        /// <returns></returns>
        public string getPathToMencoderBin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string tempDirectoryPath = Path.GetTempPath();
                string mencoderFileName = "mencoder" + Guid.NewGuid() + ".exe";
                string mencoderPath = Path.Combine(tempDirectoryPath, mencoderFileName);
                if (!File.Exists(mencoderPath))
                    ExtractBinaryFromManifest("MencoderSharp.mencoder.exe", tempDirectoryPath, mencoderFileName);
                return mencoderPath;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                customMencoderLocation = true;
                const string wellKnownPathToMencoder = "/usr/bin/mencoder";
                if (File.Exists(wellKnownPathToMencoder))
                    return wellKnownPathToMencoder;
                throw new FileNotFoundException("Mencoder was not found at " + wellKnownPathToMencoder + ". Install mendocer using apt install mencoder.");
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
    }
}