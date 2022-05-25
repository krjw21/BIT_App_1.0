using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Skill : INotifyPropertyChanged
    {
        private string _skillCategory;
        private string _skillDescription;
        private SQLHelper _db;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


        public string SkillCategory
        {
            get { return _skillCategory; }
            set
            {
                _skillCategory = value;
                OnPropertyChanged("SkillCategory");
            }
        }
        public string SkillDescription
        {
            get { return _skillDescription; }
            set
            {
                _skillDescription = value;
                OnPropertyChanged("SkillDescription");
            }
        }


        public Skill()
        {
            _db = new SQLHelper();
        }
        public Skill(DataRow dr)
        {
            this.SkillCategory = dr["Skill_Category"].ToString();
            this.SkillDescription = dr["Skill_Description"].ToString();
            _db = new SQLHelper();
        }


        public string AddSkill(int? contractorID)
        {
            string sql = "UPDATE Contractor_Skill SET [Status] = 1 WHERE Skill_Category = @SkillCategory AND Contractor_ID = @ContractorID";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[0].Value = this.SkillCategory;
            objParameters[1] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[1].Value = contractorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Skill: \"{SkillCategory}\" has been added as a category for this Contractor.";
            }
            return "Skill addition was unsuccessful. Please try again.";
        }
        public string RemoveSkill(int? contractorID)
        {
            string sql = "UPDATE Contractor_Skill SET [Status] = 0 WHERE Skill_Category = @SkillCategory AND Contractor_ID = @ContractorID";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[0].Value = this.SkillCategory;
            objParameters[1] = new SqlParameter("@ContractorID", DbType.String);
            objParameters[1].Value = contractorID;

            int rowsAffected = _db.ExecuteNonQuery(sql, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Skill: \"{SkillCategory}\" has been removed as a category for this Contractor.";
            }
            return "Skill removal was unsuccessful. Please try again.";
        }
        public string AddNewSkill()
        {
            string sqlNewSkill = "INSERT INTO Skill (Skill_Category, Skill_Description) VALUES (@SkillCategory, @SkillDescription)";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[0].Value = this.SkillCategory;
            objParameters[1] = new SqlParameter("@SkillDescription", DbType.String);
            objParameters[1].Value = this.SkillDescription;

            //string sqlContractorSkill

            int rowsAffected = _db.ExecuteNonQuery(sqlNewSkill, objParameters);
            if (rowsAffected >= 1)
            {
                return $"Skill: \"{SkillCategory}\" has been added as a new category to the list.";
            }
            return "Skill addition was unsuccessful. Please try again.";
        }
    }
}
