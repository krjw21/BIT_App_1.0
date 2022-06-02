using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BIT_DesktopApp.Logs.LogHelper;

namespace BIT_DesktopApp.Models
{
    public class Skill : INotifyPropertyChanged
    {
        private string _skillCategory;
        private string _skillDescription;
        private SQLHelper _db;
        public static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
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


        // SQL query to add a skill for a specific Contractor
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
                Log(LogTarget.File, $"SUCCESS: New Skill: \"{SkillCategory}\" was added for Contractor ID: \"{contractorID}\".");
                logger.Info($"SUCCESS: New Skill: \"{SkillCategory}\" was added for Contractor ID: \"{contractorID}\".");

                return $"Skill: \"{SkillCategory}\" has been added as a category for this Contractor.";
            }
            Log(LogTarget.File, $"FAILURE: New Skill: \"{SkillCategory}\" insertion for Contractor ID: \"{contractorID}\" unsuccessful.");
            logger.Debug($"FAILURE: New Skill: \"{SkillCategory}\" insertion for Contractor ID: \"{contractorID}\" unsuccessful.");

            return "Skill addition was unsuccessful. Please try again.";
        }

        // SQL query to remove a skill for a specific Contractor
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
                Log(LogTarget.File, $"SUCCESS: Skill: \"{SkillCategory}\" for Contractor ID: \"{contractorID}\" was deactivated.");
                logger.Info($"SUCCESS: Skill: \"{SkillCategory}\" for Contractor ID: \"{contractorID}\" was deactivated.");

                return $"Skill: \"{SkillCategory}\" has been removed as a category for this Contractor.";
            }
            Log(LogTarget.File, $"FAILURE: Skill: \"{SkillCategory}\" deactivation for Contractor ID: \"{contractorID}\" unsuccessful.");
            logger.Debug($"FAILURE: Skill: \"{SkillCategory}\" deactivation for Contractor ID: \"{contractorID}\" unsuccessful.");

            return "Skill deactivation was unsuccessful. Please try again.";
        }

        // SQL query to add a new skill category to the existing list of skill categories
        public string AddNewSkill()
        {
            string sqlNewSkill = "INSERT INTO Skill (Skill_Category, Skill_Description) VALUES (@SkillCategory, @SkillDescription)";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@SkillCategory", DbType.String);
            objParameters[0].Value = this.SkillCategory;
            objParameters[1] = new SqlParameter("@SkillDescription", DbType.String);
            objParameters[1].Value = this.SkillDescription;

            int rowsAffected = _db.ExecuteNonQuery(sqlNewSkill, objParameters);
            if (rowsAffected >= 1)
            {
                Log(LogTarget.File, $"SUCCESS: New Skill: \"{SkillCategory}\" added to list of categories.");
                logger.Info($"SUCCESS: New Skill: \"{SkillCategory}\" added to list of categories.");

                return $"Skill: \"{SkillCategory}\" has been added as a new category to the list.";
            }
            Log(LogTarget.File, $"FAILURE: New Skill: \"{SkillCategory}\" addition unsuccessful.");
            logger.Debug($"FAILURE: New Skill: \"{SkillCategory}\" addition unsuccessful.");

            return "Skill addition was unsuccessful. Please try again.";
        }
    }
}
