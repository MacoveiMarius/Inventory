using Inventory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public static class SVC
    {
        private static ICalculatoare _calculatoareServices = null;
        private static IGestiuni _gestiuniServices = null;
        private static ILaboratoare _laboratoareServices = null;
        private static ISurse _surseServices = null;
        private static ITipuri _tipuriServices = null;
        private static IInventare _inventareServices = null;

        public static ICalculatoare Calculatoare
        {
            get
            {
                if (_calculatoareServices == null)
                {
                    _calculatoareServices = new CalculatoareService(new StringBuilder(Config.InventaryConnection));
                }
                return _calculatoareServices;
            }
        }

        public static IGestiuni Gestiuni
        {
            get
            {
                if (_gestiuniServices == null)
                {
                    _gestiuniServices = new GestiuniService(new StringBuilder(Config.InventaryConnection));
                }
                return _gestiuniServices;
            }
        }

        public static ILaboratoare Laboratoare
        {
            get
            {
                if (_laboratoareServices == null)
                {
                    _laboratoareServices = new LaboratoareService(new StringBuilder(Config.InventaryConnection));
                }
                return _laboratoareServices;
            }
        }

        public static ISurse Surse
        {
            get
            {
                if (_surseServices == null)
                {
                    _surseServices = new SurseService(new StringBuilder(Config.InventaryConnection));
                }
                return _surseServices;
            }
        }

        public static ITipuri Tipuri
        {
            get
            {
                if (_tipuriServices == null)
                {
                    _tipuriServices = new TipuriService(new StringBuilder(Config.InventaryConnection));
                }
                return _tipuriServices;
            }
        }

        public static IInventare Inventare
        {
            get
            {
                if (_inventareServices == null)
                {
                    _inventareServices = new InventareService(new StringBuilder(Config.InventaryConnection));
                }
                return _inventareServices;
            }
        }
    }
}
