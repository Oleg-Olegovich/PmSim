using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Projects;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.Others;

public class Player : Employee
{
    public IStatusChangeNotifier StatusChangeNotifier { get; }

    public int Id { get; }

    private int _money;

    public int Money
    {
        get => _money;
        set => StatusChangeNotifier.Money = _money = value;
    }

    public int CompletedProjects { get; set; }

    public int FailedProjects { get; set; }

    private int _actionsNumber;

    /// <summary>
    /// This is necessary so that the player cannot exceed the number of action per stage.
    /// </summary>
    public int ActionsNumber
    {
        get => _actionsNumber;
        set => StatusChangeNotifier.ActionsNumber = _actionsNumber = value;
    }

    private int _maxEmployeesNumber;

    public int MaxEmployeesNumber
    {
        get => _maxEmployeesNumber;
        set => StatusChangeNotifier.MaxEmployeesNumber = _maxEmployeesNumber = value;
    }

    private int _officesNumber;

    public int OfficesNumber
    {
        get => _officesNumber;
        set => StatusChangeNotifier.OfficesNumber = _officesNumber = value;
    }

    private int _techSupportOfficersNumber;

    public int TechSupportOfficersNumber
    {
        get => _techSupportOfficersNumber;
        set
        {
            if (value < 0 || value > OfficesNumber)
            {
                return;
            }
            
            StatusChangeNotifier.TechSupportOfficersNumber = _techSupportOfficersNumber = value;
        }
    }
    
    private bool _isOut;
    
    public bool IsOut 
    { 
        get => _isOut; 
        set => StatusChangeNotifier.IsOut = _isOut = value; 
    }

    public ObservableCollection<Employee> Employees { get; } = new();

    public ObservableCollection<Project> Projects { get; } = new();

    public ObservableCollection<int> Opportunities { get; } = new();

    public int TotalRentPayment { get; set; }
    
    public bool IsBackgroundChosen { get; set; }

    public bool IsStartupOpen { get; set; }

    public Player(int id, string name, int capital, IStatusChangeNotifier notifier)
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Name = name;
        Money = capital;
        MaxEmployeesNumber = 1;
        Employees.CollectionChanged += EmployeesCollectionChanged;
        Employees.Add(this);
        Projects.CollectionChanged += ProjectsCollectionChanged;
        Opportunities.CollectionChanged += OpportunitiesCollectionChanged;
    }

    protected Player(int id, string name, Professions profession, int capital, IStatusChangeNotifier notifier)
        : base(GameConstants.GetSkillsByProfession(profession))
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Money = capital;
        MaxEmployeesNumber = 1;
        if (profession == Professions.Major)
        {
            Money *= 2;
        }

        Name = name;
        Employees.Add(this);
    }

    private void EmployeesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is Employee newEmployee)
                {
                    StatusChangeNotifier.Add(newEmployee);
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is Employee oldEmployee)
                {
                    StatusChangeNotifier.Remove(oldEmployee);
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
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is Project newProject)
                {
                    StatusChangeNotifier.Add(newProject);
                }

                break;
            case NotifyCollectionChangedAction.Remove:
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
            default:
                throw new PmSimException("Unexpected operation with a collection Player.Projects.");
        }
    }

    private void OpportunitiesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems?[0] is int newOpportunity)
                {
                    StatusChangeNotifier.AddOpportunity(newOpportunity);
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems?[0] is int oldOpportunity)
                {
                    StatusChangeNotifier.RemoveOpportunity(oldOpportunity);
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