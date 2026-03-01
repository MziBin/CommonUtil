using CommonUtil.CSVOrDataTable.Interface;
using CommonUtil.CSVOrDataTable.Implement;
using System.Data;

namespace CommonUtil.CSVOrDataTable
{
    /// <summary>
    /// CSV文件操作助手类，提供静态方法简化CSV文件操作
    /// </summary>
    public static class CsvOrDataTableHelper
    {
        private static readonly ICSVOrDataTable _csvHandler = new CSVOrDataTableImpl();

        /// <summary>
        /// 从CSV文件读取数据到DataTable
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="hasHeader">CSV文件是否包含表头</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        /// <returns>包含CSV数据的DataTable</returns>
        public static DataTable ReadCsvToDataTable(string filePath, bool hasHeader = true, char delimiter = ',')
        {
            return _csvHandler.ReadCsvToDataTable(filePath, hasHeader, delimiter);
        }

        /// <summary>
        /// 将DataTable数据写入到CSV文件
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要写入的DataTable</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        public static void WriteDataTableToCsv(string filePath, DataTable dataTable, char delimiter = ',')
        {
            _csvHandler.WriteDataTableToCsv(filePath, dataTable, delimiter);
        }

        /// <summary>
        /// 向CSV文件追加数据
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要追加的数据</param>
        /// <param name="hasHeader">是否包含表头，默认为false</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        public static void AppendDataTableToCsv(string filePath, DataTable dataTable, bool hasHeader = false, char delimiter = ',')
        {
            _csvHandler.AppendDataTableToCsv(filePath, dataTable, hasHeader, delimiter);
        }
    }
}