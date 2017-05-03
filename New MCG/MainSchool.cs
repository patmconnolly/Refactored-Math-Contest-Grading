using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_MCG
{
    class MainSchool
    {
        string theName;
        int theCode;

        //The file for everything
        List<string> schoolFile;

        List<string> titlePage;//
        List<string> lowerRanking;
        List<string> upperRanking;
        List<string> lowerIndividualAwards;//
        List<string> upperIndividualAwards;//
        List<string> teamAwards;//
        List<string> lowerFreqDist;//
        List<string> lowerTeamResults;//
        List<string> upperFreqDist;//
        List<string> upperTeamResults;//

        //Getters
        public string returnName() { return theName; }
        public int returnCode() { return theCode; }
        public List<string> returnSchoolFile() { return schoolFile; }

        //Constructor
        public MainSchool(int Code, string Name)
        {
            theCode = Code;
            theName = Name;
        }

        public void buildSchoolFile()
        {
            schoolFile = new List<string>();
            schoolFile.Add(theName);
            for(int i=0; i<titlePage.Count; i++)
            {
                schoolFile.Add(titlePage[i]);
            }
            schoolFile = concatPages(schoolFile, lowerRanking);
            schoolFile = concatPages(schoolFile, upperRanking);
            schoolFile = concatPages(schoolFile, lowerIndividualAwards);
            schoolFile = concatPages(schoolFile, upperIndividualAwards);
            schoolFile = concatPages(schoolFile, teamAwards);
            schoolFile = concatPages(schoolFile, lowerFreqDist);
            schoolFile = concatPages(schoolFile, lowerTeamResults);
            schoolFile = concatPages(schoolFile, upperFreqDist);
            schoolFile = concatPages(schoolFile, upperTeamResults);
        }

        public void addConstantPages(List<string> TitlePage, List<string> LIndAwards, List<string> UIndAwards, List<string> LTeamResults, List<string> UTeamResults, List<string> TeamAwards, List<string> LFDist, List<string> UFDist)
        {
            titlePage = TitlePage;
            lowerIndividualAwards = LIndAwards;
            upperIndividualAwards = UIndAwards;
            teamAwards = TeamAwards;
            lowerTeamResults = LTeamResults;
            upperTeamResults = UTeamResults;
            lowerFreqDist = LFDist;
            upperFreqDist = UFDist;
        }

        public void addSchoolSpecificPages(List<string> LRanking, List<string> URanking)
        {
            lowerRanking = LRanking;
            upperRanking = URanking;
        }
        private List<string> concatPages(List<string> page1, List<string> page2)
        {
            page1.Add("\f");
            for(int i=0; i<page2.Count; i++)
            {
                page1.Add(page2[i]);
            }
            return page1;
        }
        
    }
}
