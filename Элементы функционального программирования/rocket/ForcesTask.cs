using System;

namespace func_rocket;

public class ForcesTask
{
	/// <summary>
	/// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
	/// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
	/// </summary>
	public static RocketForce GetThrustForce(double forceValue)
	{
		return r =>
		{
			return new Vector(Math.Cos(r.Direction) * forceValue,
				Math.Sin(r.Direction) * forceValue); //Возвращаем вектор, который представляет собой проекцию силы тяги на ось X и Y
		};
	}

	/// <summary>
	/// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
	/// </summary>
	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize)
	{
		return r => gravity(spaceSize, r.Location);
	}

	/// <summary>
	/// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
	/// </summary>
	public static RocketForce Sum(params RocketForce[] forces) // На вход получаем массив объектов RocketForce, которые являются делегатами, возвращающими Vector
	{
		return r =>
		{
			var newX = 0; //Так как X, Y неизменяемы в классе Vector, создадим его в конце
			var newY = 0;
			foreach (var f in forces) 
			{
				newX += f(r).X; //Соотвественно, берем проекцию текущего вектора на соответствующую ось
				newY += f(r).Y; //И прибавляем ее к итоговому вектору
			}	
			return new Vector(newX, newY); //Возвращаем вектор - сумму всех сил 
		};
	}
}