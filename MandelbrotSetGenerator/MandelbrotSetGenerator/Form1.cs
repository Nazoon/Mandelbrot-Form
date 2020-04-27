/* Project Name: MandelbrotSetGenerator
 * File Name: MainProgram.cs
 * Author: Ilan Segal
 * Date Created: August 29 2016
 * Date Modified: December 15 2018
 * Description: Draws the Mandelbrot Set in all its glory. Allows for change of domain and range drawn both manually and with a double-click anywhere on the window.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MandelbrotSetGenerator
{
    public partial class MainProgram : Form
    {
        //Maximum distance from origin before point blows up
        const int MAX_DIST = 2;

        //Maximum bail out value
        int bailOut = 150;
        
        //Used to store the bail-out value of any point in the given domain and range
        int pointValue;
                

        //Domain and range to be rendered
        double renderTop = -2;
        double renderLeft = -2;
        double renderSideLength = 4;
        int domainSize = 0;

        //Brush value
        int brush = 0;

        public MainProgram()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Showing values
            topTxt.Text = renderTop.ToString();
            leftTxt.Text = renderLeft.ToString();
            sizeTxt.Text = renderSideLength.ToString();
            bailOutTxt.Text = bailOut.ToString();
        }

        /// <summary>
        /// Recursive check if a complex number is within the Mandelbrot set
        /// </summary>
        /// <param name="z"></param>
        /// <param name="c"></param>
        /// <param name="recursions"></param>
        /// <param name="maxRecursions"></param>
        /// <returns>TRUE if "c" is within Mandelbrot Set, FALSE otherwise</returns>
        private bool RecursiveCheck(ComplexNum z, ComplexNum c, int recursions, int maxRecursions)
        {
            if (Math.Sqrt(z.GetReal() * z.GetReal() + z.GetComplex() * z.GetComplex()) > MAX_DIST)
            {
                //Number blew up
                return false;
            }
            else if (recursions > maxRecursions)
            {
                //Number probably does not blow up
                return true;
            }
            else
            {
                //Run it again
                z.Squared();
                z.Add(c);
                return RecursiveCheck(z, c, recursions + 1, maxRecursions);
            }
        }
        /// <summary>
        /// Iterative check if a complex number is within the Mandelbrot set
        /// </summary>
        /// <param name="z"></param>
        /// <param name="c"></param>
        /// <param name="maxIterations"></param>
        /// <returns>TRUE if "c" is within Mandelbrot Set, FALSE otherwise</returns>
        private int IterativeCheck(ComplexNum z, ComplexNum c, int maxIterations)
        {
            int iterations = 0;
            while (true)
            {
                if(z.Magnitude() > MAX_DIST)
                {
                    //Number blew up
                    return iterations;
                }
                else if(iterations >= maxIterations)
                {
                    //Number probably does not blow up
                    return maxIterations;
                }

                //Run it through the function again
                z.Multiply(z);
                z.Add(c);
                iterations++;
            }
        }

        /// <summary>
        /// Generates set upon clicking button
        /// </summary>
        private void generateBtn_Click(object sender, EventArgs e)
        {
            //Objects used to draw the set
            Graphics objGraphics = this.CreateGraphics();
            Brush objBrush = new SolidBrush(System.Drawing.Color.Blue);

            //Setting size of array based on size of window
            domainSize = Math.Min(this.Size.Height, this.Size.Width);

            //Parsing input for values, if it is valid
            double.TryParse(topTxt.Text, out renderTop);
            double.TryParse(leftTxt.Text, out renderLeft);
            double.TryParse(sizeTxt.Text, out renderSideLength);
            int.TryParse(bailOutTxt.Text, out bailOut);

            //Showing values
            topTxt.Text = renderTop.ToString();
            leftTxt.Text = renderLeft.ToString();
            sizeTxt.Text = renderSideLength.ToString();
            bailOutTxt.Text = bailOut.ToString();

            //Complex numbers
            ComplexNum z = new ComplexNum(0, 0);
            ComplexNum c = new ComplexNum(0, 0);

            //Going through each point in the pointSet
            for (int b = 0; b < domainSize; b++)
            {
                for (int a = 0; a < domainSize; a++)
                {
                    //Setting complex value
                    c.SetReal(renderSideLength * ((double)a / (double)domainSize) + renderLeft);
                    c.SetComplex(renderSideLength * ((double)b / (double)domainSize) + renderTop);
                    z.SetReal(0);
                    z.SetComplex(0);

                    //Setting blow-up value for point
                    pointValue = IterativeCheck(z, c, bailOut);
                    if (pointValue == bailOut)
                    {
                        objBrush = new SolidBrush(System.Drawing.Color.Black);
                    }
                    else
                    {

                        //Setting brush shade
                        float s = (float)pointValue / (float)bailOut;
                        int t = (int)(Math.Log10(10 * s + 1) * 255);
                        if (t > 255)
                        {
                            t = 255;
                        }
                        int red = t;
                        int green = t;
                        int blue = 255 - t;
                        objBrush = new SolidBrush(System.Drawing.Color.FromArgb(red, green, blue));
                    }

                    //Drawing point
                    objGraphics.FillRectangle(objBrush, a, b, 1, 1);

                }

                //Ensures memory does not run out
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Zooms in on clicked point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Getting mouse coordinates, relative to rendered area
            double screenRatio = (double)renderSideLength / (double)domainSize;
            double xRelative = renderLeft + (screenRatio * e.X);
            double yRelative = renderTop + (screenRatio * e.Y);

            //Zooming in
            renderSideLength /= 16;
            sizeTxt.Text = renderSideLength + "";
            leftTxt.Text = (xRelative - renderSideLength / 2) + "";
            topTxt.Text = (yRelative - renderSideLength / 2) + "";
            bailOutTxt.Text = (int)(bailOut + 300) + "";

            //Generating new render
            generateBtn.PerformClick();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
