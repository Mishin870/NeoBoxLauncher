using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace NeoBoxLauncher.Extensions;

public class ClickableCommand : Border {
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<Button, ICommand?>(nameof(Command), enableDataValidation: true);

    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<Button, object?>(nameof(CommandParameter));

    public static readonly StyledProperty<ClickMode> ClickModeProperty =
        AvaloniaProperty.Register<Button, ClickMode>(nameof(ClickMode));

    public ICommand? Command {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object? CommandParameter {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public ClickMode ClickMode {
        get => GetValue(ClickModeProperty);
        set => SetValue(ClickModeProperty, value);
    }

    public ClickableCommand() {
        PointerPressed += OnPointerPressed;
        PointerReleased += OnPointerReleased;
    }

    private bool IsPressed;

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e) {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) {
            if (IsEffectivelyEnabled) {
                IsPressed = true;
                e.Handled = true;

                if (ClickMode == ClickMode.Press) {
                    OnClick();
                }
            }
        }
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e) {
        if (IsPressed && e.InitialPressMouseButton == MouseButton.Left) {
            IsPressed = false;
            e.Handled = true;

            if (ClickMode == ClickMode.Release &&
                this.GetVisualsAt(e.GetPosition(this)).Any(c => this == c || this.IsVisualAncestorOf(c))) {
                OnClick();
            }
        }
    }

    private void OnClick() {
        if (Command != null && Command.CanExecute(CommandParameter)) {
            Command.Execute(CommandParameter);
        }
    }
}