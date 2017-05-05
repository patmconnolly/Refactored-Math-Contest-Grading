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
        public int returnLevelNumber() { return levelNumber; }

        //Returns a specific student
        public Student returnStudent(int i) { return theClass[i]; }
        
        //Returns appropriate line for team results page
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
            if (level=="AA") { it += "  AA "; }
            else { it += "   A "; }

            //Adds school name
            it += schoolName;
            return it;
        }
        #endregion Getters

        //Setters
        #region Setters
        public void setDivision(string Division) { division = Division; }
        public void setLevel(string Level) { level = Level; }

        //Sneaks in a tie for the school if there is a perfect tie between schools
        //Based on which team had a member scanned first
        //Technically purely random
        /*Don't have a referance, must have forgotten to use, should work anyway just leaves score alone*/
        public void sneakTie() { scoreTie += 1; Score += 0.0001; }

        //Adds a student to the class
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

        //Takes the average of the level number to determine the actual level for this school
        public void completeAdding() { if (studentCount > 0) { levelNumber = levelNumber / studentCount; } }
        #endregion Setters

        #region String Builders
        public List<string> buildRanking()
        {
            List<string> it = new List<string>();
            string tempString;

            it.Add(schoolName + "\t" + division + "\t" + level + "\n");

            for(int i=0;i<3;i++)
            {
                if (i >= theClass.Count) { break; }
                tempString = theClass[i].returnIntScore().ToString();
                while (tempString.Length < 5) { tempString += " "; }
                tempString += "* ";
                tempString += theClass[i].returnName();
                it.Add(tempString);
            }

            for (int i = 3; i < theClass.Count; i++)
            {
                tempString = theClass[i].returnIntScore().ToString();
                while (tempString.Length < 5) { tempString += " "; }
                tempString += "  ";
                tempString += theClass[i].returnName();
                it.Add(tempString);
            }

            it.Add("\nTeam Score: " + scoreInt.ToString());

            it.Add("\n* Team members awarded a T-shirt.\n");

            it.Add(studentCount.ToString() + " students participated.");

            return it;
        }

        public string teamString(int place)
        {
            string it = "Class ";
            if (level == "AA") { it += "AA"; }
            else { it += " A"; }
            it += " - ";
            it += place.ToString() + ": " + schoolName;
            return it;
        }
        
        #endregion String Builders
    }
}
