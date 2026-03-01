using CommonUtil.CSVOrDataTable.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.CSVOrDataTable.Implement
{
    /// <summary>
    /// CSV文件操作实现类，提供CSV数据与DataTable互相转换的功能
    /// </summary>
    public class CSVOrDataTableImpl : ICSVOrDataTable
    {
        /// <summary>
        /// 从CSV文件读取数据到DataTable
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="hasHeader">CSV文件是否包含表头</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        /// <returns>包含CSV数据的DataTable</returns>
        public DataTable ReadCsvToDataTable(string filePath, bool hasHeader = true, char delimiter = ',')
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("CSV文件路径不能为空");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV文件不存在", filePath);

            DataTable dataTable = new DataTable();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int lineCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] values = SplitCsvLine(line, delimiter);
                    lineCount++;

                    if (lineCount == 1)
                    {
                        // 处理表头
                        if (hasHeader)
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                string columnName = values[i] ?? string.Empty;
                                if (string.IsNullOrWhiteSpace(columnName))
                                    columnName = "Column" + i;
                                
                                // 确保列名唯一
                                if (dataTable.Columns.Contains(columnName))
                                {
                                    int duplicateIndex = 1;
                                    string newColumnName = columnName;
                                    while (dataTable.Columns.Contains(newColumnName))
                                    {
                                        newColumnName = $"{columnName}_{duplicateIndex++}";
                                    }
                                    columnName = newColumnName;
                                }
                                
                                dataTable.Columns.Add(columnName, typeof(string));
                            }
                        }
                        else
                        {
                            // 如果没有表头，使用默认列名
                            for (int i = 0; i < values.Length; i++)
                            {
                                dataTable.Columns.Add("Column" + i, typeof(string));
                            }
                            
                            // 添加第一行数据
                            AddRowToDataTable(dataTable, values);
                        }
                    }
                    else
                    {
                        // 添加数据行
                        AddRowToDataTable(dataTable, values);
                    }
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 将DataTable数据写入到CSV文件
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要写入的DataTable</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        public void WriteDataTableToCsv(string filePath, DataTable dataTable, char delimiter = ',')
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("CSV文件路径不能为空");

            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable), "DataTable不能为null");

            // 确保目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (i > 0)
                        writer.Write(delimiter);
                    writer.Write(EscapeCsvValue(dataTable.Columns[i].ColumnName, delimiter));
                }
                writer.WriteLine();

                // 写入数据行
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0)
                            writer.Write(delimiter);
                        writer.Write(EscapeCsvValue(row[i]?.ToString() ?? string.Empty, delimiter));
                    }
                    //最后写入换行符
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// 向CSV文件追加数据
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataTable">要追加的数据</param>
        /// <param name="hasHeader">是否包含表头，默认为false</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        public void AppendDataTableToCsv(string filePath, DataTable dataTable, bool hasHeader = false, char delimiter = ',')
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("CSV文件路径不能为空");

            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable), "DataTable不能为null");

            // 确保目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            bool fileExists = File.Exists(filePath);

            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                // 如果文件不存在且需要表头，写入表头
                if (!fileExists && hasHeader)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0)
                            writer.Write(delimiter);
                        writer.Write(EscapeCsvValue(dataTable.Columns[i].ColumnName, delimiter));
                    }
                    writer.WriteLine();
                }

                // 写入数据行
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0)
                            writer.Write(delimiter);
                        writer.Write(EscapeCsvValue(row[i]?.ToString() ?? string.Empty, delimiter));
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// 分割CSV行，处理包含分隔符的字段
        /// </summary>
        /// <param name="line">CSV行</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>分割后的字段数组</returns>
        private string[] SplitCsvLine(string line, char delimiter)
        {
            List<string> values = new List<string>();
            StringBuilder currentValue = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    // 处理引号
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // 处理转义引号
                        currentValue.Append('"');
                        i++; // 跳过下一个引号
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == delimiter && !inQuotes)
                {
                    // 分隔符，结束当前字段
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                }
                else
                {
                    // 普通字符
                    currentValue.Append(c);
                }
            }

            // 添加最后一个字段
            values.Add(currentValue.ToString());

            return values.ToArray();
        }

        /// <summary>
        /// 转义CSV值，处理包含分隔符、引号或换行符的情况
        /// </summary>
        /// <param name="value">要转义的值</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>转义后的值</returns>
        private string EscapeCsvValue(string value, char delimiter)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            //检查内容中是否包含需要转义的字符，避免csv文件格式错误
            bool needsQuoting = value.Contains(delimiter) || value.Contains('"') || value.Contains('\n') || value.Contains('\r');

            if (!needsQuoting)
                return value;

            // 转义值中的双引号：CSV标准规定，值内的单个双引号需替换为两个连续双引号，没有单引号就不需要替换
            string escapedValue = value.Replace("\"", "\"\"");
            // 用双引号包裹整个值，以处理包含分隔符或换行符的情况
            return $"\"{escapedValue}\"";
        }

        /// <summary>
        /// 向DataTable添加行
        /// </summary>
        /// <param name="dataTable">目标DataTable</param>
        /// <param name="values">行数据</param>
        private void AddRowToDataTable(DataTable dataTable, string[] values)
        {
            DataRow row = dataTable.NewRow();
            //获取列数量，用来防止数组越界
            int columnCount = dataTable.Columns.Count;

            for (int i = 0; i < Math.Min(values.Length, columnCount); i++)
            {
                row[i] = values[i];
            }

            // 处理列数不足的情况
            for (int i = values.Length; i < columnCount; i++)
            {
                row[i] = DBNull.Value;
            }

            dataTable.Rows.Add(row);
        }
    }
}