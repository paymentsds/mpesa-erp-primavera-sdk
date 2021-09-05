using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;

namespace Primavera.Paymentsds.Mpesa.UI
{
    public partial class FDU_PagMpesa : CustomForm
    {
        public FDU_PagMpesa()
        {
            InitializeComponent();
        }

        public void Initialize(string tipoDoc, string serie, int numdoc)
        {
            try
            {
                cbTipoDoc.SelectedValue = tipoDoc;
                cbSerie.SelectedValue = serie;
                txtNumdoc.Value = numdoc;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
