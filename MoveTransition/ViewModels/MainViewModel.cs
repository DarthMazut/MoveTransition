using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoveTransition.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<string> _items = [
        "1",
        "2",
        "3"
        ];

    [RelayCommand]
    private void Add()
    {
        Items.Add(GetRandomItem());
    }

    [RelayCommand]
    private void Insert()
    {
        Items.Insert(Items.Count / 2, GetRandomItem());
    }

    [RelayCommand]
    private void Remove()
    {
        Items.RemoveAt(Items.Count - 2);
    }

    [RelayCommand]
    private void RemoveFirst()
    {
        Items.RemoveAt(0);
    }

    [RelayCommand]
    private void Sort()
    {
        Items = new ObservableCollection<string>(Items.OrderBy(int.Parse));
    }

    [RelayCommand]
    private void Clear()
    {
        Items.Clear();
    }

    private string GetRandomItem() => Random.Shared.Next(int.MaxValue).ToString();
}
