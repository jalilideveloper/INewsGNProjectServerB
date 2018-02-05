using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INewGN.Models;
namespace INewGN.Utilties
{

    
    public static class DataCenter
    {
        public static List<tblCategory> lstcsategories = new List<tblCategory>();
        public static tblCategory ReturnCategryID(string CategoryName)
        {
            if (DataCenter.lstcsategories.Count > 0)
            {
                return DataCenter.lstcsategories.Find(p => p.CategoryName == CategoryName);
            }
            else
            {
                Controllers.CategoriesController c = new Controllers.CategoriesController();
                c.Get();
                return DataCenter.lstcsategories.Find(p => p.CategoryName == CategoryName);
            }
        }


    }
}