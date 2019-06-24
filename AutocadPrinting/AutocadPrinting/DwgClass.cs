using System.Collections.Generic;

namespace AutocadPrinting
{
    class DwgClass
    {
        public string dwgPath;
        public List<LayoutClass> layoutsList;
        public List<XrefClass> xrefStatus;
        public string dwgName;
        public DwgClass(string dwgPath, List<LayoutClass> layoutsList, List<XrefClass> xrefStatus)
        {
            this.dwgPath = dwgPath;
            this.layoutsList = layoutsList;
            this.xrefStatus = xrefStatus;
        }

        public DwgClass()
        {
             //THIS IS BAD 
        }

        public void deleteLayout(string layoutName)
        {
            for(int i = 0; i < layoutsList.Count; i++)
            {
                if(layoutName == layoutsList[i].layoutName)
                {
                    layoutsList.RemoveAt(i);
                    break;
                }
            }
        }

        public bool hasXref()
        {
            foreach(XrefClass xc in xrefStatus)
            {
                if(xc.status != "Resolved")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
