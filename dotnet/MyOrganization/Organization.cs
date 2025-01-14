﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int _newEmployeeId = 1;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title 
         */ 

        // I know the description says to return empty, so we could return an empty object, however returning
        // an empty object implies that data has been returned, so I think returning null is a better practice
        public Position? Hire(Name person, string title)
        {
            //your code here
            var pos = FindPosition(title);
            if (pos == null)
            {
                return null;
            }
            
            var employee = new Employee(_newEmployeeId, person);
            _newEmployeeId++;
            pos.SetEmployee(employee);

            return pos;

        }

        private Position? FindPosition(string title)
        {
            var allPos = new List<Position>();
            allPos.Add(root);
            
            while(allPos.Count != 0)
            {
                var curPos = allPos[0];
                allPos.RemoveAt(0);

                if(curPos.GetTitle() == title) {
                    return curPos;
                }

                var affiliates = curPos.GetDirectReports();

                foreach( var aff in affiliates )
                {
                    allPos.Add(aff);
                }

            }

            return null;
        }

        

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
