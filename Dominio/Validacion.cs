using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Validacion
    {
        public bool[] validacionesfrmAlta(string codigo, string nombre, string descripcion, string precio, int indexMarca, int indexCategoria)
        {
            bool[] validar = new bool[6];
            
            if (validarStr(codigo))
                validar[0] = true;
            
            if (validarStr(nombre))
                validar[1] = true;
            
            if (validarStr(descripcion))
                validar[2] = true;
            
            if (validarPrecio(precio))
                validar[3] = true;
            
            if (validarCb(indexMarca))
                validar[4] = true;
            
            if (validarCb(indexCategoria))
                validar[5] = true;
            return validar;
        }
        public bool validarStr(string objeto)
        {
            if (objeto.Length > 0)
                return true;
            return false;
        }

        public bool validarPrecio(string precio)
        {
            if (precio.Count(c => c == '.') > 1 || precio == "" || Decimal.Parse(precio) == 0)
                return false;
            return true;
        }
        public bool validarCb(int index)
        {
            if (index < 0)
                return false;
            return true;
        }
    }
}
