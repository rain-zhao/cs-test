using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestEvent
{
    /*
     * 自定义event 事件
     */
    public class ShapeEventArgs : EventArgs
    {
        public double NewArea
        {
            get;
        }

        public ShapeEventArgs(double area)
        {
            this.NewArea = area;
        }
    }

    /*
     * shape抽象类
     */
    public abstract class Shape
    {

        protected double _area;
        public double Area
        {
            get => _area;
            set => _area = value;
        }

        public event EventHandler<ShapeEventArgs> ShapeChanged;

        public abstract void Draw();

        protected virtual void OnShapeChange(ShapeEventArgs e)
        {
            ShapeChanged?.Invoke(this, e);
        }

    }

    public class Circle : Shape
    {

        private double _radius;

        public Circle(double radius)
        {
            _radius = radius;
            _area = 3.14 * _radius * _radius;
        }

        public override void Draw()
        {
            Console.WriteLine("Write a Circle!");
        }

        protected override void OnShapeChange(ShapeEventArgs e)
        {
            base.OnShapeChange(e);
        }

        public void Update(double d)
        {
            _radius = d;
            _area = 3.14 * _radius * _radius;
            OnShapeChange(new ShapeEventArgs(_area));
        }

    }

    public class Rectangle : Shape
    {
        private double _length;
        private double _width;

        public Rectangle(double length, double width)
        {
            _length = length;
            _width = width;
            _area = _length * _width;
        }


        public override void Draw()
        {
            Console.WriteLine("Write a Rectangle!");
        }

        protected override void OnShapeChange(ShapeEventArgs e)
        {
            base.OnShapeChange(e);
        }

        public void Update(double length,double width)
        {
            _length = length;
            _width = width;
            _area = _length * _width;
            OnShapeChange(new ShapeEventArgs(_area));
        }
    }

    public class ShapeContainer
    {
        private readonly  IList<Shape> _list;

        public ShapeContainer()
        {
            _list = new List<Shape>();
        }

        public void AddShape(Shape shape)
        {
            _list.Add(shape);
            shape.ShapeChanged += handleShapeChanged;
         
        }

        private void handleShapeChanged(object sender, ShapeEventArgs e)
        {
            if (sender is Shape shape)
            {
                Console.WriteLine($"Received event. Shape area is now {e.NewArea}");
                shape.Draw();
            }
            
        }

    }

    class Test
    {
        static void Main(string[] args)
        {
            ShapeContainer container = new ShapeContainer();
            Circle circle = new Circle(54);
            Rectangle rectangle = new Rectangle(12, 9);
            container.AddShape(circle);
            container.AddShape(rectangle);

            // Cause some events to be raised.
            circle.Update(57);
            rectangle.Update(7, 7);

        }









    }
}
