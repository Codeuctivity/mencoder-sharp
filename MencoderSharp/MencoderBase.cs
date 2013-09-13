using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace MencoderSharp
{
    /// <summary>
    /// The <see cref="MencoderSharp"/> Namespace contains wrapperclasses for async and sync calls of mencoder. Keep the mencoderbinary (mencoder.exe) in the same directory as the instancing assembly. Documentation and Assembly - http://code.google.com/p/mencoder-sharp/ (GPLv3) Stefan Seeland
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }
    /// <summary>
    /// Mencoderparamters
    /// </summary>
    struct MencoderParameters
    {
        public string source;
        public string destination;
        public string videoParameter;
        public string audioParameter;
    }
    /// <summary>
    /// Baseclass for async and syncmencodercalls
    /// </summary>
    public  class MencoderBase
    {

        /// <summary>
        /// The path to mencoder exe
        /// </summary>
        internal string pathToMencoderExe;
        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        public MencoderBase()
        {
            pathToMencoderExe = getPathToMencoderBin();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        /// <param name="pathToExternalMencoderExe">The path to external mencoder exe.</param>
        public MencoderBase(string pathToExternalMencoderExe)
        { pathToMencoderExe = pathToExternalMencoderExe; }
        /// <summary>
        /// Finalizes an instance of the <see cref="MencoderAsync"/> class.
        /// </summary>
        ~MencoderBase()
        {
            File.Delete(pathToMencoderExe);
        }
        /// <summary>
        /// Gets the path to mencoder bin.
        /// </summary>
        /// <returns></returns>
        public string getPathToMencoderBin()
        {
            string path = Path.GetTempPath() + @"\mencoder" +  Guid.NewGuid()+ ".exe";
            if (!File.Exists(path))
                using (FileStream fsDst = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = MencoderSharp.Properties.Resources.mencoder;
                    fsDst.Write(bytes, 0, bytes.Length);
                }
            return path;
        }
    }



}