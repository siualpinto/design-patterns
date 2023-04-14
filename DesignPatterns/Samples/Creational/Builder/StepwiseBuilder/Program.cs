﻿namespace StepwiseBuilder
{
    public enum CarType
    {
        Sedan,
        Crossover
    };
    public class Car
    {
        public CarType Type;
        public int WheelSize;

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(WheelSize)}: {WheelSize}";
        }
    }

    public interface ISpecifyCarType
    {
        public ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        public IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }

        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private readonly Car car = new Car();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
                }
                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return car;
            }
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            Car car = CarBuilder.Create()
              .OfType(CarType.Crossover)
              .WithWheels(18)
              .Build();
            Console.WriteLine(car);
        }
    }
}
