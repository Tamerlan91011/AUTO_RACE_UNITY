# **Тема**: Модель автогонок с использованием Unity и OpenStreetMap. 
## **Преподаватель**: Парыгин Данила Сергеевич
## **Кафедра**: САПР и ПК

## **Нефункциональные требование**: [Unity](https://unity.com/), [OpenStreetMap](https://www.openstreetmap.org/#map=3/59.90/89.71), [C#](https://docs.microsoft.com/ru-ru/dotnet/csharp/)
## **Функциональные требование**: 

* Трасса на основе дорог реального города
* Модель искуственного интелекта гоночных соревновательных ботов
* Трехмерная или двумерная модель с реализованными ботами
* Боты должны переноситься на любую трассу за минимальное количество действий 

## **Команда** "Автоботы" (группа ИВТ-262) 
* [Глазунов](https://github.com/Tamerlan91011)
* [Гончаров](https://github.com/bigwitch3r)
* [Макагонов](https://github.com/theDeMolition)

# **Обязанности членов команды**
## **Гончаров**:
1.	Определить область города Волгограда, которая будет использована в качестве прототипа для будущей трассы.
2.	Получить файл формата OSM для выбранной области.
3.	Проанализировать содержание OSM-файла.
4.	Составить на его основе алгоритм для выделения ключевых объектов: дорог и зданий.
5.	Применить алгоритм для выделения ключевых объектов.
6.	Представить дороги в виде линий и здания в виде контуров.
7.	Осуществить трёхмерную визуализацию дорог и зданий на общей карте.

## **Макагонов**:
1. Определение маршрута для самостоятельного движения ботов;
2. Определение границы трассы проекта;
3. Реализация маршрута за счет создания и внедрения дорожного графа;
4. Реализация самостоятельной езды бота по проложенному маршруту;
5. Реализация границы трассы, которую боты не должны пересекать;
6. Реализация поведения ботов при столкновении или пересечении с границей трассы;
7. Визуализация траектории, по которой движутся боты.

## **Глазунов**:
1. Определение визуальной модели бота-автомобиля и её реализация;
2. Определение габаритов бота-автомобиля;
3. Описание физических параметров бота-автомобиля (скорость, сцепление, управляемость и пр.)
4. Реализация физики бота при езде по трассе (заносы, дрифты, скорость езды и пр.);
5. Реализация поведения бота при столкновении с неподвижным объектом карты (домом, столбом и пр.);
6. Реализация поведения бота при столкновении с другим ботом-автомобилем;
7. Реализация стратегии-поведения ботов на одной трассе (агрессивная стратегия, миролюбивая стратегия, и пр.);
8. Реализация модели гонки ботов (5-10 штук) на трассе.



# Прогресс по проделанной работе
> # Гончаров

## Последние изменения от 13.07.21
Кратко: карта стала выглядеть более правдоподобно.
Подробно:
1. Изменён материал для отрисовки почвы (Terrain).
2. Изменён материал для отрисовки зданий.
3. Дороги расширены в 2,5 раза для достижения лучшей видимости.
4. Добавлены коллайдеры для дорог, зданий и крыш.
5. Проект очищен от ненужных файлов.

## Скриншоты от 13.07.21
![Сгенерированный город в приближении](https://github.com/Tamerlan91011/AUTO_RACE_UNITY/blob/main/Screenshots/7.jpg)

![Почва с деревьями, но ещё без сгенерированного города](https://github.com/Tamerlan91011/AUTO_RACE_UNITY/blob/main/Screenshots/8.jpg)

![Вид сверху](https://github.com/Tamerlan91011/AUTO_RACE_UNITY/blob/main/Screenshots/9.jpg)

> # Макагонов
# Changelog 
## 07.07.21 Была создана тестовая сцена, на которой проверена и реализована самостоятельная езда автомобиля по дорожному графу.
`Дорожный граф` - это маршрут автомобиля, состоящий из узлов (Node) и ребер, соединяющих их. Движение автомобиля происходит посредством скрипта, который двигает автомобиль вперед к каждому последующему узлу через цикл.
Каждый новый автомобиль, использующий этот скрипт, будет ездить по одному маршруту друг за другом (либо бок о бок).
> # Глазунов
# Changelog 
## **07.07.21.** На основе маршрута была проверена работа езды тестового автомобиля по трассе, а также проанализирована его физика и поведение.
![cardriving](Vid_gif_info/CarDriving.gif)

## В ходе анализа были выявлены следующие моменты, требующие корректировки:
- Повороты осуществляются "топорно", не показывая никакого "отлкика" при повороте (нету покачивания, свойственного для реальных автомомбилей); 
- Автомобиль не снижает скорость на поворотах, от чего на больших скоростях (200+ единиц) он просто врезается в обочину, и застревает;
- Автомобиль в принципе не имеет возможности останавливаться; 
- Автомобиль не имеет возможности сдавать назад в принципе, и в частности при застреваниях на обочине;

## **08.07.21.** Были скорректированы следующие моменты: 
- Автомобилю был добавлен центр массы, обеспечивающий покачивания при поворотах, что делает движение более естественным и плавным;
- Была реализована возможность останавливать автомобиль по нажатию кнопки `IsBraking` (в гиф файле ниже эта кнопка находится под курсором); 
- При нажатии кнопки остановки автомомбиль полностью останавливал движение, прекращая крутить всеми колесами. 
- Теперь автомобиль не застревает и не буксует посреди трассы во время движения и поворотов;
![cardriving_2](Vid_gif_info/CarDriving_2.gif)

## **10.07.21 - 11.07.21** Что получилось сделать: 
- Добавить скрипты **сенсоров**, которые помогают автомомбилю объезжать препятствия, стоящие левее и правее от поля зрения автомобиля;
- Сделать движение при поворотах более плавным;
- Настроить сцепление с дорогой таким образом, чтобы при повортах автомобиль сильно не заносило;
- Реализовать скрипт, при котором будет происходить объезд посредине стоящего объекта;
- Реализовать и протестировать езду двух автомобилей по тестовому маршруту с препятсвиями;
- Протестировать езду двух автомобилей при разных скоростях;
## Выявленные в ходе тестирования проблемы:
- Застревание автомобиля при определенных условиях, то есть не бывает такого, чтобы автомомбиль никогда не застревал;
- При больших скоростях автомобиль может развернуться и въехать в препятствие;
- Автомобиль продолжает ехать вперед даже если застрял;
- Возможна ситуация, при которой второй автомобиль может повлиять на движения первого, встав в его поле зрения и сбив тем самым с пути, спровоцировав застревание;
- Автомобили имеют фиксированную скорость, а значит не могут менять её в зависимости от ситуации (при езде по прямой, при появлении препятствия в поле зрения, при застреваниях);

![cardriving_3](Vid_gif_info/CarDriving_3.gif)
![cardriving_4](Vid_gif_info/CarDriving_4.gif)