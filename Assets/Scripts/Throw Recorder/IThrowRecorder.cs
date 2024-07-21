using System.Collections.Generic;

public interface IThrowRecorder : IService
{
    void StartRecord(List<Dice> dices);
    void Play();
}