using System.ComponentModel;
using System.ServiceProcess;

namespace DCX.DPE.Service.Cache
{
    /// <summary>
    /// The ProjectInstaller method
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller() : base()
        {
            
        }
    }
}
