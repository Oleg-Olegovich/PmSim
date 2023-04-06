using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

public class Player : Employee
{
    public IStatusChangeNotifier StatusChangeNotifier { get; set; }

    public int Id { get; }

    public string Name { get; }

    private int _money;

    public int Money
    {
        get => _money;
        set => StatusChangeNotifier.Money = _money = value;
    }

    private int _completedProjects;

    public int CompletedProjects
    {
        get => _completedProjects;
        set => StatusChangeNotifier.CompletedProjectsNumber = _completedProjects = value;
    }

    private int _actionsNumber;

    /// <summary>
    /// This is necessary so that the player cannot exceed the number of action per stage.
    /// </summary>
    public int ActionsNumber
    {
        get => _actionsNumber;
        set => StatusChangeNotifier.ActionsNumber = _actionsNumber = value;
    }

    private int _maxEmployeesNumber = 1;

    public int MaxEmployeesNumber
    {
        get => _maxEmployeesNumber;
        set => StatusChangeNotifier.MaxEmployeesNumber = _maxEmployeesNumber = value;
    }

    public ObservableCollection<Employee> Employees { get; } = new();

    /// <summary>
    /// This repository contains projects.
    /// </summary>
    public ObservableCollection<Project> Projects { get; } = new();

    public ObservableCollection<int> Opportunities { get; } = new();

    public bool IsBackgroundChosen { get; set; }

    public bool IsStartupOpen { get; set; }

    public bool IsOut { get; set; }

    public Player(int id, string name, int capital, IStatusChangeNotifier notifier)
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Name = name;
        Money = capital;
        Employees.CollectionChanged += EmployeesCollectionChanged;
        Projects.CollectionChanged += ProjectsCollectionChanged;
        Opportunities.CollectionChanged += OpportunitiesCollectionChanged;
    }

    protected Player(int id, string name, Professions profession, int capital, IStatusChangeNotifier notifier)
        : base(GameConstants.GetSkillsByProfession(profession))
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Money = capital;
        if (profession == Professions.Major)
        {
            Money *= 2;
        }

        Name = name;
    }

    private void EmployeesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StatusChangeNotifier.EmployeesNumber = Employees.Count;
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is Employee newEmployee)
                {
                    StatusChangeNotifier.AddEmployee = Employees.Count - 1;
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is Employee oldEmployee)
                {
                    StatusChangeNotifier.RemoveEmployee = Employees.Count - 1;
                }

                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
            default:
                throw new PmSimException("Unexpected operation with a collection Player.Employee.");
        }
    }

    private void ProjectsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StatusChangeNotifier.ProjectsNumber = Projects.Count;
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is Employee newEmployee)
                {
                    StatusChangeNotifier.AddEmployee = Employees.Count - 1;
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is Employee oldEmployee)
                {
                    StatusChangeNotifier.RemoveEmployee = Employees.Count - 1;
                }

                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
            default:
                throw new PmSimException("Unexpected operation with a collection Player.Projects.");
        }
    }

    private void OpportunitiesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StatusChangeNotifier.EmployeesNumber = Employees.Count;
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is Employee newEmployee)
                {
                    StatusChangeNotifier.AddEmployee = Employees.Count - 1;
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is Employee oldEmployee)
                {
                    StatusChangeNotifier.RemoveEmployee = Employees.Count;
                }

                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
            default:
                throw new PmSimException("Unexpected operation with a collection Player.Opportunities.");
        }
    }
}