# Cheat-Dice

## Задание
Необходимо создать сцену, где кубики падают с реальной физикой, но значение, которое на них выпадает, должно быть известно заранее. 
Например, значение может приходить с бекенда.

## Версия Unity 
2022.3.38f1

## Управление
Кнопка __T__ - бросок кубиков
  + в каждом броске всегда выпадают дубли
  + при каждом следующем броске число на кубиках увеличивается 
  + бросается кол-во кубиков от 2 до указанного кол-ва точек спавна в `ThrowSettings`

## Настройки
`ThrowSettings` - содержит в себе стартовые значения броска
  + расположение - _Assets/Scriptable Objects/ThrowSettings_
  + `Vector3 ThrowDirection` - направление броска
  + `float ThrowForce` - сила броска
  + `float ThrowForceRandScale` - максимальное значение скейла добавосной силы броска 
  + `float MaxTorqueForce` - максимальная значение стартовой скорости поворота для кадой из осей
  + `List<Vector3> StartPositions` - возможные стартовые позиции кубов

`Dice Physic Material` - физичный материал кубов

## Струкрута

`EnteringPoint` - точка входа
  + отвечает за инициализацию систем, проброс зависимостей и вызовов `Update` и `FixedUpdate` в `ServiceContainer`
  + Единственный `MonoBehaviour`на сцене, не считая создаваемых в процессе `Dice`

`ServiceContainer` - содержит в себе все сервисы, отвечает за доступ к ним и вызывает у них `Update` и `FixedUpdate`
  * в данном проекте избыточен, но при дальнейшем масштабировании необходим, для реализации сервисной архитектуры


## Основные компоненты
  + `DiceController` - отвечает за бросок кубиков
    - имеет единственную публичную функцию `ThrowDices` принимающую на вход `IThrowInfo`
      
  + `IThrowInfo` - содержит в себе лист с необходимыми числами для броваемых кубов
    - в идеале приходит с бэка, сейчас тестового создаётся в том же скрипте
      
  + `ThrowRecorder` - записывает и воспроизводит анимацию падения кубиков
    - `StartRecord` - переводит тип симуляции физики на `SimulationMode.Script` и в одном кадре проводит симуляцию для всех кубиков, запимсывая её
      * в идеале разбить симуляцию на несколько кадров, сейчас даёт довольно сильную спайку 
    - `Play` - воспроизводит записанную ранее анимацию
  
  + Dice - компонент находящийся непостредственно да объекте куба
    - `EnablePhysics` - включает/выключает физику
    - `Throw` - устанавливает начальные значения для броска
    - `SetPositionAndRotation` - дублирует функцию вызов `transform.SetPositionAndRotation` да бы оставить логику работы с объектом в нутри класса
    - `ResetDiceView` - возвращает `_view` в дефолтное положение
    - `FindUpperFace` - поиск текущей верхней грани 
    - `RotateToFace` - поворот `_view` от тещей грани, до необходимой
