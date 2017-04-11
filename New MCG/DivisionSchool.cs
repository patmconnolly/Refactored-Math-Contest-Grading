using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_MCG
{
    class DivisionSchool
    {
        #region Variable Definitions
        //School Information
        int schoolCode;
        string schoolName;
        bool used;
        string division;
        string level;
        int levelNumber;

        //List of students
        List<Student> theClass;

        //Student information
        double Score;
        int scoreInt;
        int scoreTie;

        int studentCount;
        #endregion Variable Definitions

        //Constructor
        public DivisionSchool(List<string> it)
        {
            used = false;
            schoolCode = Convert.ToInt32(it[0]);
            schoolName = it[1];
            for(int i=2; i<it.Count; i++)
            {
                schoolName += " " + it[i];
            }

            theClass = new List<Student>();
            studentCount = 0;
            levelNumber = 0;
        }

        //Getters
        #region Getters
        public int returnSchoolCode() { return schoolCode; }
        public string returnSchoolName() { return schoolName; }
        public bool returnUsed() { return used; }
        public string returnDivision() { return division; }
        public string returnLevel() { return level; }
        public int returnScoreInt() { return scoreInt; }
        public int returnTie() { return scoreTie; }
        public double returnScore() { return Score; }

        public Student returnStudent(int i) { return theClass[i]; }

        public string returnTeamAwardString(int place)
        {
            string it = "Class ";
            if (levelNumber / studentCount > 5) { it += "AA - "; }
            else { it += " A - "; }
            it += place.ToString() + ": ";
            it += schoolName;
            return it;
        }

        public string returnTeamResults(int place)
        {
            string it = "";

            //Adds rank
            if (place >= 100) { it += place.ToString(); }
            else if (place >= 10) { it += " " + place.ToString(); }
            else { it += "  " + place.ToString(); }

            it += ".    ";

            //Adds score without tiebreakers
            if (scoreInt < 10) { it += " " + scoreInt.ToString() + "   "; }
            else { it += scoreInt.ToString() + "   "; }

            //Adds tiebreaker
            if (scoreTie < 10) { it += "000" + scoreTie.ToString(); }
            else if (scoreTie < 100) { it += "00" + scoreTie.ToString(); }
            else if (scoreTie < 1000) { it += "0" + scoreTie.ToString(); }
            else { it += scoreTie.ToString(); }

            //Adds level
            if (levelNumber / studentCount > 5) { it += "  AA "; }
            else { it += "   A "; }

            //Adds school name
            it += schoolName;
            return it;
        }
        #endregion Getters

        //Setters
        #region Setters
        public void setSchoolCode(int SchoolCode) { schoolCode = SchoolCode; }
        public void setSchoolName(string SchoolName) { schoolName = SchoolName; }
        public void flipUsed() { used = !used; }
        public void setDivision(string Division) { division = Division; }
        public void setLevel(string Level) { level = Level; }

        public void sneakTie() { scoreTie += 1; Score += 0.0001; }

        public void addStudent(Student it)
        {
            theClass.Add(it);
            used = true;
            studentCount++;
            if(studentCount<=3)
            {
                Score += it.returnScore();
            }
            scoreInt = (int)Score;
            scoreTie = (int)((Score - scoreInt) * 10000);
            levelNumber += it.returnLevel();
        }
        #endregion Setters
        
    }
}
