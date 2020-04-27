/* Project Name: MandelbrotSetGenerator
 * File Name: ComplexNum.cs
 * Author: Ilan Segal
 * Date Created: October 26 2016
 * Date Modified: December 15 2018
 * Description: Class which represents and behaves as a complex number
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSetGenerator
{
    class ComplexNum
    {
        //Number components
        private double real;
        private double complex;

        /// <summary>
        /// Constructor for the ComplexNum class, both components set to 0
        /// </summary>
        public ComplexNum()
        {
            real = 0;
            complex = 0;
        }

        /// <summary>
        /// Constructor for the ComplexNum class
        /// </summary>
        /// <param name="real">Real component</param>
        /// <param name="complex">Complex component</param>
        public ComplexNum(double real, double complex)
        {
            this.real = real;
            this.complex = complex;
        }

        /// <summary>
        /// Gets the real component
        /// </summary>
        /// <returns>Dobule value of real component</returns>
        public double GetReal()
        {
            return real;
        }

        /// <summary>
        /// Gets the complex component
        /// </summary>
        /// <returns>Dobule value of complex component</returns>
        public double GetComplex()
        {
            return complex;
        }

        /// <summary>
        /// Sets a new value for the real component
        /// </summary>
        /// <param name="newReal">The new real value</param>
        public void SetReal(double newReal)
        {
            real = newReal;
        }

        /// <summary>
        /// Sets a new value for the complex component
        /// </summary>
        /// <param name="newComplex">The new complex value</param>
        public void SetComplex(double newComplex)
        {
            complex = newComplex;
        }

        /// <summary>
        /// Adds a ComplexNum value to this one
        /// </summary>
        /// <param name="n">The ComplexNum to be added</param>
        public void Add(ComplexNum n)
        {
            real += n.GetReal();
            complex += n.GetComplex();
        }

        /// <summary>
        /// Subtracts a ComplexNum value from this one
        /// </summary>
        /// <param name="n">The ComplexNum to be subtracted</param>
        public void Subtract(ComplexNum n)
        {
            real -= n.GetReal();
            complex -= n.GetComplex();
        }

        /// <summary>
        /// Finds the product of a ComplexNum and this one
        /// </summary>
        /// <param name="n">The ComplexNum to be multiplied</param>
        public void Multiply(ComplexNum n)
        {
            double a = real;
            double b = complex;
            double c = n.GetReal();
            double d = n.GetComplex();

            //Using FOIL to determine new values
            //(a + bi)(c + di)
            real = (a * c) - (b * d);
            complex = (a * d) + (b * c);
        }

        /// <summary>
        /// Divides this ComplexNum by a given one
        /// </summary>
        /// <param name="n">The ComplexNum to divide with</param>
        public void Divide(ComplexNum n)
        {
            double a = real;
            double b = complex;
            double c = n.GetReal();
            double d = n.GetComplex();

            //Trust me on this one
            real = (a * c + b * d) / (c * c + d * d);
            complex = (b - a * d) / (c * c + d * d);
        }

        /// <summary>
        /// Squares this ComplexNum
        /// </summary>
        public void Squared()
        {
            double a = real;
            double b = complex;

            //Squared binomial applied to a complex number
            real = (a * a) - (b * b);
            complex = 2 * a * b;
        }

        /// <summary>
        /// Returns the magnitude of this ComplexNum
        /// </summary>
        /// <returns>Double representing the distance between this ComplexNum and the origin on the complex plane</returns>
        public double Magnitude()
        {
            return Math.Sqrt(real * real + complex * complex);
        }
    }
}
