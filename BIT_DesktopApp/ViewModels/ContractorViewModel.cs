using BIT_DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_DesktopApp.ViewModels
{
    public class ContractorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contractor> _contractors;
        private ObservableCollection<Skill> _allSkills;
        private ObservableCollection<Skill> _contractorSkills;
        private ObservableCollection<Availability> _contractorAvailabilities;
        private ObservableCollection<Suburb> _contractorSuburbs;
        private Contractor _selectedContractor;
        private Skill _selectedSkill;
        private Skill _selectedContractorSkill;
        private Suburb _selectedContractorSuburb;
        private Availability _selectedAvailability;
        private Skill _newSkill;
        private RelayCommand _updateContractorCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addSkillCommand;
        private RelayCommand _removeSkillCommand;
        private RelayCommand _addSuburbCommand;
        private RelayCommand _removeSuburbCommand;
        private RelayCommand _availabilityCommand;
        private RelayCommand _newSkillCommand;
        private bool _enableUpdate;
        private bool _enableSuburbUpdate;
        private bool _enableNewSkill;
        private bool _enableFields;
        private bool _enableSuburbFields;
        private bool _enableButtons;
        private bool _enableSuburbRemove;
        private bool _enableAdd;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set
            {
                _contractors = value;
                OnPropertyChanged("Contractors");
            }
        }
        public ObservableCollection<Skill> AllSkills
        {
            get { return _allSkills; }
            set
            {
                _allSkills = value;
                OnPropertyChanged("AllSkills");
            }
        }
        public ObservableCollection<Skill> ContractorSkills
        {
            get { return _contractorSkills; }
            set
            {
                _contractorSkills = value;
                OnPropertyChanged("ContractorSkills");
            }
        }
        public ObservableCollection<Availability> ContractorAvailabilities
        {
            get { return _contractorAvailabilities; }
            set
            {
                _contractorAvailabilities = value;
                OnPropertyChanged("ContractorAvailabilities");
            }
        }
        public ObservableCollection<Suburb> ContractorSuburbs
        {
            get { return _contractorSuburbs; }
            set
            {
                _contractorSuburbs = value;
                OnPropertyChanged("ContractorSuburbs");
            }
        }
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;

                if (SelectedContractor != null)
                {
                    Skills skills = new Skills(SelectedContractor.ContractorID);
                    this.ContractorSkills = new ObservableCollection<Skill>(skills);


                    Availabilities availabilities = new Availabilities(SelectedContractor.ContractorID);
                    this.ContractorAvailabilities = new ObservableCollection<Availability>(availabilities);

                    Suburbs suburbs = new Suburbs(SelectedContractor.ContractorID);
                    this.ContractorSuburbs = new ObservableCollection<Suburb>(suburbs);
                }

                OnPropertyChanged("SelectedContractor");
                EnableButtons = true;
                EnableSuburbRemove = false;
                EnableFields = false;
                EnableUpdate = false;
            }
        }
        public Skill SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }
        public Skill SelectedContractorSkill
        {
            get { return _selectedContractorSkill; }
            set
            {
                _selectedContractorSkill = value;
                OnPropertyChanged("SelectedContractorSkill");
            }
        }
        public Suburb SelectedContractorSuburb
        {
            get { return _selectedContractorSuburb; }
            set
            {
                _selectedContractorSuburb = value;
                OnPropertyChanged("SelectedContractorSuburb");
                EnableSuburbRemove = true;
            }
        }
        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set
            {
                _selectedAvailability = value;
                OnPropertyChanged("SelectedAvailability");
            }
        }
        public Skill NewSkill
        {
            get { return _newSkill; }
            set
            {
                _newSkill = value;
                OnPropertyChanged("NewSkill");
            }
        }
        public RelayCommand UpdateContractorCommand
        {
            get
            {
                if (_updateContractorCommand == null)
                {
                    _updateContractorCommand = new RelayCommand(this.UpdateContractorMethod, true);
                }
                return _updateContractorCommand;
            }
            set { _updateContractorCommand = value; }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.DeleteContractorMethod, true);
                }
                return _deleteCommand;
            }
            set { _deleteCommand = value; }
        }
        public RelayCommand AddSkillCommand
        {
            get
            {
                if (_addSkillCommand == null)
                {
                    _addSkillCommand = new RelayCommand(this.AddSkillMethod, true);
                }
                return _addSkillCommand;
            }
            set { _addSkillCommand = value; }
        }
        public RelayCommand RemoveSkillCommand
        {
            get
            {
                if (_removeSkillCommand == null)
                {
                    _removeSkillCommand = new RelayCommand(this.RemoveSkillMethod, true);
                }
                return _removeSkillCommand;
            }
            set { _removeSkillCommand = value; }
        }
        public RelayCommand AddSuburbCommand
        {
            get
            {
                if (_addSuburbCommand == null)
                {
                    _addSuburbCommand = new RelayCommand(this.AddSuburbMethod, true);
                }
                return _addSuburbCommand;
            }
            set { _addSuburbCommand = value; }
        }
        public RelayCommand RemoveSuburbCommand
        {
            get
            {
                if (_removeSuburbCommand == null)
                {
                    _removeSuburbCommand = new RelayCommand(this.RemoveSuburbMethod, true);
                }
                return _removeSuburbCommand;
            }
            set { _removeSuburbCommand = value; }
        }
        public RelayCommand AvailabilityCommand
        {
            get
            {
                if (_availabilityCommand == null)
                {
                    _availabilityCommand = new RelayCommand(this.UpdateAvailabilityMethod, true);
                }
                return _availabilityCommand;
            }
            set { _availabilityCommand = value; }
        }
        public RelayCommand NewSkillCommand
        {
            get
            {
                if (_newSkillCommand == null)
                {
                    _newSkillCommand = new RelayCommand(this.NewSkillMethod, true);
                }
                return _newSkillCommand;
            }
            set { _newSkillCommand = value; }
        }
        public bool EnableUpdate
        {
            get { return _enableUpdate; }
            set
            {
                _enableUpdate = value;
                OnPropertyChanged("EnableUpdate");

                if (SelectedContractor != null)
                {
                    if (SelectedContractor.ContractorID != null)
                    {
                        if (value)
                        {
                            MessageBox.Show($"Updating details for the Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\".");
                            EnableFields = true;
                            EnableAdd = false;
                        }
                        else
                        {
                            if (EnableFields == true)
                            {
                                UpdateContractorCommand.Execute(this);
                            }
                            EnableFields = false;
                            EnableAdd = true;
                            //this.ContractorSkills.Clear();
                            //this.ContractorSuburbs.Clear();
                            //this.ContractorAvailabilities.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select a Contractor record before editing.");
                    }
                }
            }
        }
        public bool EnableSuburbUpdate
        {
            get { return _enableSuburbUpdate; }
            set
            {
                _enableSuburbUpdate = value;
                OnPropertyChanged("EnableUpdate");

                if (SelectedContractor != null)
                {
                    if (value)
                    {
                        MessageBox.Show($"Adding a Suburb preference for the Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\".");
                        EnableSuburbFields = true;
                        SelectedContractorSuburb.SuburbName = String.Empty;
                        SelectedContractorSuburb.Postcode = String.Empty;
                    }
                    else
                    {
                        AddSuburbCommand.Execute(this);
                        EnableSuburbFields = false;
                        EnableButtons = false;
                        EnableSuburbRemove = false;
                        this.ContractorSkills.Clear();
                        this.ContractorSuburbs.Clear();
                        this.ContractorAvailabilities.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Contractor record before editing their Suburb preferences.");
                }
            }
        }
        public bool EnableNewSkill
        {
            get { return _enableNewSkill; }
            set
            {
                _enableNewSkill = value;
                OnPropertyChanged("EnableNewSkill");

                if (value)
                {
                    MessageBox.Show($"Adding a new Skill Category to the existing list of Skills.");
                    EnableFields = true;
                }
                else
                {
                    NewSkillCommand.Execute(this);
                    EnableFields = false;
                }
            }
        }
        public bool EnableFields
        {
            get { return _enableFields; }
            set
            {
                _enableFields = value;
                OnPropertyChanged("EnableFields");
            }
        }
        public bool EnableSuburbFields
        {
            get { return _enableSuburbFields; }
            set
            {
                _enableSuburbFields = value;
                OnPropertyChanged("EnableSuburbFields");
            }
        }
        public bool EnableButtons
        {
            get { return _enableButtons; }
            set
            {
                _enableButtons = value;
                OnPropertyChanged("EnableButtons");
            }
        }
        public bool EnableSuburbRemove
        {
            get { return _enableSuburbRemove; }
            set
            {
                _enableSuburbRemove = value;
                OnPropertyChanged("EnableSuburbRemove");
            }
        }
        public bool EnableAdd
        {
            get { return _enableAdd; }
            set
            {
                _enableAdd = value;
                OnPropertyChanged("EnableAdd");
            }
        }


        private string _searchText;
        private string _searchFilter;
        private RelayCommand _searchCommand;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public string SearchFilter
        {
            get { return _searchFilter; }
            set
            {
                _searchFilter = value;
                OnPropertyChanged("SearchFilter");
            }
        }
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(this.SearchMethod, true);
                }
                return _searchCommand;
            }
            set { _searchCommand = value; }
        }
        public void SearchMethod()
        {
            if (SearchFilter != null)
            {
                Contractors allContractors = new Contractors(SearchText, SearchFilter);
                this.Contractors = new ObservableCollection<Contractor>(allContractors);
            }
            else
            {
                MessageBox.Show("Please select a filter before searching for a record.");
            }
        }

        public void RefreshGrid()
        {
            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
        }
        public void UpdateContractorMethod()
        {
            try
            {
                string message = SelectedContractor.UpdateContractor();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with updating Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\". Please try again or contact an Administrator.");
            }
            RefreshGrid();
            EnableButtons = false;
        }
        public void DeleteContractorMethod()
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\"?", "Delete Confirmation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        string message = SelectedContractor.DeleteContractor();
                        MessageBox.Show(message);
                        this.ContractorSkills.Clear();
                        this.ContractorSuburbs.Clear();
                        this.ContractorAvailabilities.Clear();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"There was a problem with deleting this Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\". Please try again or contact an Administrator.");
                        this.ContractorSkills.Clear();
                        this.ContractorSuburbs.Clear();
                        this.ContractorAvailabilities.Clear();
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            RefreshGrid();
        }
        public void AddSkillMethod()
        {
            if (SelectedSkill != null)
            {
                try
                {
                    string message = SelectedSkill.AddSkill(SelectedContractor.ContractorID);
                    MessageBox.Show(message);
                }
                catch (Exception)
                {
                    MessageBox.Show($"There was a problem with adding Skill: \"{SelectedSkill.SkillCategory}\". Please try again or contact an Administrator.");
                }
                Skills skills = new Skills(SelectedContractor.ContractorID);
                this.ContractorSkills = new ObservableCollection<Skill>(skills);
            }
            else
            {
                MessageBox.Show("You must select a Skill in the left table in order to add it.");
            }
        }
        public void RemoveSkillMethod()
        {
            if (SelectedContractorSkill != null)
            {
                try
                {
                    string message = SelectedContractorSkill.RemoveSkill(SelectedContractor.ContractorID);
                    MessageBox.Show(message);
                }
                catch (Exception)
                {
                    MessageBox.Show($"There was a problem with removing Skill: \"{SelectedContractorSkill.SkillCategory}\". Please try again or contact an Administrator.");
                }
                Skills skills = new Skills(SelectedContractor.ContractorID);
                this.ContractorSkills = new ObservableCollection<Skill>(skills);
            }
            else
            {
                MessageBox.Show($"You must select a Skill in the right table in order to remove it.");
            }
        }
        public void NewSkillMethod()
        {
            try
            {
                string message = NewSkill.AddNewSkill();
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with adding the New Skill: \"{NewSkill.SkillCategory}\". Please try again or contact an Administrator.");
            }
            Skills allSkills = new Skills();
            this.AllSkills = new ObservableCollection<Skill>(allSkills);
            NewSkill.SkillCategory = String.Empty;
            NewSkill.SkillDescription = String.Empty;
        }
        public void AddSuburbMethod()
        {
            try
            {
                string message = SelectedContractor.AddSuburb(SelectedContractorSuburb.SuburbName, SelectedContractorSuburb.Postcode);
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with adding Suburb: \"{SelectedContractorSuburb.SuburbName}\". Please try again or contact an Administrator.");
            }
            Suburbs suburbs = new Suburbs(SelectedContractor.ContractorID);
            this.ContractorSuburbs = new ObservableCollection<Suburb>(suburbs);
        }
        public void RemoveSuburbMethod()
        {
            if (SelectedContractorSuburb.SuburbName == null || SelectedContractorSuburb.Postcode == null)
            {
                MessageBox.Show($"You must select a Suburb in the left table before removing it as a preference.");
            }
            else
            {
                try
                {
                    string message = SelectedContractor.RemoveSuburb(SelectedContractorSuburb.SuburbName, SelectedContractorSuburb.Postcode);
                    MessageBox.Show(message);
                }
                catch (Exception)
                {
                    MessageBox.Show($"There was a problem with removing Suburb: \"{SelectedContractorSuburb.SuburbName}\". Please try again or contact an Administrator.");
                }
                Suburbs suburbs = new Suburbs(SelectedContractor.ContractorID);
                this.ContractorSuburbs = new ObservableCollection<Suburb>(suburbs);
                EnableButtons = false;
                EnableSuburbRemove = false;
            }
        }
        public void UpdateAvailabilityMethod()
        {
            try
            {
                if (SelectedAvailability.DayName == null || SelectedAvailability.ShiftType == null)
                {
                    MessageBox.Show("A Day and Shift Type must be selected before updating availability.");
                }
                else
                {
                    Availability newAvailability = new Availability(SelectedContractor.ContractorID, SelectedAvailability.DayName, SelectedAvailability.ShiftType);
                    string message = newAvailability.UpdateAvailability();
                    MessageBox.Show(message);
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"There was a problem with updating the Availability for Contractor: \"{SelectedContractor.FirstName} {SelectedContractor.LastName}\". Please try again or contact an Administrator");
            }
            Availabilities availabilities = new Availabilities(SelectedContractor.ContractorID);
            this.ContractorAvailabilities = new ObservableCollection<Availability>(availabilities);
        }

        public ContractorViewModel()
        {
            SelectedContractorSuburb = new Suburb();
            SelectedAvailability = new Availability();
            NewSkill = new Skill();

            EnableButtons = false;
            EnableAdd = true;
            EnableSuburbRemove = false;

            RefreshGrid();

            Skills allSkills = new Skills();
            this.AllSkills = new ObservableCollection<Skill>(allSkills);
        }
    }
}
