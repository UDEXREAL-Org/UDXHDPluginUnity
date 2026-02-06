
public struct SingleVirbator
{
    public int ActiveCommand;
    public int Duration;
    public int Amplitude;

    public SingleVirbator(int command = 1, int duration = 0, int amplitude = 4)
    {
        command = command < 1 ? 1 : command > 3 ? 3 : command;
        duration = duration < 0 ? 0 : duration;
        amplitude = amplitude < 4 ? 4 : amplitude > 10 ? 10 : amplitude;

        ActiveCommand = command;
        Duration = duration;
        Amplitude = amplitude;
    }
}

public class VibrationData
{
    public SingleVirbator[] Virbators = new SingleVirbator[2];

    public VibrationData(SingleVirbator[] virbators)
    {
        if (virbators.Length != 2) return;

        for (int i = 0; i < virbators.Length; i++)
        {
            Virbators[i] = virbators[i];
        }
    }
}
