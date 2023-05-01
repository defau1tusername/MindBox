# Shape Processor (библиотека)
Функции:
1. Нахождение площади окружности (при наличии радиуса)
2. Нахождение площади треугольника (при наличии длины трех сторон)
3. Проверка на прямоугольность треугольника (при наличии длины трех сторон)

Пример пользовательского интерфейса: 
```csharp
new Circle(3).Area; 
new Triangle(1,2,3).Area;
```

<details> 
  <summary>Решаемая задача</summary>
  <img src="https://i.ibb.co/rQ4SHLK/image.png" width="350" title="hover text">
</details>

# Shape Processor2 (библиотека)
Функции:
1. Нахождение площади окружности (при наличии радиуса)
2. Нахождение площади треугольника (при наличии длины трех сторон)
3. Проверка на прямоугольность треугольника (при наличии длины трех сторон)

Преимущество по сравнению с Shape Processor - пользователям библиотеки классы фигур недоступны, реализация через FluentApi

Пример пользовательского интерфейса: 
```csharp
Figure.ForCircle().WithRadius(3).GetArea(); 
Figure.ForTriangle().WithSides(1, 2, 3).CheckIsRight();
```

<details> 
  <summary>Решаемая задача</summary>
  <img src="https://i.ibb.co/rQ4SHLK/image.png" width="350" title="hover text">
</details>
