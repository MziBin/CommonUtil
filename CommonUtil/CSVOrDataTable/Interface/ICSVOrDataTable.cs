using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.CSVOrDataTable.Interface
{
    /// <summary>
    /// CSV文件操作接口，提供将CSV数据与DataTable互相转换的功能
    /// </summary>
    public interface ICSVOrDataTable
    {
        /// <summary>
        /// 从CSV文件读取数据到DataTable
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="hasHeader">CSV文件是否包含表头</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        /// <returns>包含CSV数据的DataTable</returns>
        DataTable ReadCsvToDataTable(string filePath, bool hasHeader = true, char delimiter = ',');

        /// <summary>
        /// 将DataTable数据写入到CSV文件
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要写入的DataTable</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        void WriteDataTableToCsv(string filePath, DataTable dataTable, char delimiter = ',');

        /// <summary>
        /// 向CSV文件追加数据
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要追加的数据</param>
        /// <param name="hasHeader">是否包含表头，默认为false</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        void AppendDataTableToCsv(string filePath, DataTable dataTable, bool hasHeader = false, char delimiter = ',');
    }
}
