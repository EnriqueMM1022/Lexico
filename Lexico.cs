using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lexico
{
    public class Lexico : Token, IDisposable
    {
        private StreamReader archivo;
        private StreamWriter log;
        public Lexico()
        {
            archivo = new StreamReader("prueba.cpp");
            log = new StreamWriter("prueba.log");
        }
        public Lexico(string nombre)
        {
            archivo = new StreamReader(nombre);
            log = new StreamWriter("prueba.log");
        }
        public void Dispose()
        {
            archivo.Close();
            log.Close();
        }
        public void nextToken()
        {
            char c;
            string buffer = "";
            while (char.IsWhiteSpace(c = (char)archivo.Read()))
            {
            }
            buffer += c;
            if (char.IsLetter(c))
            {
                setClasificacion(Tipos.Identificador);
                while (char.IsLetterOrDigit(c = (char)archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (char.IsDigit(c))
            {
                setClasificacion(Tipos.Numero);
                while (char.IsDigit(c = (char)archivo.Peek()))
                {
                    archivo.Read();
                    buffer += c;
                }
            }
                
            else if (c == '=')
            {
                setClasificacion(Tipos.Asignacion);
                if ((c =(char)archivo.Peek())== '=')
                {
                setClasificacion(Tipos.OperadorRelacional);
                    buffer += c;
                    archivo.Read();

                }
            }
             else if (c == '>')
            {
                setClasificacion(Tipos.Asignacion);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    setClasificacion(Tipos.OperadorRelacional);
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == '<')
            {
                setClasificacion(Tipos.Asignacion);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    setClasificacion(Tipos.OperadorRelacional);
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == ';')
            {
                setClasificacion(Tipos.FinSentencia);
            }
            else if (c == ':')
            {
                setClasificacion(Tipos.Caracter);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    setClasificacion(Tipos.Inicializacion);
                    archivo.Read();
                    buffer += c;
                }
            }
            else if (c == "Hola Mundo")
            {
                setClasificacion(Tipos.Caracter);
                if ((c =(char)archivo.Peek())== '\"')
                {
                setClasificacion(Tipos.OperadorCadena);
                    buffer += c;
                    archivo.Read();

                }
            }
             else if (c == '\"')
            {
               setClasificacion(Tipos.OperadorCadena);
            }
            else if (c == '!')
            {
                setClasificacion(Tipos.OperadorLogico);
            }
            else if (c == '{')
            {
                setClasificacion(Tipos.Inicio);
            }
            else if (c == '}')
            {
                setClasificacion(Tipos.Fin);
            }
              else if (c == '<' || c == '>')
            {
                setClasificacion(Tipos.OperadorRelacional);
            }
            else if (c == '&')
            {
                setClasificacion(Tipos.Caracter);
                if ((c = (char)archivo.Peek()) == '&')
                {
                    setClasificacion(Tipos.OperadorLogico);
                    archivo.Read();
                    buffer += c;
                }
            }
            else
            {
                setClasificacion(Tipos.Caracter);
            }
            setContenido(buffer);
            log.WriteLine(getContenido() + " = " + getClasificacion());
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}