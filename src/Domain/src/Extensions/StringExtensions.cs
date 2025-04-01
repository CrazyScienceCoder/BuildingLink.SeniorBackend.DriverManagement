using System.Diagnostics.CodeAnalysis;

namespace BuildingLink.DriverManagement.Domain.Extensions;

internal static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? str)
    {
        return !str.IsNullOrWhiteSpace();
    }

    public static string ToAlphabetized(this string str)
    {
        var chars = str.ToCharArray();
        Array.Sort(chars, (a, b) => char.ToLower(a).CompareTo(char.ToLower(b)));
        return new string(chars);
    }
}