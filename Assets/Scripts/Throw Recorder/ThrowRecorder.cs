using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRecorder : IThrowRecorder
{
    private List<Dice> _dices;
    private List<RecordData> _recordDatas;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _animation;

    public ThrowRecorder(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;

        _dices = new List<Dice>();
        _recordDatas = new List<RecordData>();
    }

    public void StartRecord(List<Dice> dices)
    {
        _dices.Clear();
        _recordDatas.Clear();

        if (_animation != null)
            _coroutineRunner.StopCoroutine(_animation);

        _dices.AddRange(dices);
        InitRecording();
        Recording();
    }

    public void Play()
    {
        _animation = _coroutineRunner.StartCoroutine(PlayAnimation());
    }

    // Инициализация записи, сохранение начальных позиций и вращений кубиков
    private void InitRecording()
    {
        foreach (Dice dice in _dices)
        {
            _recordDatas.Add(new RecordData(dice.transform.position, dice.transform.rotation));
        }
    }

    // Запись позиций и вращений кубиков во время их движения
    private void Recording()
    {
        Physics.simulationMode = SimulationMode.Script;

        EnablePhysics(true);

        while (!IsAllDiceSleeping())
        {
            for (int i = 0; i < _dices.Count; i++)
            {
                Frame newFrame = new Frame(_dices[i].transform.position, _dices[i].transform.rotation);

                _recordDatas[i].frames.Add(newFrame);
            }

            Physics.Simulate(Time.fixedDeltaTime);
        }

        EnablePhysics(false);

        Physics.simulationMode = SimulationMode.FixedUpdate;
    }

    // Проверка, все ли кубики перестали двигаться
    private bool IsAllDiceSleeping()
    {
        foreach (Dice dice in _dices)
        {
            if (!dice.IsSleeping)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator PlayAnimation()
    {
        int frameCounter = 0;
        bool isHaveFrame = true;

        while (isHaveFrame)
        {
            isHaveFrame = false;
            for (int i = 0; i < _recordDatas.Count; i++)
            {
                if (frameCounter >= _recordDatas[i].frames.Count)
                    continue;

                _dices[i].SetPositionAndRotation(_recordDatas[i].frames[frameCounter].position, _recordDatas[i].frames[frameCounter].rotation);
                isHaveFrame = true;
            }

            frameCounter++;
            yield return new WaitForFixedUpdate();
        }
    }

    private void EnablePhysics(bool value)
    {
        foreach (Dice dice in _dices)
        {
            dice.EnablePhysics(value);
        }
    }

    // Структура для хранения начальной позиции, вращения и кадров каждого кубика
    private struct RecordData
    {
        public Vector3 StartPosition;
        public Quaternion StartRotation;
        public List<Frame> frames;

        public RecordData(Vector3 startPosition, Quaternion startRotation)
        {
            StartPosition = startPosition;
            StartRotation = startRotation;
            frames = new List<Frame>();
        }
    }

    // Структура для хранения позиции и вращения каждого записанного кадра
    private struct Frame
    {
        public Vector3 position;
        public Quaternion rotation;

        public Frame(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
