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

        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

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

        public SkillVM()
        {
            var temp = SkillDB.GetAllSkills();
            foreach (var item in temp)
            {
                Skills.Add(item);
            }
        }

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

        private void SearchSkill()
        {
            Skills.Clear();
            var temp = SkillDB.SearchSkills(Input);
            foreach (var item in temp)
            {
                Skills.Add(item);
            }
        }

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

        private void insertSkill()
        {
            if (SelectedSkill == null)
            {
                MessageBox.Show("Please select the last row before inserting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                rowsAffected = SkillDB.insertSkill(SelectedSkill);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("skill added!");
                }
                else
                {
                    MessageBox.Show("insert failed!");
                }
            }
        }

        private void updateSkill()
        {
            if (SelectedSkill == null)
            {
                MessageBox.Show("Please make sure that you've selected a skill to update.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                rowsAffected = SkillDB.updateSkill(SelectedSkill);

                if (rowsAffected != 0)
                {
                    MessageBox.Show("skill updated!");
                }
                else
                {
                    MessageBox.Show("update failed!");
                }
            }
        }
    }
}
