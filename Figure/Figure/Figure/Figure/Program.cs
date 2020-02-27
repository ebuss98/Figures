using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
namespace Figure
{
    /// <summary>
    /// Абстрактный класс фигур с абстрактными методами Perimeter(),Square(),Describe()
    /// </summary>
    [XmlInclude(typeof(Triangle))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
    [Serializable]
    public abstract class Figure
    { 
        public abstract double Perimeter();
        public abstract double Square();
        public abstract void Describe();
    }
    /// <summary>
    /// Класс-наследник, конкретный класс круга
    /// Содержит переопределенные методы по вычислению площади, периметра и описания фигуры
    /// содержит поле rad - радиус
    /// </summary>
    [Serializable]
    public class Circle : Figure
    {
        public double rad { get; set; }
        private const string name = "Circle";
        public Circle() { }
        public Circle(double rad)
        {
            Trace.WriteLine("Конструктор Circle был вызван");
            this.rad = rad;
        }
        public override double Perimeter()
        {
            Trace.WriteLine("Метод Circle.Perimeter() был вызван");
            return Math.Round(2 * Math.PI * rad,2);
        }
        public override double Square()
        {
            Trace.WriteLine("Метод Circle.Square() был вызван");
            return Math.Round(Math.PI * Math.Pow(rad, 2),2);
        }
        public override void Describe()
        {
            Console.WriteLine("Фигура - " + name + " , радиус: " + rad + " , периметр: " + Perimeter() + " , площадь: " + Square());
            Trace.WriteLine("Метод Circle.Describe() был вызван");
        }
        
    }
    /// <summary>
    /// Конкретный класс прямоугольника
    /// Содержит переопределенные методы по вычислению площади, периметра и описания фигуры
    /// Содержит поля sideA и sideB - стороны прямоугольника
    /// </summary>
    [Serializable]
    public class Rectangle : Figure
    {
        public double sideA { get; set; }
        public double sideB { get; set; }
        public const string name = "Rectangle";
        public Rectangle() { }
        public Rectangle (double sideA, double sideB)
        {
            Trace.WriteLine("Конструктор Rectangle был вызван");
            this.sideA = sideA;
            this.sideB = sideB;
        }
        public override double Perimeter()
        {
            Trace.WriteLine("Метод Rectangle.Perimeter() был вызван");
            return Math.Round(2 * (sideA + sideB),2);
        }
        public override double Square()
        {
            Trace.WriteLine("Метод Rectangle.Square() был вызван");
            return Math.Round(sideA * sideB,2);
        }
        public override void Describe()
        {
            Trace.WriteLine("Метод Rectangle.Describe() был вызван");
            Console.WriteLine("Фигура - " + name + " , сторона A: "+ sideA + " , сторона B: " + sideB + " , периметр: " + Perimeter() + " , площадь: " + Square());
        }
    }
    /// <summary>
    /// Конкретный класс треугольника
    /// Содержит переопределенные методы по вычислению площади, периметра и описания фигуры
    /// /// Содержит поля sideА, sideB и sideC - стороны треугольника
    /// </summary>
    [Serializable]
    public class Triangle : Figure
    {
        public double sideA { get; set; }
        public double sideB { get; set; }
        public double sideC { get; set; }
        public double semiPer;
        public const string name = "Triangle";
        public Triangle() { }
        public Triangle (double sideA, double sideB, double sideC)
        {
            Trace.WriteLine("Конструктор Triangle был вызван");
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }
        public override double Perimeter()
        {
            Trace.WriteLine("Метод Triangle.Perimeter() был вызван");
            return Math.Round(sideA+sideB+sideC,2);
        }
        public override double Square()
        {
            Trace.WriteLine("Метод Triangle.Square() был вызван");
            semiPer = this.Perimeter() / 2;
            return Math.Round(Math.Sqrt(semiPer * (semiPer-sideA) * (semiPer - sideB) * (semiPer - sideC)),2);
        }
        public override void Describe()
        {
            Trace.WriteLine("Метод Triangle.Describe() был вызван");
            Console.WriteLine("Фигура -  " + name + " , сторона A: " + sideA + " , сторона B: " + sideB + " , сторона C: " + sideC +  " , периметр: " + Perimeter() + " , площадь: " + Square());
        }
    }
    class Program
    {
        /// <summary>
        /// В методе main содержится работа с файлом input.txt, из которого извлекаются входные данные
        /// input.txt содержит число строк n и далее n строк с числами, идущими через пробел
        /// Исходя из этих данных, в зависимости от количества поступающих аргументов, вызывается конструктор определенной фигуры
        /// Информация о фигурах выводится в консоль
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Trace.WriteLine("Debug Information-Product Starting ");
            Trace.Indent();
            try
            {
                FileStream file = new FileStream("/Users/evgenijbuss/Projects/Figure/Figure/input.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                int count;
                if (!Int32.TryParse(reader.ReadLine(), out count))
                {
                    throw new IOException();
                }
                Console.WriteLine(count);
                Figure[] figures = new Figure[count];
                string str;
                int counter = 0;
                while (counter < count)
                {
                    str = reader.ReadLine();
                    string[] buf = str.Split(' ');
                    if (buf.Length == 1)
                    {
                        Circle circle = new Circle(Convert.ToDouble(buf[0]));
                        figures[counter] = circle;
                        circle.Describe();
                    }
                    else if (buf.Length == 2)
                    {
                        Rectangle rectangle = new Rectangle(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]));
                        figures[counter] = rectangle;
                        rectangle.Describe();
                    }
                    else if (buf.Length == 3)
                    {
                        Triangle triangle = new Triangle(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]), Convert.ToDouble(buf[2]));
                        figures[counter] = triangle;
                        triangle.Describe();
                    }
                    else
                    {
                        throw new OverflowException();
                    }
                    counter++;
                }
                XmlSerializer formatter = new XmlSerializer(typeof(Figure[]));
                using (FileStream fs = new FileStream("/Users/evgenijbuss/Projects/Figure/Figure/figures.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, figures);
                }
                reader.Close();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid arguments count");

            }
            catch (IOException)
            {
                Console.WriteLine("Invalid input");
            }
            catch
            {
                Console.WriteLine("Input error");

            }

        }
    }
}
