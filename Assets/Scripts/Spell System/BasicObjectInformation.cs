using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class BasicObjectInformation
    {
        private string name;
        private string description;
        private Sprite icon;

        public BasicObjectInformation(string oName)
        {
            name = oName;
        }
        public BasicObjectInformation(string oName, string oDescription)
        {
            name = oName;
            description = oDescription;
        }
        public BasicObjectInformation(string oName, string oDescription, Sprite oIcon)
        {
            name = oName;
            description = oDescription;
            icon = oIcon;
        }

        public string ObjectName
        {
            get { return name; }
        }

        public string ObjectDescription
        {
            get { return description; }
        }
        public Sprite ObjectIcon
        {
            get { return icon; }
        }
    }
}
