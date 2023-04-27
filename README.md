# Solver (библиотека)
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

# Solver2 (библиотека)
Функции:
1. Нахождение площади окружности (при наличии радиуса)
2. Нахождение площади треугольника (при наличии длины трех сторон)
3. Проверка на прямоугольность треугольника (при наличии длины трех сторон)

Преимущество по сравнению с Solve - пользователям библиотеки классы фигур недоступны, использование FluentApi

Пример пользовательского интерфейса: 
```csharp
Figure.ForCircle().WithRadius(3).GetArea(); 
Figure.ForTriangle().WithSides(1, 2, 3).CheckIsRight();
```

<details> 
  <summary>Решаемая задача</summary>
  <img src="https://i.ibb.co/rQ4SHLK/image.png" width="350" title="hover text">
</details>

# Avito Analytics (веб-приложение)
Функции:
<details> 
  <summary>1. Авторизация;</summary>
  <img src="https://i.ibb.co/RPDP5F2/image.png" width="350" title="hover text">
</details>
<details> 
  <summary>2. Автоматический мониторинг позиций объявлений на Авито;</summary>
  <img src="https://i.ibb.co/5hzB0Yj/image.png" width="350" title="hover text">
</details>
<details> 
  <summary>3. Просмотр статистики позиций по определенному объявлению;</summary>
  <img src="https://i.ibb.co/GcjGhD8/image.png" width="350" title="hover text">
</details>
<details> 
  <summary>4. Панель управления.</summary>
  <img src="https://i.ibb.co/S5W2bym/image.png" width="350" title="hover text">
  <img src="https://i.ibb.co/nc4scZr/image.png" width="350" title="hover text">
</details>
