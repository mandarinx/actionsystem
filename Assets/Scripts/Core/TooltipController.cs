using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Altruist.UI
{
    public class TooltipController : MonoBehaviour
    {
        public static TooltipController instance;

        [SerializeField] private ATooltipView tooltipView = default;

        public static bool IsVisible { get; set; }

        private void Awake()
        {
            instance = this;
        }

        public static void ShowTooltip(string tooltip)
        {
            instance.tooltipView.Show(tooltip);
            IsVisible = true;
        }

        public static void ShowTooltip(Tooltip tooltip)
        {
            instance.tooltipView.Show(tooltip.ToString());
            IsVisible = true;
        }

        public static void HideTooltip()
        {
            instance.tooltipView.Hide();
            IsVisible = false;
        }
    }

    public class Tooltip
    {
        private readonly List<string> tips;

        private Tooltip()
        {
            tips = new List<string>();
        }

        public static Tooltip Create()
        {
            return new Tooltip();
        }

        public Tooltip Add(string text)
        {
            tips.Add(text);
            return this;
        }

        public Tooltip Add(string text, Color32 color)
        {
            return Add($"<color={color.ToHexString()}>{text}</color>");
        }

        public override string ToString()
        {
            return string.Join("\n\n", tips);
        }

        public bool IsValid => tips.Any();
    }

    public static class TooltipExtensions
    {
        public static string ToHexString(this Color32 c) => $"#{c.r:X2}{c.g:X2}{c.b:X2}";
    }

    public abstract class ATooltipView : MonoBehaviour
    {
        public abstract void Show(string text);
        public abstract void Hide();
    }
}
