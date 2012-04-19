using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SCalculator
{
    public partial class Form1 : Form
    {
        Dictionary<int, string> delimitadores;
        string olddel;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo utilizando delimitador
        /// </summary>
        /// <param name="suma"></param>
        /// <returns>Suma de los numeros</returns>
        private int DelimeterMethod(string numeros)
        {
            int suma = 0;
            string delimeter = getDelimeter(numeros);
            if (numeros.Contains('\n'))
            {
                if (delimitadores.Count > 0)
                {
                    foreach (var pair in delimitadores)
                    {
                        delimeter = pair.Value;
                        if (delimeter.Contains('*') || delimeter.Contains('+') || delimeter.Contains('?') || delimeter.Contains('.') || delimeter.Contains('$'))
                            delimeter = AddSpecialCharacters(pair.Value);
                        suma = DoSum(suma, delimeter, numeros);
                    }
                }
                else
                {
                    if (delimeter.Contains('*') || delimeter.Contains('+') || delimeter.Contains('?') || delimeter.Contains('.') || delimeter.Contains('$'))
                        delimeter = AddSpecialCharacters(delimeter);
                    suma = DoSum(suma, delimeter, numeros);
                }
            }
            return suma;
        }

        /// <summary>
        /// Metodo encargado de realizar la suma
        /// </summary>
        /// <param name="suma"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public int DoSum(int suma, string delimeter, string numerosTextBox)
        {                    
            string[] numeros = Regex.Split(numerosTextBox.Substring(numerosTextBox.IndexOf('\n')).Replace('\n', ' ').Trim(), delimeter);
            for (int i = 0; i < numeros.Length; i++)
            {
                if (numeros[i].Length > 1)
                {
                    int parseo;
                    if (int.TryParse(numeros[i], out parseo))
                        suma += parseo;

                    if (numeros[i].Contains(',') || numeros[i].Contains('\n'))
                    {
                        string[] numcom;
                        if (numeros[i].Contains(','))
                            numcom = numeros[i].Split(',');
                        else
                            numcom = numeros[i].Split('\n');

                        foreach (string item in numcom)
                        {
                            suma += WithoutDelimeter(item);
                        }
                    }
                    else                    
                    {
                        if (!string.IsNullOrEmpty(olddel))
                        {
                            string[] numcom = Regex.Split(numeros[i], olddel);
                            suma += int.Parse(numcom[numcom.Length - 1]);
                        }                       
                    }
                }
                else
                {
                    int parse;
                    if (int.TryParse(numeros[i].ToString(), out parse))
                        if (parse <= 1000)
                            suma += parse;
                }
            }
            olddel = delimeter;
            return suma;
        }

        public int Add(string numeros)
        {
            olddel = string.Empty;
            int suma = 0;
            if (string.IsNullOrEmpty(getAllNegatives(numeros)))
            {
                if (findDelimiters(numeros) == 2)                
                    suma = DelimeterMethod(numeros);                
                else                
                    suma = WithoutDelimeter(numeros);                
            }
            else
            {
                MessageBox.Show("Numeros negativos no permitidos " + getAllNegatives(numeros));
                textBox1.Text = string.Empty;
            }
            return suma;
        }

        private string getAllNegatives(string numeros)
        {
            string result = string.Empty;
            bool entro = false;
            string negnum = string.Empty;
            for (int i = 0; i < numeros.Length; i++)
            {
                if (numeros[i].Equals('-') && !entro)
                {
                    negnum += numeros[i];
                    entro = true;
                }
                else
                {
                    if (char.IsNumber(numeros[i]) && entro)                    
                        negnum += numeros[i];                    
                    else
                    {
                        if (numeros[i].Equals('-'))
                        {
                            result += string.IsNullOrEmpty(negnum) ? "" : string.Format("{0},", negnum);
                            negnum = numeros[i].ToString();
                        }
                        else
                        {
                            result += string.IsNullOrEmpty(negnum) ? "" : string.Format("{0},", negnum);
                            entro = false;
                            negnum = string.Empty;
                        }
                    }
                }
            }
            result += string.IsNullOrEmpty(negnum) ? "" : string.Format("{0},", negnum);
            return result;
        }

        /// <summary>
        /// Metodo sin utilizar Delimitador \n y ,
        /// </summary>
        /// <param name="suma"></param>
        /// <returns></returns>
        public int WithoutDelimeter(string numerosText)
        {
            int suma = 0;
            string invalid = string.Empty;
            string result = string.Empty;
            for (int i = 0; i < numerosText.Length; i++)
            {
                if (Char.IsNumber(numerosText[i]))                
                    result += numerosText[i].ToString();   
                else
                {
                    int outparse;
                    if (int.TryParse(result, out outparse))
                    {
                        if(outparse<=1000)
                            suma += outparse;
                        result = string.Empty;
                    }
                }
            }
            if(!string.IsNullOrEmpty(result))
                if(int.Parse(result)<=1000)
                    suma +=int.Parse(result);
            return suma;
        }

        /// <summary>
        /// Metodo que permite conocer si el caracter es especial y agregarle \\para que pueda ser utilizado por el Regex
        /// </summary>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        private string AddSpecialCharacters(string delimeter)
        {
            string result = string.Empty;
            foreach (char item in delimeter.ToCharArray())
            {
                result += string.Format("{0}{1}", "\\", item);
            }
            return result;

        }

        /// <summary>
        /// Metodo para habilitar Delimitadores
        /// </summary>
        private int findDelimiters(string numeros)
        {
            int resultado = 0;
            foreach (char item in numeros)
            {
                if (item == '/')
                    resultado += 1;
            }
            return resultado;
        }

        private string Checkstart(string numeros)
        {
            string res = string.Empty;
            string[] regex = Regex.Split(numeros, "//");
            if (regex[1].Contains('\n'))
            {
                res = regex[1].Remove(regex[1].IndexOf('\n'));
                delimitadores = new Dictionary<int, string>();
                if (res.Contains('[') && res.Contains(']'))
                {
                    bool band = false;
                    int i = 0;
                    string delimiter = string.Empty;
                    foreach (char item in res)
                    {
                        if (item.Equals(']'))
                        {
                            delimitadores.Add(i, delimiter);
                            band = false;
                            i++;
                        }
                        if (band)
                            delimiter += item;
                        if (item.Equals('['))
                        {
                            band = true;
                            delimiter = string.Empty;
                        }
                    }
                }
            }
            res = res.Replace('\n', ' ').Replace('\r', ' ');
            return res.Trim();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               label1.Text = Add(textBox1.Text).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado " + ex.Message);
            }

        }

        /// <summary>
        /// Metodo que obtiene los delimitadores
        /// </summary>
        /// <returns>Returna un String con el delimitador</returns>
        private string getDelimeter(string numeros)
        {
            string res = string.Empty;
            string[] regex = Regex.Split(numeros, "//");            
            if (regex[1].Contains('\n'))
            {
                res = regex[1].Remove(regex[1].IndexOf('\n'));
                delimitadores = new Dictionary<int, string>();
                if (res.Contains('[') && res.Contains(']'))
                {
                    bool band = false;
                    int i = 0;
                    string delimiter = string.Empty;
                    foreach (char item in res)
                    {
                        if (item.Equals(']'))
                        {
                            delimitadores.Add(i, delimiter);
                            band = false;
                            i++;
                        }
                        if (band)
                            delimiter += item;
                        if (item.Equals('['))
                        {
                            band = true;
                            delimiter = string.Empty;
                        }
                    }
                }
            }
            res = res.Replace('\n', ' ').Replace('\r', ' ');
            return res.Trim();
        }

    }
}
