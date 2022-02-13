using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data.Enums;

namespace WebAppMovie.Data.ViewModels
{
    public class SortModel
    {
        private string UpIcon = "bi bi-file-arrow-up-fill";
        private string DownIcon = "bi bi-file-arrow-down-fill";


        public string SortedProperty { get; set; }

        public SortOrder SortedOrder { get; set; }

        private List<SortableColumn> sortableColumns = new List<SortableColumn>();


        public void AddColumn(string colName, bool isDefaultColumn = false)
        {
            SortableColumn temp = sortableColumns.Where(c => c.ColumnName.ToLower() == colName.ToLower()).SingleOrDefault();

            if (temp == null)
            {
                sortableColumns.Add(new SortableColumn()
                {
                    ColumnName = colName
                });
            }

            if (isDefaultColumn == true || sortableColumns.Count == 1)
            {
                SortedProperty = colName;
                SortedOrder = SortOrder.Ascending;
            }
        }


        public SortableColumn GetColumn(string colName)
        {
            SortableColumn temp = sortableColumns.Where(c => c.ColumnName.ToLower() == colName.ToLower()).SingleOrDefault();

            if (temp == null)
            {
                sortableColumns.Add(new SortableColumn()
                {
                    ColumnName = colName
                });
            }

            return temp;
        }


        public void ApplySort(string sortExpression)
        {
            if (sortExpression == "")
            {
                sortExpression = SortedProperty;
            }

            sortExpression = sortExpression.ToLower();

            foreach (SortableColumn sortableColumn in sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;

                if (sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    SortedOrder = SortOrder.Ascending;
                    SortedProperty = sortableColumn.ColumnName;

                    sortableColumn.SortIcon = DownIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "_desc";
                }

                if (sortExpression == sortableColumn.ColumnName.ToLower() + "_desc")
                {
                    SortedOrder = SortOrder.Descending;
                    SortedProperty = sortableColumn.ColumnName;

                    sortableColumn.SortIcon = UpIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName;
                }
            }
        }
    }
}
