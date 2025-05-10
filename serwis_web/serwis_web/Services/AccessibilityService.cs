namespace serwis_web.Services;

public class AccessibilityService
{
    public int? FontSize { get; set; } = null;
    public bool IsHighContrast { get; set; } = false;
    
    public event Action? OnChange;
    
    public void NotifyStateChanged() => OnChange?.Invoke();
}