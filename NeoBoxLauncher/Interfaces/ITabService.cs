namespace NeoBoxLauncher.Interfaces;

public interface ITabService {
    T Create<T>() where T : ITab;
}