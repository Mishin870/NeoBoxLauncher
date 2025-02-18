namespace NeoBoxLauncher.Data.Common;

public class Interval(int from = 0, int to = 0) {
    public int From { get; set; } = from;
    public int To { get; set; } = to;
}