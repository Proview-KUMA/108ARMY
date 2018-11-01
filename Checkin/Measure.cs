using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using ArmyOnline.Core;
namespace CheckinLib
{
    class Measure
    {
        //public Score mergeToScore(Score score) {
        //    score.BackNo = this.backNo.ToString();
        //    score.BMI = this.bmi;
        //    score.BodyFat = this.bodyFat;
        //    score.Height = this.height;
        //    score.LFID = this.rfid;
        //    score.Weight = this.weight;
        //    score.ArmLength = this.armLength;
        //    if (this.notPass)
        //        score.Status = 3;
        //    else
        //        score.Status = 0;

        //    return score;
        //}


        #region attribute

        double bmr;

        public double Bmr
        {
            get { return bmr; }
            set { bmr = value; }
        }


        double clothingWeight;

        public double ClothingWeight
        {
            get { return clothingWeight; }
            set { clothingWeight = value; }
        }

        
        string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        int gender;

        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        int bodyType;

        public int BodyType
        {
            get { return bodyType; }
            set { bodyType = value; }
        }
        double bmi;

        public double Bmi
        {
            get { return bmi; }
            set { bmi = value; }
        }
        double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        double weight;

        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        double bodyFat;

        public double BodyFat
        {
            get { return bodyFat; }
            set { bodyFat = value; }
        }

        double armLength;

        public double ArmLength
        {
            get { return armLength; }
            set { armLength = value; }
        }

        int backNo;

        public int BackNo
        {
            get { return backNo; }
            set { backNo = value; }
        }

        string rfid;

        public string Rfid
        {
            get { return rfid; }
            set { rfid = value; }
        }

        string printId;

        public string PrintId
        {
            get { return printId; }
            set { printId = value; }
        }


        Boolean notPass = false;

        public Boolean NotPass
        {
            get { return notPass; }
            set { notPass = value; }
        }
        #endregion


        #region calulate

        

        #endregion
    }
}
