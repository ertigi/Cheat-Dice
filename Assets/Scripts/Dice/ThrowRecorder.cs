using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThrowRecorder
{
    private List<Dice> _dices;
    private List<RecordData> _recordDatas;
    private RecorderSettings _recorderSettings;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _animation;

    public ThrowRecorder(RecorderSettings recorderSettings, ICoroutineRunner coroutineRunner)
    {
        _recorderSettings = recorderSettings;
        _coroutineRunner = coroutineRunner;

        _dices = new List<Dice>();
        _recordDatas = new List<RecordData>();
    }

    public void StartRecord(List<Dice> dices)
    {
        if (_dices.Count > 0)
            _dices.Clear();

        if (_recordDatas.Count > 0)
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

    private IEnumerator PlayAnimation()
    {
        float deltaTime = Time.fixedDeltaTime / _recorderSettings.AnimationSpeed;
        int frameCounter = 0;
        bool isHaveFrame = true;

        WaitForSeconds frameTime = new WaitForSeconds(deltaTime);

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
            yield return new WaitForSeconds(deltaTime);
        }
    }

    private void InitRecording()
    {
        foreach (Dice dice in _dices)
        {
            _recordDatas.Add(new RecordData(dice.transform.position, dice.transform.rotation));
        }
    }

    private void Recording()
    {
        Physics.simulationMode = SimulationMode.Script;

        EnablePhysics(true);

        while (!IsAllDiceSliping())
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

    private bool IsAllDiceSliping()
    {
        bool returnValue = true;

        foreach (Dice dice in _dices)
        {
            returnValue &= dice.IsSleeping;
        }

        return returnValue;
    }

    private void EnablePhysics(bool value)
    {
        foreach (Dice dice in _dices)
        {
            dice.EnablePhysics(value);
        }
    }

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
