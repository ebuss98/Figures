using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Figure
{
    /// <summary>
    /// Абстрактный класс фигур с абстрактными методами Perimeter(),Square(),Describe()
    /// </summary>
    abstract class Figure
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
    class Circle : Figure
    {
        private double rad;
        private const string name = "Circle";
        public Circle(double rad)
        {
            this.rad = rad;
        }
        public override double Perimeter()
        {
            return Math.Round(2 * Math.PI * rad,2);
        }
        public override double Square()
        {
            return Math.Round(Math.PI * Math.Pow(rad, 2),2);
        }
        public override void Describe()
        {
            Console.WriteLine("Фигура - " + name + " , радиус: " + rad + " , периметр: " + Perimeter() + " , площадь: " + Square());
        }
    }
    /// <summary>
    /// Конкретный класс прямоугольника
    /// Содержит переопределенные методы по вычислению площади, периметра и описания фигуры
    /// Содержит поля sideA и sideB - стороны прямоугольника
    /// </summary>
    class Rectangle : Figure
    {
        private double sideA;
        private double sideB;
        private const string name = "Rectangle";
        public Rectangle (double sideA, double sideB)
        {
            this.sideA = sideA;
            this.sideB = sideB;
        }
        public override double Perimeter()
        {
            return Math.Round(2 * (sideA + sideB),2);
        }
        public override double Square()
        {
            return Math.Round(sideA * sideB,2);
        }
        public override void Describe()
        {
            Console.WriteLine("Фигура - " + name + " , сторона A: "+ sideA + " , сторона B: " + sideB + " , периметр: " + Perimeter() + " , площадь: " + Square());
        }
    }
    /// <summary>
    /// Конкретный класс треугольника
    /// Содержит переопределенные методы по вычислению площади, периметра и описания фигуры
    /// /// Содержит поля sideА, sideB и sideC - стороны треугольника
    /// </summary>
    class Triangle : Figure
    {
        private double sideA;
        private double sideB;
        private double sideC;
        private double semiPer;
        private const string name = "Triangle";
        public Triangle (double sideA, double sideB, double sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }
        public override double Perimeter()
        {
            return Math.Round(sideA+sideB+sideC,2);
        }
        public override double Square()
        {
            semiPer = this.Perimeter() / 2;
            return Math.Round(Math.Sqrt(semiPer * (semiPer-sideA) * (semiPer - sideB) * (semiPer - sideC)),2);
        }
        public override void Describe()
        {
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
        static void Main(string[] args)
        {
            FileStream file = new FileStream("/Users/evgenijbuss/Projects/Figure/Figure/input.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            int count = Convert.ToInt32(reader.ReadLine());
            Console.WriteLine(count);
            string str;
            int counter = 0;
            while (counter < count)
            {
                str = reader.ReadLine();
                string[] buf = str.Split(' ');
                    if (buf.Length == 1)
                    {
                        Circle circle = new Circle(Convert.ToDouble(buf[0]));
                        circle.Describe();
                    }
                    if (buf.Length == 2)
                    {
                        Rectangle rectangle = new Rectangle(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]));
                        rectangle.Describe();
                    }
                    if (buf.Length == 3)
                    {
                        Triangle triangle = new Triangle(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]), Convert.ToDouble(buf[2]));
                        triangle.Describe();
                    }
                counter++;
            }
            reader.Close();
        }
    }
}
