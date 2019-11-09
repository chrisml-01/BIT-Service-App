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

        public RelayCommand Add
        {
            get { return new RelayCommand(insertSkill, true); }
        }

        public RelayCommand Update
        {
            get { return new RelayCommand(updateSkill, true); }
        }

        private void insertSkill()
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

        private void updateSkill()
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
