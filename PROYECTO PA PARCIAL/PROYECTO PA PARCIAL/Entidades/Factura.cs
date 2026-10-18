using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PA_PARCIAL.Entidades
{
    public class Factura
    {
        private string numFactura;
        private string ruc;
        private string cedulaCliente;
        private string fechaEmision;
        private double totalFactura;
        private string formaPago; // "Efectivo" o "Transferencia"
        public Factura()
        {
        }
        public Factura(string numFac, string rucVal, string cedCli, string fecEmi, double totFac, string forPag)
        {
            this.NumFactura = numFac;
            this.Ruc = rucVal;
            this.CedulaCliente = cedCli;
            this.FechaEmision = fecEmi;
            this.TotalFactura = totFac;
            this.FormaPago = forPag;
        }

        // Propiedades públicas (PascalCase)
        public string NumFactura { get => numFactura; set => numFactura = value; }
        public string Ruc { get => ruc; set => ruc = value; }
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }
        public string FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public double TotalFactura { get => totalFactura; set => totalFactura = value; }
        public string FormaPago { get => formaPago; set => formaPago = value; }
        public string GetFactura()
        {
            string datos = $"{this.NumFactura};{this.Ruc};{this.CedulaCliente};{this.FechaEmision};{this.TotalFactura};{this.FormaPago};";
            return datos;
        }
    }
}