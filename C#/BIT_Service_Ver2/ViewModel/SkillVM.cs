using BIT_Service_Ver2.Commands;
using BIT_Service_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIT_Service_Ver2.ViewModel
{
    class SkillVM : NotifyClass
    {
        private ObservableCollection<Skill> _skill = new ObservableCollection<Skill>();
        private Skill _selectedSkill;
        private int rowsAffected;
        private string _input;

        //Will be binded for the SearchInput from the UC: Search
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        //A list of all the skills and their details
        public ObservableCollection<Skill> Skills
        {
            get { return _skill; }
            set { _skill = value; }
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

        //constructor
        public SkillVM()
        {
            var temp = SkillDB.GetAllSkills();
            foreach (var item in temp)
            {
                Skills.Add(item);
            }
        }

        //BUTTON COMMANDS
        //All will be binded to button commands of UC (User Controls): Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(insertSkill, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(updateSkill, true); }
        }
        public RelayCommand Add
        {
            get { return new RelayCommand(AddSkills, true); }
        }
        public RelayCommand Search
        {
            get { return new RelayCommand(SearchSkill, true); }
        }

        //Method for searching a specific skill information
        //This will be binded to the 'Search' button command above
        private void SearchSkill()
        {
            Skills.Clear();
            var temp = SkillDB.SearchSkills(Input);
            foreach (var item in temp)
            {
                Skills.Add(item);
            }
        }

        //Method for adding a new row to the skill data grid
        private void AddSkills()
        {
            int lastRow = Skills.Count;
            Skill skill = new Skill();

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Skills.Add(skill);
                }
            }

        }

        //Method for adding a new skill
        //This will be binded to the 'Save' button command above
        private void insertSkill()
        {
            if (SelectedSkill == null)
            {
                MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    switch (SelectedSkill.ValidateSkill())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = SkillDB.insertSkill(SelectedSkill);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Skill has been added!");
                            }                           
                            break;
                    }
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("Insert failed! Please make sure that you've given the correct details to be added, please try again.");
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
        }

        //Method for updating a skill
        //This will be binded to the 'Update' button command above
        private void updateSkill()
        {
            if (SelectedSkill == null)
            {
                MessageBox.Show("Please make sure that you've selected a skill to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    switch (SelectedSkill.ValidateSkill())
                    {
                        case 0:
                            break;
                        case 1:
                            rowsAffected = SkillDB.updateSkill(SelectedSkill);

                            if (rowsAffected != 0)
                            {
                                MessageBox.Show("Skill has been updated!");
                            }
                            break;
                    }
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("Update failed! Please make sure that you've given the correct details to be added, please try again.");
                }
                catch(Exception e)
                {
                    throw e;
                }
                
            }
        }
    }
}
