using ErpBS100;
using Primavera.Extensibility.Engine;
using StdBE100;
using StdPlatBS100;
using System;
using static StdBE100.StdBETipos;

namespace Primavera.Paymentsds.Mpesa.Services
{
    sealed class PriEngine : DisposableBase
    {
        #region  Singleton pattern 

        // .NET guarantees thread safety for static initialization
        private static readonly PriEngine engineInstance = new PriEngine();

        /// <summary>
        /// Private constructor
        /// </summary>
        private PriEngine()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;

            System.Reflection.AssemblyName assemblyName;

            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(Environment.GetEnvironmentVariable("PERCURSOSGE100", EnvironmentVariableTarget.Machine), assemblyName.Name + ".dll");

            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }

        public static PriEngine CreatContext(int tipoPlataforma, string Company, string User, string Password)
        {
            StdBSConfApl objAplConf = new StdBSConfApl();
            StdPlatBS Plataforma = new StdPlatBS();
            ErpBS MotorLE = new ErpBS();

            EnumTipoPlataforma objTipoPlataforma;
            objTipoPlataforma = tipoPlataforma == 0 ? EnumTipoPlataforma.tpEmpresarial : EnumTipoPlataforma.tpProfissional;

            objAplConf.Instancia = "Default";
            objAplConf.AbvtApl = "ERP";
            objAplConf.PwdUtilizador = Password;
            objAplConf.Utilizador = User;
            objAplConf.LicVersaoMinima = "10.00";

            StdBETransaccao objStdTransac = new StdBETransaccao();

            try
            {
                Plataforma.AbrePlataformaEmpresa(Company, objStdTransac, objAplConf, objTipoPlataforma);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (Plataforma.Inicializada)
            {
                MotorLE.AbreEmpresaTrabalho(objTipoPlataforma, Company, User, Password, objStdTransac, "Default");

                // Use this service to trigger the API events.
                ExtensibilityService service = new ExtensibilityService();

                // Suppress all message box events from the API.
                //Plataforma.ExtensibilityLogger.AllowInteractivity = false;
                service.Initialize(MotorLE);

                // Check if service is operational
                if (service.IsOperational)
                {
                    // Inshore that all extensions are loaded.
                    service.LoadExtensions();
                }

                Platform = Plataforma;
                Engine = MotorLE;



                EngineStatus = true;
            }

            return engineInstance;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// The platform
        /// </summary>
        public static StdPlatBS Platform { get; set; }

        /// <summary>
        /// The engine that allows access to the modules.
        /// </summary>
        public static ErpBS Engine { get; set; }

        /// <summary>
        /// The engine status 0 - Fail | 1 - OK
        /// </summary>
        public static bool EngineStatus { get; private set; }

        #endregion

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called
            if (!this.Disposed)
            {

            }

            // Dispose on base class
            base.Dispose(disposing);
        }
        #endregion
    }
}
